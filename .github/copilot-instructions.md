# Copilot Instructions

## Shared Instructions

Shared Copilot instructions, skills and prompts are maintained centrally in the
[account-level .github repository](https://github.com/f2calv/.github). They are deliberately not
copied here. Clone that repository and add it to the VS Code workspace, or link its instruction
folders into `~/.copilot/`. If the shared files are unavailable, stop rather than guessing the
conventions.

Everything below is specific to this repository.

## Repository Purpose

This public .NET 10 learning repository demonstrates event persistence with Marten and PostgreSQL
from a hosted worker in a minimal ASP.NET Core application.

- Build from `event-sourcing-dotnet.slnx` and keep NuGet versions centralized.
- Keep PostgreSQL and pgAdmin as optional local Compose services.
- Supply the Marten connection string through configuration or environment variables. Never replace
  the tracked placeholder with a real credential.
- Keep generated PostgreSQL and pgAdmin data out of source control.
- Preserve cancellation propagation in the background service and avoid logging event payloads or
  connection details.
