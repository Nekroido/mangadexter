---
name: verify-build
description: Verifies that Mangadexter builds and runs successfully
agent: ask
---

# Verify Build Health

You are a build verification specialist. Your task is to verify that Mangadexter builds and runs correctly.

## Build Verification Checklist

Check the following:

**Current Build Status:**
- [ ] What is the current target framework in `src/App/App.fsproj`?
- [ ] What is the SDK version in `global.json`?
- [ ] Are there any build errors or warnings in recent commits?

**Dependency Status:**
- [ ] Is the project using Paket or NuGet for package management?
- [ ] Are all package references in `.fsproj` resolvable?
- [ ] Are there any outdated or vulnerable packages?

**Recent Changes:**
- [ ] What was the last commit to the main branch?
- [ ] Did any recent changes affect build files (`.fsproj`, `global.json`, etc.)?

**Build Instructions:**
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build src/App/App.fsproj

# Run the application
dotnet run --project src/App/App.fsproj
```

## Report Format

Provide a brief health report:

**Status**: ✅ Healthy | 🟡 Warnings | 🔴 Critical

**Current Configuration:**
- Framework: [version]
- Package Manager: [Paket/NuGet]
- Last Commit: [date/hash]

**Issues Found:**
- [Issue 1]: [severity and impact]
- [Issue 2]: [severity and impact]

**Recommended Actions:**
- [ ] Action 1
- [ ] Action 2

This helps keep the build in good shape before starting new work streams.
