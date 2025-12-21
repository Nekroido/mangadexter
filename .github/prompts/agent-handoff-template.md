# Agent Handoff Template (Reference)

**Note**: This is a reference template, not a prompt file. It shows the format agents should use to notify the Project Coordinator when a work stream is complete.

For actual prompt files, see the `.prompt.md` files in this directory.

## Purpose

Template for notifying the next agent in the execution sequence when work stream is complete.

## Usage

Agent completing a work stream uses this template to notify the Project Coordinator. Coordinator uses this to trigger the next agent(s).

---

## To: [Next Agent Name]

**From**: [Current Agent Name]  
**Work Stream**: [Plan NNNN - Work Stream Title]  
**Status**: ✅ COMPLETE  

### Summary
[2-3 sentence overview of what was completed]

### Success Criteria
- [x] Criterion 1
- [x] Criterion 2
- [x] Criterion 3
[... all items marked complete]

### Changes Made
- [Component 1]: [brief description]
- [Component 2]: [brief description]
- [Build/Test]: Verified with command: `dotnet build` / `dotnet run --project src/App/App.fsproj`

### Blockers Encountered
[None] / [Documented at docs/BLOCKERS.md#section]

### Handoff Notes
[Any context the next agent should know (e.g., deprecations, breaking changes, new conventions)]

### Verification
You can verify completion by:
```bash
dotnet build src/App/App.fsproj  # Should succeed without errors
dotnet run --project src/App/App.fsproj  # Should run without regressions
```

---

**Next Agent**: Please begin work stream using your `.agent.md` as the source of truth for objectives and success criteria.
