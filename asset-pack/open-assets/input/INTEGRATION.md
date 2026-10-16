# Input Actions — `.inputactions` Pack

> **TL;DR**: drop `JamKit_MinimalInputActions.inputactions` (or
> `JamKit_DefaultInputActions.inputactions`) into `<unity-project>/Assets/Input/`,
> install the Input System package via UPM, wire `InputActionAsset` to your
> PlayerInput component, done.

## Which file to use

| File | When |
| ---- | ---- |
| `JamKit_MinimalInputActions.inputactions` | Default choice. 3 actions (Move/Jump/Fire), WASD + arrows + space + mouse left + gamepad. ~9KB, hand-readable. **Use this for the jam.** |
| `JamKit_DefaultInputActions.inputactions` | Comprehensive. 13 actions including full UI map (Submit/Cancel/Point/Click/ScrollWheel/MiddleClick/RightClick/TrackedDevicePosition/TrackedDeviceOrientation), 5 control schemes (Keyboard&Mouse/Gamepad/Touch/Joystick/XR), 58 bindings. ~33KB. Use if you need touch / XR / on-screen UI nav. |

Both are derived from `Unity-Technologies/InputSystem` `DefaultInputActions.inputactions`
(Unity Companion License — same terms as the InputSystem package itself; safe
to redistribute as a sample asset within a Unity project that has the
InputSystem package installed).

## Bindings (JamKit_Minimal)

**Player map:**

| Action | Binding | Device |
| ------ | ------- | ------ |
| Move   | WASD (2DVector composite: W/A/S/D) | KeyboardAndMouse |
| Move   | Arrow keys (2DVector composite) | KeyboardAndMouse |
| Move   | `<Gamepad>/leftStick` | Gamepad |
| Jump   | `<Keyboard>/space` | KeyboardAndMouse |
| Jump   | `<Gamepad>/buttonSouth` (A on Xbox, X on PS) | Gamepad |
| Fire   | `<Mouse>/leftButton` | KeyboardAndMouse |
| Fire   | `<Gamepad>/rightTrigger` | Gamepad |

**Control Schemes**: `KeyboardAndMouse` (Keyboard + Mouse), `Gamepad` (any
Gamepad). The PlayerInput component auto-detects which is connected.

## Wiring in Unity (3 minutes)

1. **Install Input System** — In Tuanjie Hub / Unity, open `Window → Package
   Manager`, install `Input System`. (Or in `Packages/manifest.json` add
   `"com.unity.inputsystem": "1.13.0"`.)

2. **Copy the file** — Copy
   `open-assets/input/InputActions/JamKit_MinimalInputActions.inputactions`
   into your Unity project's `Assets/Input/` folder. Unity will create a
   sibling `.meta` file on first import.

3. **Activate New Input System** — `Edit → Project Settings → Player → Other
   Settings → Configuration`. Set "Active Input Handling" to "Input System
   Package (New)" or "Both" (Both is safer for legacy UnityEvent helpers).

4. **Add PlayerInput to your player prefab** — Component → PlayerInput. Drag
   `JamKit_MinimalInputActions.inputactions` into the "Actions" slot. Set
   "Default Map" to `Player`.

5. **Hook callbacks** — Either:
   - Use "Unity Events" mode and wire `Move.performed → PlatformerMover.OnMove`
     etc. in the inspector.
   - Use "Invoke C# Events" mode and subscribe in code:
     ```csharp
     var pi = GetComponent<PlayerInput>();
     pi.actions["Move"].performed += ctx => mover.MoveInput(ctx.ReadValue<Vector2>());
     pi.actions["Jump"].performed += _ => mover.Jump();
     pi.actions["Fire"].performed += _ => shooter.Fire();
     ```

## File validity check

Both files are valid JSON (UTF-8, no BOM). Parseable by Python `json` and by
Unity's InputActionImporter. Schema version: standard 1.x (works with Input
System 1.4+, tested against 1.13).

The 5- and 13-action maps both use only control paths documented in the Input
System 1.x package — no deprecated paths.

## License note

`DefaultInputActions.inputactions` is shipped by Unity inside the InputSystem
package under the Unity Companion License. We redistribute it as a sample
asset — it ships alongside the package itself in the standard InputSystem install
(see `Packages/com.unity.inputsystem/InputSystem/Runtime/Plugins/PlayerInput/DefaultInputActions.inputactions`).
Safe to use in any Unity / Tuanjie project.

> **2026-10-15 fix (R3)**: Earlier drafts of this prep pack shipped a full
> clone of `Unity-Technologies/InputSystem` (≈39 MB including `.git/`) in
> `open-assets/input/InputSystem/`. That full clone has been **deleted** from
> the pack. To get the canonical upstream source, use the official GitHub
> URL:
>
> **Official source**: https://github.com/Unity-Technologies/InputSystem
>
> Install InputSystem via UPM (`com.unity.inputsystem`) — do NOT clone the
> repo into your Unity project.

`JamKit_MinimalInputActions.inputactions` is hand-authored by the
hackathon-prep maintainers under MIT (see `OPEN-ASSETS.md` § G2).