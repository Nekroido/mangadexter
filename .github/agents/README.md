# Mangadexter Custom Agents Guide

This directory contains **custom Copilot agents** that orchestrate the Mangadexter modernization roadmap. Each agent is specialized for a specific work stream and can be assigned to GitHub Issues via [github.com/copilot/agents](https://github.com/copilot/agents).

## Quick Start

1. **Assign an issue to a custom agent:**
   - Go to [github.com/copilot/agents](https://github.com/copilot/agents)
   - Select repository and branch
   - Create a GitHub Issue with your task (e.g., "Migrate Mangadexter from .NET 5.0 to 10.0")
   - Assign issue to the corresponding custom agent

2. **Agent profiles** (in this directory):
   - `framework-lead.agent.md` - .NET 5.0 → 10.0 migration
   - `dependency-manager.agent.md` - Paket → NuGet migration
   - `maintainer.agent.md` - Package version updates
   - `tech-lead.agent.md` - Code Style Guide & analyzer rules
   - `uiux-lead.agent.md` - String localization
   - `qaci-lead.agent.md` - Testing infrastructure & CI/CD
   - `project-coordinator.agent.md` - Roadmap orchestration (not assigned to code tasks)

3. **Each agent file contains:**
   - **YAML frontmatter**: `name`, `description`, `tools` (what the agent can access)
   - **Objective**: What the agent will accomplish
   - **Scope**: What it will and won't do
   - **Success Criteria**: Checklist of deliverables
   - **Testing/Handoff**: How to verify and hand off to the next agent

## Workflow Pattern (GitHub's Agentic Workflow)

```
1. Create GitHub Issue
   ↓ Describe work tightly scoped with acceptance criteria
   ↓
2. Assign to Custom Agent
   ↓ Agent creates branch, implements changes, opens PR
   ↓
3. Review PR
   ↓ Check code, test locally, provide feedback
   ↓ (Optional) Agent iterates based on comments
   ↓
4. Merge & Trigger Next Work Stream
   ↓ Coordinator updates status table
   ↓ Next agent assigned to new issue
```

## Locked Execution Sequence

The roadmap enforces this order to avoid dependency issues:

```
Phase 1: Stabilization
├─ Framework Lead: .NET Migration (must complete first)
├─ Dependency Manager: Paket → NuGet (waits for Framework Lead)
└─ Maintainer: Package Updates (waits for Dependency Manager)

Phase 2: Quality
├─ Tech Lead: Code Style Guide (runs after Maintainer)
├─ UI/UX Lead: String Localization (runs after Maintainer, parallel with Tech Lead)
└─ QA/CI Lead: Testing Infrastructure (runs after both above)

Phase 3: Release
└─ Tag, release, celebrate 🎉
```

## Key Points About These Agents

**Custom agents are NOT:**
- Deployment automation (use GitHub Actions for that)
- Chat bots (they're coding agents—they implement changes)
- Broadly applicable (they're specialized for Mangadexter's roadmap)

**Custom agents ARE:**
- Task-focused (each handles one work stream)
- Scoped (they know what to do and what NOT to do)
- Stateful (they maintain context throughout a task)
- Reviewable (humans review PRs before merge)

## How to Use These Agents

### Creating an Issue for an Agent

Use tightly-scoped, actionable language:

```markdown
# Title: Migrate Mangadexter to .NET 10.0

## Objective
Update the framework from .NET 5.0 (EOL) to .NET 10.0 (current LTS).

## Acceptance Criteria
- [ ] global.json targets net10.0
- [ ] src/App/App.fsproj updated
- [ ] dotnet build succeeds
- [ ] Console app runs: `dotnet run --project src/App/App.fsproj`
- [ ] Feature parity verified (search, download, CBZ creation work)

## File Hints
- global.json
- src/App/App.fsproj
- Mangadexter.sln
```

### Assigning to the Agent

1. In GitHub, assign the issue to the **framework-lead** custom agent
2. Copilot will:
   - Create a new branch
   - Review the codebase
   - Implement changes
   - Open a PR with a detailed summary
3. You review the PR:
   - Check code changes
   - Run tests locally (if needed)
   - Request changes or approve

### Iterating with the Agent

If the PR needs changes:
1. Post a comment on the PR with feedback
2. Copilot will react and create a new commit
3. Re-review and merge when ready

Example feedback:
```
Great start! However, we also need to update the paket.references file to use NuGet. 
Can you add that as well?
```

## Tools Available to Agents

Agents in Mangadexter are configured with these tools:

- `read` - Read files from the repository
- `search` - Search code patterns
- `edit` - Edit files
- `github` - Access GitHub API (create issues, comments, PRs)

Agents **do not** have:
- Shell/terminal access (can't run arbitrary commands)
- Deployment access (can't push to main or publish releases)
- Credential access (all operations authenticated via your account)

## Monitoring Agent Progress

1. **Watch on GitHub**: Agent progress appears as PR comments and commits
2. **Check sessions**: Visit `github.com/copilot/sessions` to see agent logs
3. **Review PRs**: Once agent completes, PR appears in your repo for review

## Troubleshooting

**Agent is stuck or not progressing:**
- Comment on the PR with clarification
- Example: "This file also needs updating—see lines 10–15"

**Agent produced incorrect code:**
- Comment with the issue
- Request changes or manually fix and close PR

**Agent task is too complex:**
- Break it into smaller issues
- Assign smaller issues to the agent

**Need to cancel agent task:**
- Close the GitHub Issue
- Agent will stop and close associated PR

## Further Reading

- [GitHub Docs: Custom Agents](https://docs.github.com/en/copilot/tutorials/customization-library/custom-agents)
- [GitHub Docs: About Custom Agents](https://docs.github.com/en/copilot/concepts/agents/coding-agent/about-custom-agents)
- [GitHub Blog: From Idea to PR](https://github.blog/ai-and-ml/github-copilot/from-idea-to-pr-a-guide-to-github-copilots-agentic-workflows/)
- Mangadexter Roadmap: [docs/todo.md](../../docs/todo.md)

## Questions?

- Check the agent's `.agent.md` file for scope and success criteria
- Reference the roadmap: [docs/plan/](../../docs/plan/)
- Check [copilot-instructions.md](../copilot-instructions.md) for project context
