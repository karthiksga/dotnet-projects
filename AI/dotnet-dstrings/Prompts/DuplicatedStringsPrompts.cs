using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace dstrings;

[McpServerPromptType]
public static class DuplicatedStringsPrompts
{
    [McpServerPrompt, Description(
        "Analyzes duplicated managed strings from a .NET process or memory dump and summarizes " +
        "which GC generations are most impacted, the top offenders, and the " +
        "percentage of heap wasted by duplication.")]
    public static IEnumerable<ChatMessage> AnalyzeStringDuplication(
        [Description("Process ID to attach to (mutually exclusive with dumpPath)")] int? pid = null,
        [Description("Path to a .NET memory dump file (mutually exclusive with pid)")] string? dumpPath = null)
    {
        var output = DuplicatedStringsTool.GetDuplicatedStrings(
            pid: pid, dumpPath: dumpPath, countThreshold: 64, sizeThresholdKB: 50);

        string target = FormatTarget(pid, dumpPath);
        return new ChatMessage[]
        {
            new(ChatRole.User,
                $"Here is the output of the duplicated strings analysis for {target}:\n\n" +
                $"```\n{output}\n```\n\n" +
                "Summarize the key findings:\n" +
                "- Which GC generations carry the most string duplication?\n" +
                "- What are the top wasted strings and how much memory do they waste?\n" +
                "- Is the duplication mostly in long-lived (gen2/LOH) or short-lived (gen0/gen1) objects?\n" +
                "- What is the overall impact on heap size?"),
            new(ChatRole.Assistant,
                "I'll analyze the per-generation statistics table and the duplicated strings list to provide a structured summary.")
        };
    }

    [McpServerPrompt, Description(
        "Suggests concrete .NET code optimizations (string.Intern, StringPool, " +
        "dictionary dedup, etc.) based on the actual duplicated strings found in a process or dump.")]
    public static IEnumerable<ChatMessage> SuggestStringOptimizations(
        [Description("Process ID to attach to (mutually exclusive with dumpPath)")] int? pid = null,
        [Description("Path to a .NET memory dump file (mutually exclusive with pid)")] string? dumpPath = null)
    {
        var output = DuplicatedStringsTool.GetDuplicatedStrings(
            pid: pid, dumpPath: dumpPath, countThreshold: 64, sizeThresholdKB: 50);

        return new ChatMessage[]
        {
            new(ChatRole.User,
                "Here is the output of the duplicated strings analysis:\n\n" +
                $"```\n{output}\n```\n\n" +
                "Based on the actual duplicated strings found, suggest specific .NET code-level fixes:\n" +
                "- Which strings are good candidates for `string.Intern()`?\n" +
                "- Which patterns suggest using a `HashSet<string>` or dictionary-based dedup?\n" +
                "- Are there URL/path patterns that suggest a `StringPool` or caching layer?\n" +
                "- For large strings on the LOH, what alternative approaches exist?\n" +
                "Prioritize suggestions by estimated memory savings."),
            new(ChatRole.Assistant,
                "I'll review the duplicated strings, classify them by pattern, and recommend the most impactful optimizations.")
        };
    }

    private static string FormatTarget(int? pid, string? dumpPath)
    {
        if (pid.HasValue)
            return $"process {pid.Value}";
        return $"dump `{dumpPath}`";
    }
}
