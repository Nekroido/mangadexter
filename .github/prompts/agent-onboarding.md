# Agent Onboarding Template (Reference)

**Note**: This is a reference template, not a prompt file. Share this with agents when assigning them work. For actual prompt files, see the `.prompt.md` files in this directory.

## 🚀 Welcome to the Mangadexter Roadmap

You've been assigned to lead the following work stream:

**Role**: [Your Role Title]  
**Work Stream**: [Plan NNNN - Title]  
**Priority**: [🔴 HIGH / 🟡 MEDIUM]  
**Effort Estimate**: [N days]  
**Timeline**: [Start Date] → [Target Completion]

---

## 📖 What You Need to Know

### Project Context
- **Project**: Mangadexter — F# console application for manga downloads + CBZ archiving
- **Current State**: .NET 5.0 (EOL), Paket-based deps, partial localization, manual testing
- **Goal**: Modernize framework, stabilize dependencies, improve code quality, establish CI/CD
- **Tech Stack**: .NET 10.0, F# language, FSharp.Data, AsyncSeq, Spectre.Console, DotNetZip

### Your Role
[Your agent profile - read `.github/agents/[your-role].agent.md` for full details]

### What Success Looks Like
Your work stream is complete when all success criteria in `.github/agents/[your-role].agent.md` are checked ✓.

---

## 🎯 Getting Started

### Step 1: Read Your Agent Profile
Open `.github/agents/[your-role].agent.md` and review:
- Role summary
- Key directives
- Success criteria checklist
- Escalation procedures

### Step 2: Understand the Roadmap
Read [docs/todo.md](../../docs/todo.md):
- Execution sequence (locked order)
- Your prerequisites (what must complete before you start)
- Your dependents (who waits for you)
- Phase milestones

### Step 3: Review the Plan Document
Open [docs/plan/NNNN-description.md](../../docs/plan/) for detailed implementation guidance:
- Technical scope
- Blocked issues
- Implementation steps
- Testing approach

### Step 4: Claim Prerequisites
If this is your first checkpoint:
- [ ] Confirm all prerequisites from `.agent.md` are met (or this is Phase 1)
- [ ] If prerequisites are NOT met, escalate using `/blockers-and-escalation`
- [ ] Begin work once cleared

### Step 5: Track Progress
As you work:
- Keep your `.agent.md` success criteria updated (mentally, daily)
- If you encounter blockers, use `/blockers-and-escalation`
- Check in with Project Coordinator weekly (or when asked)

### Step 6: Report Completion
When finished:
- [ ] All success criteria marked ✓
- [ ] Use `/review-pr-agent-work` to ensure work is ready
- [ ] Notify Project Coordinator (use agent handoff template)
- [ ] Coordinator will trigger next work stream(s)

---

## 📞 Communication Channels

### Regular Updates
- **Kick-off**: When you begin work, post in roadmap channel (create if needed: #mangadexter-roadmap)
- **Weekly Sync**: 5–10 min check-in (time TBD)
- **Completion**: Post handoff summary

### Blockers & Escalations
- **Immediate**: Use `/blockers-and-escalation` to document and escalate
- **Response Time**: Critical — 2–4 hours; High — 1 day; Medium — 2–3 days

### Questions
- **Technical**: Ask in repo issues or sync with Team Lead
- **Roadmap**: Clarify with Project Coordinator
- **Implementation Details**: Check corresponding plan document (docs/plan/NNNN-*.md)

---

## 🛠 Key Workspace Resources

### Code Structure
```
src/App/
├── Program.fs              # Entrypoint
├── Pages/                  # UI flow modules
├── Console.fs              # CLI primitives
├── Data.fs                 # API integration
├── Manga.fs, Chapter.fs, File.fs   # Core logic
├── Preferences.fs, Utils.fs, Strings.fs  # Helpers
```

### Build Commands
```bash
# Restore (if Paket still active)
.\.paket\paket.exe restore

# Build
dotnet build src/App/App.fsproj

# Run console app
dotnet run --project src/App/App.fsproj

# Run tests (after Phase 1)
dotnet test Mangadexter.sln

# Build release
dotnet build -c Release src/App/App.fsproj
```

### Documentation
- [copilot-instructions.md](.github/copilot-instructions.md) — Architecture, patterns, conventions
- [docs/todo.md](docs/todo.md) — Roadmap master
- [docs/plan/NNNN-*.md](docs/plan/) — Detailed plan documents
- [.agent.md files](.github/agents/) — Individual agent scope

---

## 📊 Coordination & Sequencing

### Locked Sequence
```
1. Framework Lead (.NET Migration)
   ↓ [notify Dependency Manager]
2. Dependency Manager (Paket → NuGet)
   ↓ [notify Maintainer]
3. Maintainer (Package Updates)
   ↓ [notify Tech Lead + UI/UX Lead]
4. Tech Lead (Code Style) ← PARALLEL ← 5. UI/UX Lead (String Localization)
   ↓ [both notify QA/CI Lead]
6. QA/CI Lead (Testing Infrastructure)
   ↓ [verify Phase 2 complete]
RELEASE
```

### Your Position
You are: **[Agent Name]** — Step [N] in sequence

### What You're Waiting For
[List of prerequisite work streams that must complete before you start]

### Who's Waiting for You
[List of work streams that depend on your completion]

---

## ❓ FAQ

**Q: What if I encounter a blocker?**  
A: Use `/blockers-and-escalation` to document immediately. Don't wait; blocking the roadmap impacts the entire team.

**Q: Can I work in parallel with another agent?**  
A: Yes, if explicitly noted in the roadmap sequence (e.g., Tech Lead + UI/UX Lead work in parallel). Check [docs/todo.md](../../docs/todo.md) for "parallel" notation.

**Q: How often should I update status?**  
A: Daily progress mental log; formal check-in weekly with Coordinator. If blocked, escalate immediately.

**Q: What's the definition of "complete"?**  
A: All success criteria in your `.agent.md` are checked ✓, build passes, and manual verification confirms the feature works as expected.

**Q: Who do I contact if I have questions about the plan?**  
A: Read the plan document (docs/plan/NNNN-*.md) first. If unclear, escalate as a blocker or ask Project Coordinator.

---

## ✅ Pre-Work Checklist

Before you start, confirm:
- [ ] You have read your `.agent.md` file (in full)
- [ ] You have read [docs/todo.md](../../docs/todo.md)
- [ ] You have read [docs/plan/NNNN-description.md](../../docs/plan/) for your work stream
- [ ] All prerequisites are met (or this is Phase 1, so no prerequisites)
- [ ] You have access to the repo and can build it locally
- [ ] You know who to escalate to (see `.agent.md` > Escalation section)
- [ ] You have joined the roadmap communication channel (create if needed: #mangadexter-roadmap)

---

**You're ready to start! Good luck, and thank you for contributing to Mangadexter's modernization.** 🚀
