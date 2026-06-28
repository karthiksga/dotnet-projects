static class FileChecks
{
    public static Checked<string[], string> CheckLength(string[] lines) =>
        lines.Length > 0 && lines.Length < 1000 ? Checked<string[], string>.Unit(lines)
        : Checked<string[], string>.Failed(lines, "File must have between 1 and 999 lines.");

    public static Checked<string[], string> CheckSorted(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.Compare(lines[i - 1], lines[i]) > 0)
                return Checked<string[], string>.Failed(lines, "File must be sorted.");
        }
        return Checked<string[], string>.Unit(lines);
    }

    public static Checked<string[], string> CheckGrowingLines(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Length <= lines[i - 1].Length)
                return Checked<string[], string>.Failed(lines, "Each line must be longer than the previous one.");
        }
        return Checked<string[], string>.Unit(lines);
    }
}