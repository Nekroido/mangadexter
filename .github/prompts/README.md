# Mangadexter Prompt Files

This directory contains reusable **prompt files** (`.prompt.md` extension) for VS Code Copilot Chat. These prompts streamline common tasks in the Mangadexter modernization roadmap.

## Quick Start

### Using Prompt Files in VS Code

1. **In Chat, type `/` followed by the prompt name:**
   ```
   /roadmap-status
   /create-work-issue
   /review-pr-agent-work
   /blockers-and-escalation
   ```

2. **Or use the Command Palette:**
   - `Ctrl+Shift+P` → "Chat: Run Prompt"
   - Select a prompt from the list

3. **Or click the play button:**
   - Open the `.prompt.md` file in editor
   - Click the play button in the title bar
   - Choose "Run in chat" or "New chat session"

## Available Prompts

### 📊 `/roadmap-status`
**Purpose**: Report on modernization roadmap progress and blockers

**Use when**: You need a quick status check on all work streams without reading the docs manually

**Example**:
```
/roadmap-status

(Agent will read the roadmap and report: which phases are complete, 
which are in progress, what blockers exist, and what's next)
```

### 📝 `/create-work-issue`
**Purpose**: Create a tightly-scoped GitHub Issue for assigning to a custom agent

**Use when**: You want to create an issue that a custom agent can execute cleanly

**Example**:
```
/create-work-issue

Agent: Create an issue for migrating src/App/App.fsproj to .NET 10.0

(Copilot will draft a well-formed issue with acceptance criteria, 
file hints, and context links ready to paste into GitHub)
```

### 🔍 `/review-pr-agent-work`
**Purpose**: Review pull requests created by custom agents against acceptance criteria

**Use when**: A custom agent has created a PR and you need guidance on what to check

**Example**:
```
/review-pr-agent-work

Agent: Review this PR for .NET migration completeness

(Copilot will check: acceptance criteria met, code quality, 
build passing, testing verified, documentation complete)
```

### 🚨 `/blockers-and-escalation`
**Purpose**: Document blockers from custom agent work and route to appropriate owners

**Use when**: An agent encounters an issue that's out of scope

**Example**:
```
/blockers-and-escalation

Agent: A custom agent encountered "SDK not found" error

(Copilot will help document the blocker, determine root cause, 
suggest resolution, and identify who to escalate to)
```

## File Format

All prompt files follow VS Code's standard `.prompt.md` format:

```markdown
---
name: prompt-name                    # Used after typing "/" in chat
description: What this prompt does   # Shown in prompt picker
agent: ask | edit | agent           # Which agent to use
tools: ['tool1', 'tool2']           # Tools available to agent
---

# Prompt Title

[Your prompt instructions in Markdown]
```

**YAML Frontmatter Fields:**
- `name` - Short identifier (used with `/name` in chat)
- `description` - Shown in prompt picker dropdown
- `agent` - `ask` (chat only), `edit` (edit chat), or `agent` (coding agent)
- `tools` - Available tools: `github`, `codebase`, etc.

**Body:**
- Markdown formatting
- Instructions for what you want Copilot to do
- Context and guidelines
- References to workspace files (using relative paths)

## Tips for Using These Prompts

1. **Test locally first**: Use the play button in editor to test a prompt before sharing
2. **Combine with manual input**: `/roadmap-status Then focus on Phase 2 blockers`
3. **Reference files**: Prompts can read `.github/copilot-instructions.md`, docs, code
4. **Pass extra context**: Add after prompt name: `/review-pr-agent-work for the .NET migration PR`
5. **Iterate**: If first response isn't complete, ask follow-up questions in chat

## Creating Your Own Prompts

To create a new prompt file:

1. In VS Code Chat, click the gear icon → "Prompt Files" → "New prompt file"
2. Choose "Workspace" (saves to `.github/prompts/`)
3. Name the file (e.g., `my-prompt.prompt.md`)
4. Add YAML frontmatter (name, description, agent, tools)
5. Write your prompt in Markdown body
6. Click the play button to test
7. Iterate until it works well

**Example new prompt:**
```markdown
---
name: audit-strings
description: Find hard-coded UI strings in Pages modules
agent: ask
tools: ['codebase']
---

# Audit Hard-Coded Strings

Search the Pages modules for hard-coded English strings.
List each string, the file, and the line number.
Format as a table: String | File | Line | Replace With

Files to check:
- src/App/Pages/Root.fs
- src/App/Pages/Search.fs
- src/App/Pages/Manga.fs
- src/App/Pages/Preferences.fs
```

## Related Resources

- [VS Code Prompt Files Documentation](https://code.visualstudio.com/docs/copilot/customization/prompt-files)
- [Custom Agents](./../agents/README.md) - For more complex work streams
- [Roadmap](../../docs/todo.md) - Current status of all work streams
- [Copilot Instructions](../.github/copilot-instructions.md) - Project context and conventions

## Troubleshooting

**Prompt not showing up in picker:**
- Ensure file has `.prompt.md` extension
- Check it's in `.github/prompts/` or VS Code user profile folder
- Reload VS Code if needed

**Tools not working in prompt:**
- Verify tool name is correct (e.g., `github`, `codebase`)
- Check agent type supports the tool (agent mode has more tools than ask/edit)
- Try without specific tools to use defaults

**Prompt response incomplete:**
- Add follow-up questions in chat
- Rephrase instructions more specifically
- Add examples to the prompt body
- Use the play button to test and iterate
