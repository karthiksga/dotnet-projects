# dotnet-pstacks

A .NET global CLI tool and MCP server to display merged call stacks (parallel stacks) from a .NET application (live process or memory dump).

## Installation

```bash
dotnet tool install --global dotnet-pstacks
```

## CLI Usage

```bash
dotnet-pstacks [-p <pid> | <dumpfile>] [-t <threadIdLimit>]
```

### Options

| Option | Description | Default |
|--------|-------------|---------|
| `-p <pid>` | Process ID to attach to | |
| `<dumpfile>` | Path to a memory dump file | |
| `-t <threadIdLimit>` | Max thread IDs to display per stack group (-1 for all) | 4 |
| `--mcp` | Start as stdio MCP server | |

Provide either `-p <pid>` or a dump file path, but not both.

### Example

```
dotnet-pstacks myapp.dmp -t 8
```

## MCP Server Usage

```bash
dotnet-pstacks --mcp
```

When started with `--mcp`, the tool runs as a stdio-based MCP server exposing:
- A `GetParallelStacks` **tool** with the same parameters as the CLI
- A `DiagnoseThreadingIssues` **prompt** that runs the analysis and asks the LLM to identify deadlocks, contention, and starvation
- A `FindDeadlocks` **prompt** that focuses specifically on circular wait patterns
- An `AnalyzeThreadPoolHealth` **prompt** that assesses how many threads are idle, blocked, or doing useful work

### Cursor / VS Code configuration

Add the following to your MCP configuration (e.g. `.cursor/mcp.json` or user-level `mcp.json`):

```json
{
  "mcpServers": {
    "dotnet-pstacks": {
      "type": "stdio",
      "command": "dotnet-pstacks",
      "args": ["--mcp"]
    }
  }
}
```

## License

MIT
