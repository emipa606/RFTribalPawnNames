# Copilot Instructions for RimWorld Mod: Rainbeau's Tribal Pawn Names

## Mod Overview and Purpose

Rainbeau's Tribal Pawn Names is a RimWorld mod designed to enhance the immersion and cultural depth of tribal factions by providing them with unique and culturally-appropriate names. The mod aims to replace the default name generation system with a more specialized one tailored for tribal pawns, enriching the storytelling and gameplay experience in RimWorld.

## Key Features and Systems

- **Unique Name Bank**: A customizable name bank specifically for tribal pawns, featuring names drawn from various tribal cultures.
- **Dynamic Name Generation**: Replaces the game's default name generation system with a more culturally-authentic algorithm for tribal characters.
- **File-Based Configuration**: Allows users to add custom names via configuration files, enabling a highly personalized experience.
- **Seamless Integration**: Compatible with other mods that modify name generation, using Harmony patching for compatibility adjustments.

## Coding Patterns and Conventions

- **Static Class Usage**: The project primarily uses static classes for utility functions related to name generation (e.g., `NameGenerator_GenerateName`).
- **Internal and Public Classes**: Distinction between internal classes (`RTPN_Initializer`) for setup and public classes (`RTPN_NameBank`) for managing accessible resources.
- **Methodology**: Methods are designed to be single-responsibility, performing tasks such as adding names, reading files, or generating names individually.

## XML Integration

As of the current project state, there are no XML files included for data-driven configurations or settings. Future updates might consider using XML files for more complex configurations or broader mod interoperability.

## Harmony Patching

- **Purpose**: Harmony patches are utilized to modify the base game's name generation functionality without directly altering base game files, ensuring compatibility and minimizing conflicts.
- **Usage**: Ensure that all Harmony patches are thoroughly tested to avoid potential conflicts with other mods, especially those that interact with name generation or character generation systems.
  
  For example, when patching methods related to pawn creation, structure patches to be as specific as possible to target only tribal pawn name generation.

## Suggestions for Copilot

- **Naming Recommendations**: Copilot can assist with suggesting culturally accurate names by drawing from historical or encyclopedic data. It can provide default culturally rich names to broaden the name bank.
- **Code Refactoring**: Utilize Copilot's ability to refactor code to improve readability and maintainability of methods such as `AddNames` and `GetName`.
- **Error Handling and Logging**: Implement Copilot suggestions to add comprehensive logging and error handling in methods like `AddNamesFromFile` and `ErrorCheck` to ensure robust performance and easier debugging.
- **Code Integration**: Suggest methods for integrating new features, such as XML configuration integration, which could pave the way for more modular updates.

This file serves as a guide for utilizing GitHub Copilot effectively in the maintenance and further development of this mod, providing a structured approach to expanding the codebase while maintaining quality and compatibility.
