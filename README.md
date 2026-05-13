# Restore Camera Pivot While Auto-Running

Standalone Dalamud plugin for FFXIV that temporarily restores legacy camera pivot behavior while auto-running.

## Why Use It

If you play with Legacy movement and have `Disable camera pivot` enabled, this plugin makes it easier to keep moving and steer with one hand while using the mouse or the right thumbstick with the other to interact with UI windows like the map. It restores the legacy pivot only during auto-run so your movement and camera stay easier to control while your cursor or thumbstick is busy elsewhere.

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

## Icon Attribution

- <a href="https://www.magnific.com/icon/video-camera_13270083">Icon by Freepik</a>
