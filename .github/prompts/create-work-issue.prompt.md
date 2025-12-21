---
name: create-work-issue
description: Creates a tightly-scoped GitHub Issue for a specific work stream with acceptance criteria
agent: agent
---

# Create Work Stream Issue

You are a project manager. Your task is to create a well-structured GitHub Issue for assigning to a Mangadexter custom agent.

## Issue Creation Guidelines

**Good issues are:**
- Single-focused (one deliverable, one work stream)
- Tightly scoped (1-3 days of work, not entire epics)
- Have clear acceptance criteria (checkboxes)
- Include file hints (which files need updating)
- Specify success metrics

**Bad issues are:**
- Vague: "Modernize the app"
- Too broad: "Refactor entire codebase"
- No success criteria
- Missing context

## Issue Template

Use this structure for new issues:

```markdown
# Title: [Action] [Component] - [Brief Result]

## Objective
[1-2 sentences describing what will be accomplished]

## Acceptance Criteria
- [ ] Specific, measurable requirement 1
- [ ] Specific, measurable requirement 2
- [ ] Build verification: `dotnet build` succeeds
- [ ] Feature verification: manual test of key functionality

## File Hints
- Path/to/file1.fs
- Path/to/file2.fsproj
- docs/plan/NNNN-description.md

## Context
[Link to relevant plan document or roadmap section]
```

## Example

**Good:**
```
# Migrate src/App/App.fsproj to .NET 10.0

## Objective
Update the App.fsproj target framework from net5.0 to net10.0.

## Acceptance Criteria
- [ ] TargetFramework set to net10.0 in App.fsproj
- [ ] dotnet build src/App/App.fsproj succeeds
- [ ] dotnet run --project src/App/App.fsproj executes without errors
- [ ] Console app search functionality works

## File Hints
- src/App/App.fsproj
- global.json (if SDK version needs update)
```

**Bad:**
```
# Modernize the .NET framework

## Objective
Update to latest .NET

## Acceptance Criteria
- Works
```

Use this to create issues that agents can execute cleanly.
