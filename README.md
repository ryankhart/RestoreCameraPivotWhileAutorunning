# Restore Camera Pivot While Auto-Running

Standalone Dalamud plugin for FFXIV that temporarily restores legacy camera pivot behavior while auto-running.

## Why Use It

If you play with Legacy movement and have `Disable camera pivot` enabled, auto-running can still force camera behavior that feels inconsistent with the rest of your movement. This plugin makes auto-run feel more consistent by restoring the legacy pivot only during auto-run.

## What It Does

- Only affects Legacy Type movement
- Only affects the case where `Disable camera pivot` is enabled
- Only changes behavior while the player is auto-running

## Behavior

When the game would normally use `ThirdPersonFixed` camera control mode during auto-run, this plugin temporarily switches it back to `ThirdPersonLegacy`.

## Build

```bash
dotnet build -c Release
```

## Notes

- This plugin has no settings UI.
- This plugin uses a low-level hook on camera control mode resolution.
