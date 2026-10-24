# GitHub Copilot Instructions for [RF] Tribal Pawn Names (Continued)

Welcome to the GitHub Copilot instructions for the [RF] Tribal Pawn Names (Continued) mod for RimWorld. This mod overhauls the tribal pawn naming system in the game, providing Native American-style names and nicknames for tribal pawns. In this document, we'll outline the mod's key features, coding patterns, XML integration, Harmony patching, and suggestions for Copilot.

## Mod Overview and Purpose

The [RF] Tribal Pawn Names (Continued) mod is designed to update and improve the tribal pawn naming system in RimWorld. The mod changes the tribal pawn names to be Native American-style and introduces various adjectives, terrain features, and weapons as nicknames. The mod also eliminates the requirement for tribal pawns to share last names if they're related and ensures that tribal factions and settlements have names consistent with the individual pawns' names. Pets in tribal factions will also be given tribal-style names rather than colonist-style names.

## Key Features and Systems

The mod's key features include:

- Native American-style tribal pawn names
- Tribal pawn nicknames with various adjectives, terrain features, and weapons
- Elimination of the requirement for tribal pawns to share last names if they're related
- Consistent tribal faction and settlement names
- Tribal-style pet names

## Coding Patterns and Conventions

The mod follows standard RimWorld coding patterns and conventions. The mod's code is organized into several C# files, each containing specific types and members. The mod uses Harmony patching to modify existing game code and introduce new functionality.

## XML Integration

The mod does not use XML for configuration or definition files. Instead, the mod uses C# code to read and process names from text files.

## Harmony Patching

The mod uses the Harmony Patch Library to modify existing game code. The mod's Harmony patches are located in the following C# files:

- `PawnBioAndNameGenerator_GeneratePawnName.cs`
- `RTPN_NameSlot.cs`
- `RTPN_Initializer.cs`
- `RTPN_FileRead.cs`
- `RTPN_NameBank.cs`

The mod's Harmony patches modify the following game methods:

- `PawnBioAndNameGenerator.GeneratePawnName`
- `RTPN_NameSlot.GetRandomName`
- `RTPN_Initializer.Initialize`
- `RTPN_FileRead.LinesFromFile`
- `RTPN_NameBank.AddNamesFromFile`

## Suggestions for Copilot

When using GitHub Copilot to generate code for the [RF] Tribal Pawn Names (Continued) mod, keep the following suggestions in mind:

- Use Harmony patching to modify existing game code and introduce new functionality
- Use C# code to read and process names from text files
- Follow standard RimWorld coding patterns and conventions
- Organize code into several C# files, each containing specific types and members

We hope these instructions will help you get started with modding RimWorld using the [RF] Tribal Pawn Names (Continued) mod as a reference. If you have any questions or need further assistance, please feel free to ask on the Ludeon forums or in the Discord channel.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

