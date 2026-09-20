---
name: dotnet-memory-analysis
description: >-
  Analyze .NET application memory issues using CLI diagnostic tools. Use when
  investigating memory leaks, high memory usage, string duplication, growing
  heap, or GC pressure — on a live process or from a memory dump. Works with
  dotnet-counters, dotnet-gcdump, dotnet-dump, and dotnet-dstrings.
---

# .NET Memory Analysis

Diagnose memory leaks, heap growth, string duplication, and GC pressure in .NET
applications using the standard Microsoft diagnostic CLI tools and custom
ClrMD-based tools — no code changes to the target application required.

## Prerequisites

The following tools must be installed as global .NET CLI tools:

```bash
dotnet tool install -g dotnet-dump
dotnet tool install -g dotnet-counters
dotnet tool install -g dotnet-gcdump
dotnet tool install -g dotnet-dstrings
```

Verify with `dotnet tool list -g`. A .NET runtime (6.0+) is required on the
machine; an SDK is not needed to *run* the tools.

## Key Technique: Non-Interactive SOS Commands

`dotnet-dump analyze` is interactive by default, which does not work well with
agents. Always use the `-c` flag to run SOS commands non-interactively:

```bash
dotnet-dump analyze <dump-path> -c "<command1>" -c "<command2>" -c "exit"
```

Multiple `-c` flags are executed in sequence. Always end with `-c "exit"`.

---

**Note**
Always start by a detailed memory dispatch between native and managed memory with `eeheap` command. On Windows, if WinDBG is installed, use `!address -summary` and `!heap` to get more details about native memory usage, especially threads stacks. On Linux, if LLDB is installed, try to get the same level of native memory details.


## Live Process Investigation

Use this workflow when the application is still running and you want to observe
memory behavior or detect leaks without taking a full dump.

### Identify the Target

```bash
dotnet-dump ps
```

Lists running .NET processes with PID and name. Confirm the target PID.

### Monitor Memory Counters

```bash
dotnet-counters monitor -p <PID> --counters System.Runtime
```

Key counters to watch:
- `gc-heap-size` — growing steadily = possible leak
- `gen-0-gc-count` / `gen-1-gc-count` / `gen-2-gc-count` — frequent Gen2 GCs
  indicate long-lived object pressure
- `exception-count` — unexpected exceptions

Press `q` to stop.

### Detect Leaks with GC Dump Comparison

`dotnet-gcdump` captures a lightweight snapshot of the managed heap (type names, counts, and sizes — no field values or raw memory) by triggering a GC then walking the heap via EventPipe. It is much cheaper than a full `dotnet-dump collect` and does **not** freeze the process for an extended time.

**Step 1 — Capture a baseline snapshot:**

```bash
dotnet-gcdump collect -p <PID> -o baseline.gcdump
```

**Step 2 — Reproduce the suspected leak** (run the scenario, ask the user how long you should wait - 30s is the default, exercise the feature).

**Step 3 — Capture a second snapshot:**

```bash
dotnet-gcdump collect -p <PID> -o after.gcdump
```

**Step 4 — Compare the two snapshots:**

generate a report for each and compare them
```bash
dotnet-gcdump report baseline.gcdump
dotnet-gcdump report after.gcdump 
```

The diff report shows types that **grew** between the two snapshots, sorted by delta size. Look for:
- Application types with large positive deltas — likely leak candidates
- `System.String` or `System.Byte[]` growth — often symptomatic of a leak elsewhere (strings held by a leaking collection, buffers not returned)
- Framework types like `System.Threading.TimerQueueTimer` growing — background work not being cleaned up

**Interpretation tips:**
- Small fluctuations in Gen0/Gen1 types are normal. Focus on types with
  consistent, significant growth.
- If the report shows growth across many unrelated types, look for a collection
  (e.g. `List<T>`, `Dictionary<K,V>`, `ConcurrentDictionary<K,V>`) that holds them all — the collection could be the root cause.
- Confirm findings by repeating the capture/compare cycle; genuine leaks grow every time.

### Escalate to Full Dump

If GC dump comparison identifies suspicious types but you need object-level
detail (field values, GC root chains), capture a full dump:

```bash
dotnet-dump collect -p <PID>
```

**Warning:** This briefly freezes the process. Confirm with the user before
running on a production system. Then continue with the **Dump-Based
Investigation** workflow below.

---

## Dump-Based Investigation

Use this workflow when analyzing a `.dmp` file — either provided by the user or
captured via `dotnet-dump collect`. 

### Heap Analysis (memory leaks)

Start broad, then narrow down.

**Get heap summary by type:**

```bash
dotnet-dump analyze <dump> -c "dumpheap -stat" -c "exit"
```
if the memory dump is multi-GBs large, use:
```bash
dotnet-dump analyze <dump> -c "dumpheap -stat -min 1024" -c "exit"
```
to limit the size of the output

Look at the rightmost columns: **Count** and **TotalSize**. Focus on types with
unexpectedly high counts or sizes. `System.String` is often at the top — that
alone is not a leak; use the Duplicate Strings section to assess separately at the end of the investigation if no leak is detected but memory consumption could be reduced by de-duplicating strings.

**List instances of a suspicious type:**

```bash
dotnet-dump analyze <dump> -c "dumpheap -mt <MethodTable>" -c "exit"
```

The `<MethodTable>` (MT) value comes from the first column of `dumpheap -stat`.
Pick one or two object addresses from the output for root analysis.

**Inspect a specific object:**

```bash
dotnet-dump analyze <dump> -c "dumpobj <address>" -c "exit"
```

Shows field values, size, and generation. Useful for understanding what data
the object holds.

**Check the finalizer queue:**

```bash
dotnet-dump analyze <dump> -c "finalizequeue" -c "exit"
```

A large finalizer queue indicates objects with finalizers that are not being
disposed properly. Look at the "Ready for finalization" count.

### Find Leak Roots

Once you have a suspicious object address:

```bash
dotnet-dump analyze <dump> -c "gcroot <address>" -c "exit"
```

This traces all GC root paths from the target object back to the roots (static
fields, stack variables, GC handles, finalizer queue). The output shows the
chain: root -> ... -> target. Look for:

- **Static field roots**: a static collection holding references indefinitely
- **Event handler roots**: subscribers never unsubscribed
- **Timer/Task roots**: background work keeping objects alive

If multiple objects of the same type are leaking, check 2-3 addresses to
confirm they share a common root pattern.

When a culprit is found, use !dumpmt, !dumpclass, and !dumpil to look at the class implementation that could explain the leak and propose a fix. Another strategy would be to dump the assembly from the dump with `.writemem` on the corresponding module and decompile the class methods with ilspycmd (to install with `dotnet tool install ilspycmd -g` if needed)


### Duplicate Strings

In case of large ratio of strings (>10% of the total size), use the following to analyze duplicated strings

```bash
dotnet-dstrings <dump>
```

Outputs per-generation heap statistics (DupSize%, HeapSize%) followed by a list of duplicated strings sorted by total wasted size. Default thresholds:
count >= 128, cumulated size >= 100 KB, display length <= 64 chars.

Adjust thresholds if duplication is low:

```bash
dotnet-dstrings <dump> --dup -c 32 -s 10
```

Use `--gen` for generation stats only, `--dup` for duplicates only.

**Interpretation:**
- High DupSize% in Gen2 -> long-lived duplicate strings (caching / config issue)
- High DupSize% in Gen0/Gen1 -> transient duplicates (allocation pattern issue)
- Remediation: `string.Intern()` for known-finite sets, deduplication at
  ingestion, `StringPool`, or caching layers

---

## Diagnosis Patterns

| Symptom | Likely Cause | Key Command |
|---------|-------------|-------------|
| Heap growing steadily (live) | Memory leak | `dotnet-gcdump` compare |
| Heap dominated by one type (dump) | Memory leak / missing disposal | `dumpheap -stat` then `gcroot` |
| Massive System.String count | String duplication | `dotnet-dstrings` |
| Large finalizer queue | Missing Dispose calls | `finalizequeue` |
| Frequent Gen2 GCs | Long-lived object pressure | `dotnet-counters` + `dumpheap -stat` |

## Investigation Summary — summary markdown file

Throughout the investigation, maintain a summary file with the following format `<date>-<time>_memory_analysis_SUMMARY.md` file in the current working directory. **Create it before the first command and update it after
every step.** Use the following structure:

```markdown
# Memory Investigation Summary

**Date:** YYYY-MM-DD
**Target:** <process name / dump file path>
**Symptom:** <initial problem description>

## Investigation Steps

### Step N — <brief description>

**Command:**
\```
<exact command line>
\```

**Result:**
<relevant output excerpt — top types, GC dump deltas, root chains, etc.>

**Interpretation:**
<what the result means for the investigation>

**Next action:**
<what will be done next and why>

<!-- repeat for each step -->

## Commands Used

| # | Command | Purpose |
|---|---------|---------|
| 1 | `dotnet-dump ps` | Identify target process |
| 2 | `dotnet-gcdump collect -p 1234 -o baseline.gcdump` | Baseline heap snapshot |
| ... | ... | ... |

## Conclusion

**Root cause:** <identified cause or remaining candidates>
**Evidence chain:** <which steps and results led to the diagnosis>
**Recommended remediation:** <concrete fix suggestions>
```

The file serves as a full audit trail the user can review, share, or archive.

## Safety Guardrails
- **Don't forget to generate the summary file**
- **Never kill a process** without explicit user consent
- **Warn before `dotnet-dump collect`** on production — it freezes the process
- `dotnet-gcdump collect` is lightweight but still triggers a GC; warn on
  latency-sensitive production systems
- **Prefer GC dump comparison** over full dump for initial live investigation
- **Do not attach to system-critical processes** (PID 0, PID 4, services)
- If unsure about a process identity, run `dotnet-dump ps` and confirm with the user before proceeding
