# Agent Coordination Checkpoint Template (Reference)

**Note**: This is a reference template, not a prompt file. For actual prompt files, see the `.prompt.md` files in this directory.

For status reporting, use `/roadmap-status` prompt.

## Purpose

Periodic status check and synchronization point between agents working on parallel work streams.

## Usage

Project Coordinator uses this weekly (or when needed) to check progress, resolve blockers, and ensure timeline adherence.

---

## 📋 Coordination Checkpoint: [Date]

### Phase: [Stabilization / Quality / Release]

**Last Checkpoint**: [date]  
**Target Completion**: [date]  
**Days Remaining**: [N]

---

## Status Update

### Per-Agent Progress

| Agent | Work Stream | Status | Progress | Blocker | ETA |
|-------|-------------|--------|----------|---------|-----|
| Framework Lead | .NET Migration | ⏳ In-Progress | 60% | None | [date] |
| Dependency Manager | Paket → NuGet | ⏳ Queued | 0% | Blocked on Framework Lead | [date] |
| Maintainer | Package Updates | ⏳ Queued | 0% | Blocked on Dependency Manager | [date] |
| Tech Lead | Code Style Guide | ⏳ Queued | 0% | Can start after Maintainer | [date] |
| UI/UX Lead | String Localization | ⏳ Queued | 0% | Can start after Maintainer | [date] |
| QA/CI Lead | Testing Infrastructure | ⏳ Queued | 0% | Blocked on Phase 1 completion | [date] |

### Key Metrics
- **Build Status**: [🟢 Passing / 🟡 Warning / 🔴 Failing]
- **Test Coverage**: [Current % / Target %]
- **Code Quality**: [Style violations if any, analyzer status]
- **Phase 1 Readiness**: [% complete / On track / At risk / Blocked]

---

## Blocker Review

### Critical Blockers
[List any critical blockers blocking forward progress]

### Medium Blockers
[List any medium-priority blockers]

### Resolutions
- **Blocker 1**: Owner — Resolution in progress; ETA [date]
- **Blocker 2**: Escalated; awaiting [Owner]'s response

---

## Adjustments & Re-Planning

### Original Timeline vs. Actual
- Framework Lead: Planned 2–3 days, actual progress suggests [on-time / delayed by N days / ahead]
- [Other agents with timeline adjustments]

### Revised Estimates
[If timeline has shifted, document new estimates and reasons]

### Contingency Activations
[If contingency plan needed: brief description and impact]

---

## Action Items for Next Week

| Owner | Task | Deadline | Blocker Resolution |
|-------|------|----------|-------------------|
| [Agent] | [Task] | [date] | If blocked on [X], proceed with [alt approach] |
| [Agent] | [Task] | [date] | Continue current approach |

---

## Coordinator Sign-Off

**Last Updated**: [date/time]  
**Next Checkpoint**: [date]  
**Overall Status**: [On-Time / At-Risk / Delayed]  

[Coordinator Signature / Approval]
