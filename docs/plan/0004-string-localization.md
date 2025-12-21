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
| **Coverage** | Partial: Some UI text in Strings.resx; many prompts/labels hard-coded in Pages |
| **Hard-coded strings** | ~45 literals found in Pages/Search.fs, Pages/Root.fs, Pages/Preferences.fs, Pages/Manga.fs |
| **Examples** | `"Manga title:"`, `"Found works:"`, `"Save path"`, `"Creating CBZ"`, `"Done!"` |
| **Pattern** | Inconsistent: Some accessed via `Strings.Strings.GetString "key"`, many inline in prompts |

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
- [ ] Audit and categorize hard-coded strings
  - [ ] List all 45 strings by module (Search, Root, Preferences, Manga)
  - [ ] Group by type: prompts, labels, status messages, error messages, table headers
- [ ] Extend Strings.resx
  - [ ] Add entries for each hard-coded string (using naming convention: `<noun><context>`)
  - [ ] Keep existing entries; don't rename
- [ ] Update Pages/* to use Strings accessors
  - [ ] Replace inline strings with `Strings.Strings.GetString "keyName"` calls
  - [ ] Test each Page module after updates
- [ ] Update Strings.fs (if auto-generated) — Regenerate to include new keys
- [ ] Validate rendering and behavior
  - [ ] `dotnet build src/App/App.fsproj` (no compile errors)
  - [ ] `dotnet run --project src/App/App.fsproj` (verify all Pages display correctly)
- [ ] Document localization workflow (optional)
  - [ ] Add note to docs/STYLE_GUIDE.md: all UI text must go in Strings.resx
  - [ ] Define new string naming convention for future contributions

---

## Blockers

| Blocker | Impact | Mitigation |
|---------|--------|-----------|
| Strings.resx encoding or generation issues | Build fails after adding keys; Strings.fs not updated | Test adding a single key first; verify Strings.resx is properly UTF-8 and regenerates Strings.fs |
| UI strings depend on concatenation or formatting | Some strings may not fit simple key pattern | Store base strings in Strings.resx; apply formatting in code (e.g., `String.Format(Strings.GetString "template", value)`) |
| Incomplete string extraction | Some hard-coded strings missed | Use grep/semantic search to find remaining string literals in Pages/ after migration |

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

