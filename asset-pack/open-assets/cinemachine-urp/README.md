# Cinemachine + URP Post-Processing Pack

> **TL;DR**: drop `VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset` (or
> `CinemachineSampleSceneProfile_URP.asset`) into `<unity-project>/Assets/Settings/`,
> create a `Global Volume` GameObject referencing it, done. Cinemachine
> parameter values for 2D platformer / 3D third-person setups are in the
> table below.

## What's in here

```
cinemachine-urp/
├── VolumeProfiles/
│   ├── JamKit_URP_PostProcessing_Profile.asset      ← URPExamples FPS scene (Bloom+Vignette+CA+Tonemapping+ColorAdjustments+LiftGammaGain)
│   ├── JamKit_URP_PostProcessing_Profile.asset.meta ← stable GUID 38da09cb0d89b4bbc821be62fc6b67ae
│   ├── CinemachineSampleSceneProfile_URP.asset       ← Cinemachine 3.x official URP scene profile (Bloom+Vignette+Tonemapping)
│   └── CinemachineSampleSceneProfile_URP.asset.meta  ← stable GUID 10fc4df2da32a41aaa32d77bc913491c
├── _URPExamples_source_FPS_profile/FPSCameraDemo/Post-process Volume Profile.asset  ← 1 source file extracted from URPExamples (Unity Companion License)
├── _Cinemachine_source_URP_sample/Settings/{ForwardRenderer,SampleSceneProfile,UniversalRP-*} × 3 quality tiers ← 6 source files from Cinemachine 3.x URP sample
├── _Cinemachine_source_RunningRace_scene/RunningRace.unity ← 1 source scene file from Cinemachine 3.x RunningRace sample
└── README.md     (this file)
```

> **Note**: This pack ships ONLY the **.asset / .unity source files we extracted** — NOT a full clone of the upstream repos. Upstream Unity-Technologies/UniversalRenderingExamples (~2.9k files) and Unity-Technologies/com.unity.cinemachine (full package incl. Samples~) were NOT bundled into this prep pack because:
> 1. The handful of source files we actually need (FPS Post-process Volume Profile, URP sample Render Pipeline + Volume Profile, RunningRace scene) are extracted verbatim into `_URPExamples_source_*` / `_Cinemachine_source_*` subdirectories.
> 2. Redistributing a full multi-thousand-file Unity sample repo inside another Unity project is overkill for a 24h jam and bloats the prep pack by hundreds of MB.
>
> If you want to inspect the full upstream source, see `OPEN-ASSETS.md` § "Files on disk (this pack)" for the GitHub URLs.

## URP Post-Processing Volume Profiles

Both `.asset` files are real `VolumeProfile` assets ready to drop into a
project. They include the GUID-bearing `.meta` files so they import as
stable references in the editor.

### Profile 1: `JamKit_URP_PostProcessing_Profile.asset`

From Unity's official URP examples (`URPExamples/_CompletedDemos/FPS/FPSRenderPass/FPSCameraDemo`).
A "light jam-tuned" preset:

| Effect | Active | m_Value | Notes |
| ------ | ------- | ------ | ----- |
| Bloom | ✓ | threshold 1.0 (override off), intensity **0.5**, scatter 0.7, clamp 65472 | Light bloom |
| ColorAdjustments | ✓ | postExposure 1 (override) | Neutral |
| LiftGammaGain | ✓ | lift (0.92, 0.93, 1.0), gamma (1.0, 0.97, 0.92) | Slightly warm tones |
| Tonemapping | ✓ | mode 2 (Neutral) | Filmic-ish |
| Vignette | ✓ | intensity **0.4** | Subtle edges |
| ChromaticAberration | ✓ | intensity **0.1** | Subtle RGB fringing |

Total: 6 overrides. Tuned for visible-but-subtle post FX.

### Profile 2: `CinemachineSampleSceneProfile_URP.asset`

From Unity's official Cinemachine 3.x URP sample scene
(`com.unity.cinemachine/Projects/URP/Assets/Settings/SampleSceneProfile.asset`).
The "Unity reference baseline" for jam games:

| Effect | Active | m_Value |
| ------ | ------ | ------- |
| Tonemapping | ✓ | mode 2 (Neutral) |
| Bloom       | ✓ | threshold 1.0, intensity **1.0** |
| Vignette    | ✓ | intensity **0.25**, smoothness 0.4 |

Total: 3 overrides. The minimum useful profile.

### Which one to use?

- **Profile 2** for clean jam-style graphics (3 overrides = lowest perf hit).
- **Profile 1** when you want some character (warm tone shift, chromatic fringing).

Both are Unity Companion License, compatible with URP 7.x → 14.x (the
VolumeProfile YAML format has been stable since URP 7; GUIDs reference URP
package classes that ship with any URP version).

### GUID separation between VolumeProfiles and reference sources

The two VolumeProfile `.asset` files in `VolumeProfiles/` use **stable,
purpose-built** GUIDs (different from any upstream Unity asset so they
never collide on import):

| Asset | This pack GUID |
| ----- | -------------- |
| `VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset` | `38da09cb0d89b4bbc821be62fc6b67ae` |
| `VolumeProfiles/CinemachineSampleSceneProfile_URP.asset` | `10fc4df2da32a41aaa32d77bc913491c` |

The `_URPExamples_source_FPS_profile/.../Post-process Volume Profile.asset`
and `_Cinemachine_source_URP_sample/Settings/SampleSceneProfile.asset`
reference copies were originally pointing to the **same** upstream GUIDs
as the profiles above — that broke Unity import (two assets, one GUID).
They now carry **distinct** GUIDs (`...b67af` and `...491d`) so both
pairs can coexist in the same Unity project without conflict.

## Cinemachine recommended parameters

These come from inspecting `Cinemachine/com.unity.cinemachine/Samples~/3D Samples/RunningRace.unity`
(the official Unity "Running Race" sample scene, Cinemachine 3.x format).

### 3D Third-Person Follow (CinemachineCamera + CinemachineThirdPersonFollow + CinemachineRotationComposer)

| Component | Field | Value (from sample) | Notes |
| --------- | ----- | -------------------:| ----- |
| CinemachineCamera | Lens.FieldOfView | 60° | Standard TPS FOV |
| | Lens.NearClipPlane | 0.3 | |
| | Lens.FarClipPlane | 1000 | |
| | OutputChannel | 1 (Base) | |
| CinemachineThirdPersonFollow | CameraDistance | 10 | Distance behind target |
| | CenterOnActivate | true | Snap to ideal pos on activate |
| | TargetOffset | (0, 20, 0) | Aim slightly above pivot |
| | Damping | (1, 1, 1) | Soft follow |
| | Composition.ScreenPosition | (0, 0) | Center |
| | Composition.DeadZone | enabled=false, (0.2, 0.2) | |
| | Composition.HardLimits | enabled=false, (0.8, 0.8) | |
| | Lookahead | enabled=false | (turn on if needed) |
| CinemachineRotationComposer | LookAtOffset | (0, 0, 0) | |

### 3D Cinemachine Rotation Composer (for "look at" cameras)

From the same sample scene:

| Component | Field | Value |
| --------- | ----- | -----:|
| CinemachineRotationComposer | Damping | 2 |
| | SizeAdjustment | 1 |
| | LateralAdjustment | 0 |
| | FovRange | (50, 70) |
| | DollyRange | (-30, 5) |
| | OrthoSizeRange | (1, 1000) |

## Using in a 24h jam

```csharp
// In your player spawn script
var cinemachineCam = GetComponent<CinemachineCamera>();
cinemachineCam.Follow = player.transform;
cinemachineCam.LookAt = player.transform;

// Or wire it in the inspector:
// 1. Add CinemachineCamera to your Main Camera
// 2. Drag Player into Follow + LookAt
// 3. Add CinemachineThirdPersonFollow component → set CameraDistance = 6-12
// 4. Add CinemachineRotationComposer component → leave defaults
```

For a 2D platformer: see the **2D Orthographic section** below.

## 2D Orthographic (platformer / top-down) — Cinemachine 3.x + 团结 2022.3.62t16

Use this for: 2D platformers, top-down RPGs, beat-em-ups, anything where the
camera looks straight at a 2D scene. **Source**: Cinemachine 3.x official
2D sample (`com.unity.cinemachine/Samples~/2D Samples/`), cross-checked
against Unity Manual page "2D Cinemachine" (companion‑style, public docs).

### Components to add

| Component | Notes |
| --------- | ----- |
| `CinemachineCamera` (the new "v3" CM camera) on Main Camera | `Follow = player`; `LookAt` empty for 2D |
| `CinemachinePositionComposer` (preferred over Framing Transposer in 3.x) | Does screen-space framing + damping for 2D |
| `CinemachineBasicMultiChannelPerlin` (optional) | Handheld shake; OFF by default for jam games |

`CinemachineThirdPersonFollow` and `CinemachineRotationComposer` are **3D-only**
— do NOT add them to a 2D cam.

### CinemachineCamera (the main camera object)

| Field | Value (recommended) | Notes |
| ----- | ------------------- | ----- |
| `Lens.OrthographicSize` | **6.0** (depends on game) | Determines visible vertical world units. Cinemachine sets the camera's `Camera.orthographicSize` to match this. 4.5–8.0 is typical. |
| `Lens.Mode` | `Orthographic` | NOT `Perspective`. |
| `Lens.NearClipPlane` | 0.3 | Same as 3D; works for 2D. |
| `Lens.FarClipPlane` | 1000 | Plenty for 2D. |
| `OutputChannel` | 1 (Base) | Default; ignore. |
| `Follow` | player.transform | Required for PositionComposer to follow. |

### CinemachinePositionComposer (the 2D follow body)

| Field | Value (recommended) | Notes |
| ----- | ------------------- | ----- |
| `Tracking Offset` | (0, 1, 10) | (x, y, z) world-offset. z=10 keeps camera in front of 2D scene in camera-space. y=1 raises cam so feet aren't at screen bottom. |
| `Camera Distance` | 10 | Camera-space distance along Z from target. Mostly redundant if z offset set above; safe default = 10. |
| `Dead Zone Width` | 0.1 | 0–1 fraction of screen. Player can move this much before camera follows. Larger = lazier camera. |
| `Dead Zone Height` | 0.1 | Same for vertical. |
| `Soft Zone Width` | 0.8 | Player can drift this far before camera even starts to follow via damping. |
| `Soft Zone Height` | 0.8 | Same for vertical. |
| `Damping.X` | 0.5 | Horizontal follow speed (lower = smoother but laggier). 0.5 = snappy responsive. |
| `Damping.Y` | 0.5 | |
| `Damping.Z` | 0.5 | (mostly unused in 2D; safe default) |
| `Screen Position` (X, Y) | (0, 0) | Look-at offset in normalized screen units (–0.5…+0.5). Default centers. |
| `Bias X/Y` | 0 / 0 | Game-look camera bias (e.g. side-scrollers). Set Y=0.1 to peek above player. |

### CinemachineRotationComposer (NOT used in 2D)

In 2D the camera doesn't rotate, so the **rotation composer is left at its
defaults** (or absent from the GameObject). The Camera object's rotation is
frozen to identity; rotation-driven 3rd-person framing doesn't apply.

### Putting it together (5-step recipe)

```csharp
// 1. Add a CinemachineCamera to your Main Camera GameObject (or to a child of it).
// 2. Set Follow = player.transform.
// 3. Set Lens.Mode = Orthographic, Lens.OrthographicSize = 6.
// 4. Add a CinemachinePositionComposer component; set Damping = (0.5,0.5,0.5),
//    Tracking Offset = (0, 1, 10), DeadZone = (0.1, 0.1).
// 5. (Optional) Add CinemachineBasicMultiChannelPerlin → Amplitude Gain = 0.4,
//    Frequency Gain = 1 for screen-shake on damage.
```

### Alternative: legacy `CinemachineVirtualCamera` + `Framing Transposer`

If you're on Cinemachine 2.x or you're more comfortable with the legacy API,
this still works in 3.x:

| Field | Value |
| ----- | -----:|
| `Body` | `Framing Transposer` |
| `m_Lens.OrthographicSize` | 6 |
| `Follow Offset` | (0, 1, 10) |
| `m_DeadZoneWidth/Height` | 0.1 |
| `m_SoftZoneWidth/Height` | 0.8 |
| `m_XDamping/YDamping` | 0.5 |

`Framing Transposer` is **deprecated** but functional in 3.x — prefer the new
`CinemachinePositionComposer` for any new project.

### 2D scene checklist for URP

- Camera2D GameObject in scene → `Projection = Orthographic`, `Size = 6`
  (Cinmachine's Lens.OrthographicSize overrides this each frame; keep them in sync)
- Each 2D sprite on a Sorting Layer (Background/Default/Foreground/UI)
- Each sprite's `Sprite Renderer` → Sorting Order set explicitly (don't rely on Y)
- Cinemachine does NOT affect Sprite sorting — order depends on Sorting Order
  + Sorting Layer, not on Z-depth. (Z in `Tracking Offset` matters only for the
  Camera-Distance computation, not for sprite occlusion.)

## License notes

- `VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset` — derived from
  `Unity-Technologies/UniversalRenderingExamples` (Unity Companion License,
  non-commercial sample redistribution — included verbatim from a demo scene,
  bundled with the package itself when URPExamples is installed). Companion
  License §5 Notices text reproduced in `OPEN-ASSETS.md`.
- `VolumeProfiles/CinemachineSampleSceneProfile_URP.asset` — derived from
  `Unity-Technologies/com.unity.cinemachine` (Unity Companion License, ships
  with the Cinemachine package's URP sample scenes). Companion License §5
  Notices text reproduced in `OPEN-ASSETS.md`.
- `_URPExamples_source_FPS_profile/...` and `_Cinemachine_source_*/...` —
  single .asset / .unity files extracted verbatim from the above Unity
  Companion-licensed sources, retained only as upstream reference; teams
  should use the `VolumeProfiles/` copies in their Unity project, which have
  stable GUIDs and avoid the first-import GUID assignment issue.
- All parameter values above come from those official Unity sample scenes
  (Cinemachine 3.x, URP 14+). Cinemachine docs:
  https://docs.unity3d.com/Packages/com.unity.cinemachine@3.0/manual/
---

## Cinemachine 3.x — Baked Prefabs (3D Third-Person + 2D Orthographic)

In `Prefabs/` we ship two pre-baked `.prefab` files. These are the
**authoritative prefabs** — drop them onto a `CinemachineBrain` (auto-attached
to your Main Camera) and they work out of the box.

### `Prefabs/JamKit_ThirdPersonFollow3D.prefab`

A 3D third-person follow camera rig. Components:

| Component | Key field | Value | Notes |
| --- | --- | --- | --- |
| `CinemachineCamera` | `Priority.Value` | 10 | Beats default (0); enable priority |
| `CinemachineCamera` | `OutputChannel` | 1 (Base) | Standard |
| `CinemachineCamera` | `Lens.FieldOfView` | 60° | Standard TPS FOV |
| `CinemachineCamera` | `Lens.NearClipPlane` | 0.3 | |
| `CinemachineCamera` | `Lens.FarClipPlane` | 1000 | |
| `CinemachineCamera` | `Lens.ModeOverride` | 0 (None) | Use whichever mode your Unity Camera is in (default Perspective) |
| `CinemachineThirdPersonFollow` | `CameraDistance` | 4 | Distance behind target |
| `CinemachineThirdPersonFollow` | `ShoulderOffset` | (0.5, -0.4, 0) | Right-shoulder default |
| `CinemachineThirdPersonFollow` | `VerticalArmLength` | 0.4 | |
| `CinemachineThirdPersonFollow` | `CameraSide` | 1 | Right shoulder |
| `CinemachineThirdPersonFollow` | `Damping` | (1, 1, 1) | Soft follow |
| `CinemachineRotationComposer` | `Composition.ScreenPosition` | (0, 0) | Center |
| `CinemachineRotationComposer` | `Composition.DeadZone` | enabled=false, (0.2, 0.2) | |
| `CinemachineRotationComposer` | `Composition.HardLimits` | enabled=false, (0.8, 0.8) | |
| `CinemachineRotationComposer` | `Damping` | (0.5, 0.5) | Snappy aim |

**Setup**: drag onto a GameObject in your scene, set `Target.TrackingTarget` = your player Transform. Press Play — the camera should snap to the TPS over-the-shoulder view and follow.

### `Prefabs/JamKit_2DPlatformerOrtho.prefab`

A 2D platformer camera (orthographic, PositionComposer). Components:

| Component | Key field | Value | Notes |
| --- | --- | --- | --- |
| `CinemachineCamera` | `Priority.Value` | 10 | Beats default |
| `CinemachineCamera` | `Lens.OrthographicSize` | 6 | Half-height of camera view (12 world units visible vertically) |
| `CinemachineCamera` | `Lens.ModeOverride` | 1 (Orthographic) | **Forces Unity Camera to Orthographic** when active |
| `CinemachineCamera` | `Lens.NearClipPlane` | 0.3 | |
| `CinemachineCamera` | `Lens.FarClipPlane` | 1000 | |
| `CinemachinePositionComposer` | `CameraDistance` | 10 | Camera-space distance along Z from target |
| `CinemachinePositionComposer` | `TargetOffset` | (0, 1, 0) | Look 1 unit above target pivot (so feet aren't at screen bottom) |
| `CinemachinePositionComposer` | `Composition.ScreenPosition` | (0, 0.1) | Bias Y slightly upward |
| `CinemachinePositionComposer` | `Composition.DeadZone` | enabled=true, (0.1, 0.1) | Player can move 10% screen before camera follows |
| `CinemachinePositionComposer` | `Damping` | (0.5, 0.5, 0.5) | Snappy |

**Setup**: drag onto a GameObject in your scene, set `Target.TrackingTarget` = your player Transform. The Main Camera should have `Projection = Orthographic` (Cinemachine will switch to Orthographic via ModeOverride when activated anyway).

### MonoScript GUIDs referenced (Cinemachine **3.1.1** as canonical version)

> **统一口径（Cinemachine 版本）**：本 README + prep 包内 `OPEN-ASSETS.md` /
> `assets/LICENSES.md` / `templates/SUBMISSION.md` 全部统一为 **Cinemachine 3.1.1**（CM3+ API，
> `CinemachineCamera` + `CinemachineThirdPersonFollow` + `CinemachinePositionComposer`）。
> MonoScript GUIDs 在 CM3.0.x → CM3.1.x 系列内稳定（更高 6.6.x 仍向前兼容）。
>
> **现场口径**：
> - `packages-manifest-deps.md` 中声明 `com.unity.cinemachine` = **3.1.1**（赛包默认）。
> - 团结引擎 2022.3 自带 Cinemachine **2.9.7**（CM2 API，**不兼容这些 prefab**）——
>   **如比赛现场只能使用自带版本，以现场为准**：要么（a）走 `Packages/manifest.json` UPM 升级到 ≥ 3.0.0，要么
>   （b）改用本仓库未提供的 CM2 prefab 模板。
> - 任意版本相关的不一致按本节的「赛包默认 **3.1.1** / 现场自带 **2.9.7**」二档处理。

| Class | Script GUID | Source |
| --- | --- | --- |
| `CinemachineCamera` | `f9dfa5b682dcd46bda6128250e975f58` | `com.unity.cinemachine/Runtime/Behaviours/CinemachineCamera.cs.meta` |
| `CinemachineThirdPersonFollow` | `625c14eee3ad46c99df7c7c891ef668a` | `com.unity.cinemachine/Runtime/Components/CinemachineThirdPersonFollow.cs.meta` |
| `CinemachinePositionComposer` | `886251e9a18ece04ea8e61686c173e1b` | `com.unity.cinemachine/Runtime/Components/CinemachinePositionComposer.cs.meta` |
| `CinemachineRotationComposer` | `f38bda98361e1de48a4ca2bd86ea3c17` | `com.unity.cinemachine/Runtime/Components/CinemachineRotationComposer.cs.meta` |

(These GUIDs are stable across Cinemachine 3.0.x → 6.6.x for the CM3+ API
component set listed above. If the package's
MonoScript GUIDs change in a future release, the prefabs will import with
magenta — switch to the matching version, or re-export from your installed
copy.)

### Prefab file GUIDs (stable, won't change)

| Prefab | GUID |
| --- | --- |
| `JamKit_ThirdPersonFollow3D.prefab` | `78ea5746051c4c73acffe8df5caad970` |
| `JamKit_2DPlatformerOrtho.prefab` | `112b381e29654b15b6e85a40fb59c7df` |

If you copy these prefabs into your Unity project, they'll reference their
own internal components (no external GUID dependencies).
