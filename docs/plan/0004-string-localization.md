# String Localization & Hard-Coded String Extraction

## Value

- **i18n readiness**: Centralize all user-facing text in Strings.resx; simplify translation workflows
- **Maintainability**: Changing UI text requires no code edits; reduces merge conflicts and regressions
- **Consistency**: Avoid duplicate string definitions; single source of truth for prompts, labels, messages
- **Accessibility**: Enables support for additional languages without code changes

---

## Goal

Migrate ~45 hard-coded UI strings from Pages/*.fs to Strings.resx. Establish pattern for all future UI text.

---

## Current State

| Aspect | Details |
|--------|---------|
| **Localization infrastructure** | ✅ Strings.resx + Strings.fs accessor exist |
| **Coverage** | ✅ All UI text migrated to Strings.resx |
| **Hard-coded strings** | ✅ 0 remaining hard-coded strings in Pages/ |
| **Examples** | ✅ All prompts/labels now accessed via `Strings.Strings.GetString "key"` |

---

## Steps

### Already Taken
- [x] Inventoried all hard-coded strings in Pages/ (45 total)
- [x] Identified pattern for accessing Strings.resx (`Strings.Strings.GetString`)
- [x] Confirmed no existing localization tooling (e.g., gettext) in use

## Dependencies

**Execution after**: Plan 0002 (Package Updates)

**Coordination notes** (asynchronous, non-blocking): String naming conventions align with Code Style Guide (Plan 0003) post-implementation; does not block parallel execution.

## Steps
- [x] Audit and categorize hard-coded strings
  - [x] List all 45 strings by module (Search, Root, Preferences, Manga)
  - [x] Group by type: prompts, labels, status messages, error messages, table headers
- [x] Extend Strings.resx
  - [x] Add entries for each hard-coded string (using naming convention: `<noun><context>`)
  - [x] Keep existing entries; don't rename
- [x] Update Pages/* to use Strings accessors
  - [x] Replace inline strings with `Strings.Strings.GetString "keyName"` calls
  - [x] Test each Page module after updates
- [x] Update Strings.fs (if auto-generated) — Regenerate to include new keys
- [x] Validate rendering and behavior
  - [x] `dotnet build src/App/App.fsproj` (no compile errors)
  - [x] `dotnet run --project src/App/App.fsproj` (verify all Pages display correctly)
- [x] Document localization workflow (optional)
  - [x] Add note to docs/STYLE_GUIDE.md: all UI text must go in Strings.resx
  - [x] Define new string naming convention for future contributions

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Strings.resx encoding or generation issues | Build fails after adding keys; Strings.fs not updated | Resolved by testing adding keys first; verified Strings.resx is properly UTF-8 and regenerates Strings.fs |
| UI strings depend on concatenation or formatting | Some strings may not fit simple key pattern | Resolved by storing base strings in Strings.resx; applying formatting in code (e.g., `String.Format(Strings.GetString "template", value)`) |
| Incomplete string extraction | Some hard-coded strings missed | Resolved by using grep/semantic search to find remaining string literals in Pages/ after migration |

---

## Responsible Agent

**UI/UX Lead** — ownership of Strings.resx content, localization patterns, and Page-level string usage. Coordinate with Tech Lead on naming conventions and documentation updates.

---

## Success Criteria

- ✅ All ~45 hard-coded strings migrated to Strings.resx with consistent key names
- ✅ Pages/ modules use `Strings.Strings.GetString` for all UI text (no inline literals except logging)
- ✅ `dotnet build` succeeds; no compile errors
- ✅ `dotnet run` shows all prompts, labels, and status messages rendering correctly
- ✅ Localization workflow documented in STYLE_GUIDE.md or copilot-instructions.md
