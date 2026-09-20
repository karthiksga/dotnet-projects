using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace ParallelStacks;

[McpServerPromptType]
public static class ParallelStacksPrompts
{
    [McpServerPrompt, Description(
        "Diagnoses threading issues (deadlocks, contention, starvation, " +
        "sync-over-async) from the merged call stacks of a .NET process or dump.")]
    public static IEnumerable<ChatMessage> DiagnoseThreadingIssues(
        [Description("Process ID to attach to (mutually exclusive with dumpPath)")] int? pid = null,
        [Description("Path to a .NET memory dump file (mutually exclusive with pid)")] string? dumpPath = null)
    {
        var output = ParallelStacksTool.GetParallelStacks(
            pid: pid, dumpPath: dumpPath, threadIdLimit: -1);

        string target = FormatTarget(pid, dumpPath);
        return new ChatMessage[]
        {
            new(ChatRole.User,
                $"Here are the merged/parallel call stacks from {target}:\n\n" +
                $"```\n{output}\n```\n\n" +
                "Analyze these stacks for threading issues:\n" +
                "- Are there signs of a deadlock (multiple groups waiting on locks/monitors)?\n" +
                "- Is there thread pool starvation (many threads blocked on sync waits)?\n" +
                "- Are there sync-over-async patterns (`.Result`, `.Wait()`, `.GetAwaiter().GetResult()`)?\n" +
                "- What is the overall thread health (how many roots, how many idle vs stuck)?"),
            new(ChatRole.Assistant,
                "I'll examine each stack group, look for blocking patterns, and classify the threading health.")
        };
    }

    [McpServerPrompt, Description(
        "Specifically looks for deadlock patterns in merged call stacks " +
        "by examining lock waits and circular dependencies.")]
    public static IEnumerable<ChatMessage> FindDeadlocks(
        [Description("Process ID to attach to (mutually exclusive with dumpPath)")] int? pid = null,
        [Description("Path to a .NET memory dump file (mutually exclusive with pid)")] string? dumpPath = null)
    {
        var output = ParallelStacksTool.GetParallelStacks(
            pid: pid, dumpPath: dumpPath, threadIdLimit: -1);

        string target = FormatTarget(pid, dumpPath);
        return new ChatMessage[]
        {
            new(ChatRole.User,
                $"Here are the full merged/parallel call stacks from {target} (all thread IDs shown):\n\n" +
                $"```\n{output}\n```\n\n" +
                "Focus specifically on deadlock detection:\n" +
                "- Identify thread groups that are blocked on `Monitor.Enter`, `WaitOne`, `WaitForSingleObject`, or similar\n" +
                "- Look for circular wait patterns where group A holds a resource group B needs, and vice versa\n" +
                "- List the specific thread IDs involved and the lock methods in their stacks\n" +
                "- If no deadlock is found, explain what the blocking threads are waiting for"),
            new(ChatRole.Assistant,
                "I'll trace the blocking calls across stack groups to identify any circular wait chains.")
        };
    }

    [McpServerPrompt, Description(
        "Assesses thread pool health by analyzing how many threads are idle, " +
        "blocked, or doing useful work.")]
    public static IEnumerable<ChatMessage> AnalyzeThreadPoolHealth(
        [Description("Process ID to attach to (mutually exclusive with dumpPath)")] int? pid = null,
        [Description("Path to a .NET memory dump file (mutually exclusive with pid)")] string? dumpPath = null)
    {
        var output = ParallelStacksTool.GetParallelStacks(
            pid: pid, dumpPath: dumpPath, threadIdLimit: 4);

        string target = FormatTarget(pid, dumpPath);
        return new ChatMessage[]
        {
            new(ChatRole.User,
                $"Here are the merged call stacks from {target}:\n\n" +
                $"```\n{output}\n```\n\n" +
                "Assess the thread pool health:\n" +
                "- How many total threads vs how many roots (distinct stack patterns)?\n" +
                "- How many threads appear idle (waiting for work items)?\n" +
                "- How many are blocked on I/O, locks, or synchronous waits?\n" +
                "- Is there evidence of thread pool starvation (many threads, all blocked)?\n" +
                "- Recommend whether the application needs async refactoring or different pool settings"),
            new(ChatRole.Assistant,
                "I'll categorize each stack group by activity type and assess the overall pool utilization.")
        };
    }

    private static string FormatTarget(int? pid, string? dumpPath)
    {
        if (pid.HasValue)
            return $"process {pid.Value}";
        return $"dump `{dumpPath}`";
    }
}
