# Scrolling Smoother

[![Build](https://github.com/Sandidsisir/scrolling-smoother/actions/workflows/build.yml/badge.svg)](https://github.com/Sandidsisir/scrolling-smoother/actions/workflows/build.yml)

A local Windows build of a smooth-scrolling utility, with the managed settings GUI and installer source.

This is an independent local build. It is not affiliated with the original publisher, and no license is granted for the original application.

## Features

- Per-application smooth-scrolling controls.
- Managed WinForms settings window.
- Tray menu for quick enable/disable actions.
- Per-user installer with backup and uninstall support.
- Settings survive a reinstall.

## Repository layout

| Path | Description |
|---|---|
| `app/` | Ready-to-run application folder |
| `dist/SmoothScrollSetup.exe` | Double-click installer |
| `dist/SmoothScrollSetup.exe.sha256` | Installer checksum |
| `src/SmoothScrollGUI/` | C# WinForms settings GUI source |
| `src/SmoothScrollSetup/` | C# installer source |
| `src/refs/` | Reference assemblies used by the GUI build |
| `assets/app.ico` | Shared application and installer icon |
| `build.ps1` | Builds the GUI, repacks the payload, and builds the installer |
| `SHA256SUMS.txt` | Checksums for the shipped binaries |
| `.github/workflows/build.yml` | GitHub Actions **Build** workflow |

The built application keeps its internal `SmoothScroll` file names for compatibility.

## Requirements

- Windows 10 or 11
- .NET SDK 8 or newer, including the .NET Framework 4.8 reference assemblies
- `System.Resources.Extensions` 8.0.0 (restored from NuGet or the local cache)

## Build locally

From the repository root:

```powershell
powershell -ExecutionPolicy Bypass -File .\build.ps1
```

The build script:

1. Builds `src/SmoothScrollGUI/SmoothScrollGUI.csproj`.
2. Copies the new `SmoothScrollGUI.exe` and config into `app/`.
3. Re-zips `app/` into `src/SmoothScrollSetup/payload.zip`.
4. Builds `src/SmoothScrollSetup/SmoothScrollSetup.csproj`.
5. Copies the installer to `dist/SmoothScrollSetup.exe`.
6. Writes `dist/SmoothScrollSetup.exe.sha256` and `SHA256SUMS.txt`.

Both projects build with `0 Warning(s), 0 Error(s)`.

## Build on GitHub

The repository includes a **Build** workflow in `.github/workflows/build.yml`.

1. Open the **Actions** tab.
2. Select **Build**.
3. Click **Run workflow**.
4. Download the `Scrolling-Smoother` artifact from the run.

The artifact contains the installer, its checksum, a portable app archive, and `SHA256SUMS.txt`. Pushing a tag such as `v1.0.0` also publishes a GitHub Release with the same build outputs.

## Install

Double-click `dist/SmoothScrollSetup.exe` and click **Install**.

The installer:

- Closes any running application process.
- Backs up an existing per-user app folder on the first install.
- Extracts the embedded application payload.
- Sets the per-user auto-start entry.
- Creates Desktop and Start Menu shortcuts.
- Starts the application.

The **Uninstall** button removes the shortcuts and auto-start entry, restores the backup if present, and keeps the user settings folder so settings survive a reinstall.

Silent switches:

```text
SmoothScrollSetup.exe /silent
SmoothScrollSetup.exe /silent /uninstall
```

## Verification

- GUI and installer build with `0 Warning(s), 0 Error(s)`.
- The settings form, About form, and custom minimize dialog render and exit cleanly.
- No new entries are written to the application error log during smoke tests.
- Silent install, uninstall, and reinstall pass.
- Installed files match `app/`.
- `SHA256SUMS.txt` records the shipped file checksums.

## Current build

```text
dist/SmoothScrollSetup.exe
2,188,800 bytes
SHA256 ea999fd01c266badb6283f3a4d56a7a6ae35886c52e22bd6bea14232a4d9d8f1
```
