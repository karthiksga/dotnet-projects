using static FileChecks;

class FileSystemRunner
{
    public void Process(string filePath)
    {
        string report = Locate(new FileInfo(filePath))                  // Checked<FileInfo, string>
            .Bind(Read, [])                                             // Checked<string[], string>
            .Bind(CheckLength)                                          // Checked<string[], string>
            .Bind(CheckSorted)                                          // Checked<string[], string>
            .Bind(CheckGrowingLines)                                    // Checked<string[], string>
            .Match(
                onPassed: lines => $"File is valid with {lines.Length} lines.",
                onFailed: (lines, problem) => $"File check failed: {problem}"
            );

        Console.WriteLine(report);
    }

    private static Checked<FileInfo, string> Locate(FileInfo file) =>
        file.Exists ? Checked<FileInfo, string>.Unit(file)
        : Checked<FileInfo, string>.Failed(file, "File does not exist.");
    

    public static Checked<string[], string> Read(FileInfo file)
    {
        try
        {
            string[] lines = File.ReadAllLines(file.FullName);
            return Checked<string[], string>.Unit(lines);
        }
        catch (Exception)
        {
            return Checked<string[], string>.Failed(Array.Empty<string>(), $"Failed to read file {file.FullName}.");
        }
    }
}