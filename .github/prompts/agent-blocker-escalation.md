# Agent Blocker Escalation Template (Reference)

**Note**: This is a reference template, not a prompt file. For actual prompt files, see the `.prompt.md` files in this directory.

For an actual prompt that handles blocker escalation, use `/blockers-and-escalation`.

## Purpose

Template for escalating blockers or unresolved issues that require intervention outside the agent's scope.

## Usage

Any agent encountering a blocker uses this template to escalate to the Project Coordinator or relevant owner.

---

## 🚨 BLOCKER: [Blocker Title]

**Reported By**: [Agent Name]  
**Work Stream**: [Plan NNNN - Work Stream Title]  
**Severity**: [Critical | High | Medium | Low]  
**Impact**: [Blocks this phase / Blocks next phase / Deferred blocking]  

### Description
[Detailed description of the blocker, including what was attempted and where the block occurred]

### Root Cause
[What caused this issue? (e.g., SDK conflict, dependency version conflict, breaking API change)]

### Steps to Reproduce
```bash
[Command(s) that trigger the blocker]
```

### Expected Behavior
[What should happen]

### Actual Behavior
[What actually happens]

### Context
- **Current Framework**: [net5.0 / net10.0 / ...] or relevant context
- **Current Package**: [package name and version]
- **Error Message**: [Full error output, truncated if >50 lines]

### Attempted Workarounds
- [Approach 1]: Result — [worked / failed / not applicable]
- [Approach 2]: Result — [worked / failed / not applicable]

### Proposed Solution
[Agent's best guess at resolution, or request for guidance]

### Escalation Path
- **Primary Owner**: [Who should resolve this]
- **Secondary Owner**: [Fallback if primary unavailable]
- **Target Resolution Time**: [hours / days]

### Next Steps for Agent
[ ] Awaiting [Owner Name]'s guidance  
[ ] Will proceed with [alternative approach] if no response by [date]  
[ ] Preparing rollback plan in case escalation takes >24 hours

---

**Coordinator Note**: Route to [Owner] immediately. This blocks Phase [1/2].
