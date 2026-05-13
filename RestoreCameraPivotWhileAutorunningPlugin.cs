using Dalamud.Hooking;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Control;

namespace RestoreCameraPivotWhileAutorunning;

public unsafe class RestoreCameraPivotWhileAutorunningPlugin : IDalamudPlugin {
    private Hook<Camera.Delegates.DetermineControlMode>? determineControlModeHook;
    private bool disposed;

    public string Name => "Restore Camera Pivot While Auto-Running";

    public RestoreCameraPivotWhileAutorunningPlugin(IDalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Service>();
        Service.Framework.Update += OnFrameworkUpdate;
        TryInitializeHook();
    }

    public void Dispose() {
        if (disposed) return;
        disposed = true;

        Service.Framework.Update -= OnFrameworkUpdate;
        determineControlModeHook?.Disable();
        determineControlModeHook?.Dispose();
        determineControlModeHook = null;
    }

    private void OnFrameworkUpdate(IFramework framework) {
        if (determineControlModeHook == null) {
            TryInitializeHook();
        }
    }

    private void TryInitializeHook() {
        var camera = CameraManager.Instance()->Camera;
        if (camera == null) return;

        determineControlModeHook ??= Service.GameInteropProvider.HookFromAddress<Camera.Delegates.DetermineControlMode>((nint)camera->VirtualTable->DetermineControlMode, DetermineControlModeDetour);
        if (!determineControlModeHook.IsEnabled) {
            determineControlModeHook.Enable();
            Service.Log.Information("Enabled DetermineControlMode hook.");
        }
    }

    private CameraControlMode DetermineControlModeDetour(Camera* thisPtr) {
        var mode = determineControlModeHook!.Original(thisPtr);
        if (mode != CameraControlMode.ThirdPersonFixed) return mode;
        if (!Service.GameConfig.UiControl.TryGetUInt("MoveMode", out var moveMode) || moveMode != 1) return mode;
        return InputManager.IsAutoRunning() ? CameraControlMode.ThirdPersonLegacy : mode;
    }
}
