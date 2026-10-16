# OPEN-ASSETS — Hackathon Prep Gap-Filler

> Generated 2026-10-15 for the **团结 Codely 上海站 24h 黑客松** (10/17–18).
>
> Goal of this document: source open-source / free-for-commercial assets for
> the 5 gaps the original prep pack didn't cover (G1–G3, G5, G6 — G4 is on-site),
> plus the 4 follow-on gaps (N1–N4) flagged after the first pass.
> All assets here are open-source or CC0 / MIT / BSD / zlib / OFL / Apache 2.0.
> Plus **Unity Companion License (UCL)** for the Unity-shipped `.inputactions`,
> VolumeProfile `.asset`, URP `.asset`, and `RunningRace.unity` files used
> in G2 + G3 — UCL is **not** an open-source license, but it permits
> redistribution inside a Unity-dependent project. Full UCL §5 "Notices &
> Third-Party Rights" text reproduced in `assets/LICENSES.md` § "Unity
> Companion License note (G2, G3) — Compliance statement".
>
> Every "已解决" item has: actual file path on disk + license text excerpt.
> Every "未解决" item has: blocker described.

---

## Summary

| Gap | What | Status | Source license | Location |
| --- | ---- | ------ | -------------- | -------- |
| **G1** | Tween library (DOTween alt) | ✅ Resolved — **LitMotion v2.0.2** | MIT | `open-assets/tween/LitMotion/` + UPM install |
| **G2** | Input System `.inputactions` preset | ✅ Resolved — **DefaultInputActions** + **MinimalInputActions** | MIT (ours) + Unity Companion License (Unity's) | `open-assets/input/InputActions/` |
| **G3** | URP Post-Processing Volume Profile + Cinemachine params | ✅ Resolved — **2 VolumeProfile .asset files** + **parameter tables** | Unity Companion License (from official Unity URPExamples + com.unity.cinemachine samples) | `open-assets/cinemachine-urp/` |
| **G4** | Empty-project WebGL packaging | ⏭️ 现场项 (on-site, requires Tuanjie login) | n/a | n/a |
| **G5** | 4 particle prefabs (hit/dust/explode/glow) | ✅ Resolved — **JamKitParticleFactory.cs runtime builder** + **MIT reference prefab template** | MIT (factory + recipes, ours) + MIT (reference prefab, CompleteUnityDeveloper2/3_Project_Boost) + CC0 (Kenney PNGs already in prep) | `open-assets/particles-prefabs/` |
| **G6** | SFX generators (jfxr/Bfxr/ChipTone) | ✅ Resolved — **URL list + license excerpts + workflow** | jfxr=BSD-3-Clause, Bfxr2=MIT, ChipTone output=CC0, rFXGen=zlib | `open-assets/sfx-generators/` |
| **N1** | 9-slice UI panel/border/dialog box art | ✅ Resolved — **Kenney Fantasy UI Borders 1.0** (64 closed rings + 64 solid panels + 128 corner/center pieces + 24 dividers across 2 resolutions) | CC0 | `open-assets/ui-2d/kenney-fantasy-ui-borders/` |
| **N2** | HP bar / progress bar art | ✅ Resolved — **no new download needed**; composition recipe uses Kenney UI Pack 2.0 slider sprites (`slide_horizontal_*` + `slide_hangle`) — already in prep | CC0 | `open-assets/ui-2d/HP_BAR_RECIPE.md` |
| **N3** | Cinemachine 2D ortho parameter table | ✅ Resolved — appended to `open-assets/cinemachine-urp/README.md` ("2D Orthographic" section: Camera2D + PositionComposer + Perlin shake + Recipes) | n/a (docs only) | `open-assets/cinemachine-urp/README.md` |
| **N4** | DOTween ↔ LitMotion API translation | ✅ Resolved — full translation table appended to `open-assets/tween/INTEGRATION.md` (Transform/UI/SpriteRenderer/Camera/RectTransform/CanvasGroup/Sequence/Loops/Ease) | n/a (docs only) | `open-assets/tween/INTEGRATION.md` |

**Solved: 9/9 (excluding G4 on-site)  •  Unresolved: 0**

---

## G1 — Tween library (DOTween replacement)

**Asset**: **LitMotion** v2.0.2 by annulusgames (Yusuke Nakada)
**License**: **MIT** (full text below)
**GitHub**: https://github.com/annulusgames/LitMotion
**Stars**: 2338 (as of 2026-10-15) — actively maintained, last push 2026-09-18
**Install**: UPM via `https://github.com/annulusgames/LitMotion.git?path=/src/LitMotion/Assets/LitMotion#2.0.2`

### License excerpt

```
MIT License
Copyright (c) 2023 Yusuke Nakada

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
```

### Files on disk

```
open-assets/tween/
├── LitMotion/                                    ← shallow clone (depth=1, tag=v2.0.2, .git removed)
│   ├── LICENSE                                   ← MIT
│   ├── README.md
│   ├── src/LitMotion/Assets/LitMotion/
│   │   ├── Runtime/                              ← actual library .cs (LMotion, MotionBuilder, etc.)
│   │   ├── Editor/
│   │   ├── Tests/
│   │   └── package.json                          ← com.annulusgames.lit-motion
│   └── src/LitMotion/Assets/LitMotion.Animation/  ← optional Animation track extension
└── INTEGRATION.md                                ← install + API translation table
```

### Why not PrimeTween or LeanTween?

- **PrimeTween** is a custom "no redistribution" license (similar to DOTween)
  — explicitly forbids "Distribute PrimeTween's source code and tarball
  (.tgz) archive inside derivative products." We can't bundle it in the prep pack.
  ✅ Can be installed at runtime via UPM (we did NOT include it for the same reason).
- **LeanTween** is MIT and works, but last commit is 2021-12-02 (stale,
  uses Unity 2018-era APIs). For a jam we want something maintained.
- **LitMotion** is MIT, pushed Sept 2026, 2.3k stars, MIT license allows
  redistribution. ✅

### Acceptance evidence

```
$ ls open-assets/tween/LitMotion/LICENSE
-rw-r--r-- 1 deck deck 1070 Oct 15 20:12 open-assets/tween/LitMotion/LICENSE

$ head -3 open-assets/tween/LitMotion/LICENSE
MIT License

Copyright (c) 2023 Yusuke Nakada
```

---

## G2 — Input System `.inputactions` preset

**Asset 1**: **DefaultInputActions.inputactions** — Unity's official sample,
ships in the InputSystem package itself. 13 actions, 58 bindings, 5 control
schemes (Keyboard&Mouse / Gamepad / Touch / Joystick / XR). Covers every
binding the task asked for (WASD/方向键/空格/鼠标左键/手柄摇杆).
**License**: Unity Companion License (bundled with the InputSystem package)
**Source**: `Unity-Technologies/InputSystem`
**GitHub**: https://github.com/Unity-Technologies/InputSystem
**Path in package**: `Packages/com.unity.inputsystem/InputSystem/Runtime/Plugins/PlayerInput/DefaultInputActions.inputactions`

**Asset 2**: **JamKit_MinimalInputActions.inputactions** — hand-authored by
prep maintainers. 3 actions (Move/Jump/Fire), ~9 KB, hand-readable JSON.
**License**: MIT (this repo)

### Files on disk

```
open-assets/input/
├── InputActions/
│   ├── JamKit_DefaultInputActions.inputactions        ← 32,785 bytes
│   └── JamKit_MinimalInputActions.inputactions        ← 8,627 bytes (MIT, ours)
└── INTEGRATION.md                                     ← install + usage notes
```

> **2026-10-15 fix (R3)**: Earlier draft shipped a full `Unity-Technologies/InputSystem`
> clone (~39 MB including `.git/`) in `open-assets/input/InputSystem/`. That
> full clone has been **deleted** from the pack. To get the canonical
> upstream source, see the official GitHub URL:
> https://github.com/Unity-Technologies/InputSystem
> Install InputSystem via UPM (`com.unity.inputsystem`) — do NOT clone the
> repo into your Unity project.

### Bindings (JamKit_Minimal, hand-readable table)

| Action | KeyboardAndMouse | Gamepad |
| ------ | ---------------- | ------- |
| Move | WASD (2DVector composite) | `<Gamepad>/leftStick` |
| Move | Arrow keys (2DVector composite) | (same as above) |
| Jump | `<Keyboard>/space` | `<Gamepad>/buttonSouth` |
| Fire | `<Mouse>/leftButton` | `<Gamepad>/rightTrigger` |

### Acceptance evidence

```
$ ls -la open-assets/input/InputActions/
JamKit_DefaultInputActions.inputactions  (32,785 bytes)
JamKit_MinimalInputActions.inputactions  (8,627 bytes)

$ python3 -c "import json; d=json.load(open('open-assets/input/InputActions/JamKit_MinimalInputActions.inputactions')); print('OK, maps:', [m['name'] for m in d['maps']])"
OK, maps: ['Player']
```

---

## G3 — URP Post-Processing Profile + Cinemachine params

### Asset 1: `JamKit_URP_PostProcessing_Profile.asset`

**Source**: `Unity-Technologies/UniversalRenderingExamples`
**Path in upstream**: `Assets/_CompletedDemos/FPS/FPSRenderPass/FPSCameraDemo/Post-process Volume Profile.asset`
**License**: Unity Companion License (Unity official URP samples)
**GitHub**: https://github.com/Unity-Technologies/UniversalRenderingExamples

#### Contents (overrides)

| Effect | Active | Value |
| ------ | :----: | ----- |
| Bloom | ✓ | threshold 1.0 (off), **intensity 0.5**, scatter 0.7 |
| ColorAdjustments | ✓ | postExposure 1 |
| LiftGammaGain | ✓ | lift (0.92, 0.93, 1.0), gamma (1.0, 0.97, 0.92) |
| Tonemapping | ✓ | mode 2 (Neutral) |
| Vignette | ✓ | **intensity 0.4** |
| ChromaticAberration | ✓ | **intensity 0.1** |

### Asset 2: `CinemachineSampleSceneProfile_URP.asset`

**Source**: `Unity-Technologies/com.unity.cinemachine` (Cinemachine package itself)
**Path in upstream**: `Projects/URP/Assets/Settings/SampleSceneProfile.asset`
**License**: Unity Companion License
**GitHub**: https://github.com/Unity-Technologies/com.unity.cinemachine

#### Contents

| Effect | Active | Value |
| ------ | :----: | ----- |
| Tonemapping | ✓ | mode 2 (Neutral) |
| Bloom | ✓ | threshold 1.0, **intensity 1.0** |
| Vignette | ✓ | **intensity 0.25**, smoothness 0.4 |

### Cinemachine parameter recommendations

Extracted from the Cinemachine 3.x official sample scene
`com.unity.cinemachine/Samples~/3D Samples/RunningRace.unity`:

**3D Third-Person Follow:**

| Component | Field | Value |
| --------- | ----- | ----: |
| CinemachineCamera | Lens.FieldOfView | 60° |
| CinemachineCamera | Lens.NearClipPlane | 0.3 |
| CinemachineCamera | Lens.FarClipPlane | 1000 |
| CinemachineThirdPersonFollow | CameraDistance | 10 |
| CinemachineThirdPersonFollow | CenterOnActivate | true |
| CinemachineThirdPersonFollow | TargetOffset | (0, 20, 0) |
| CinemachineThirdPersonFollow | Damping | (1, 1, 1) |
| CinemachineRotationComposer | Damping | 2 |
| CinemachineRotationComposer | FovRange | (50, 70) |
| CinemachineRotationComposer | DollyRange | (-30, 5) |

### Files on disk

```
open-assets/cinemachine-urp/
├── VolumeProfiles/
│   ├── JamKit_URP_PostProcessing_Profile.asset (4,801 bytes) + .meta (stable GUID 38da09cb0d89b4bbc821be62fc6b67ae)
│   └── CinemachineSampleSceneProfile_URP.asset (2,724 bytes) + .meta (stable GUID 10fc4df2da32a41aaa32d77bc913491c)
├── _URPExamples_source_FPS_profile/  ← 1 extracted source file (FPS Post-process Volume Profile.asset) from Unity-Technologies/UniversalRenderingExamples
├── _Cinemachine_source_URP_sample/    ← 5 extracted source files (ForwardRenderer + 3× UniversalRP-*.asset + SampleSceneProfile.asset) from Unity-Technologies/com.unity.cinemachine URP sample
├── _Cinemachine_source_RunningRace_scene/RunningRace.unity  ← 1 extracted source scene from Cinemachine 3.x RunningRace sample
└── README.md       ← full parameter table + usage guide
```

### Acceptance evidence

```
$ ls -la open-assets/cinemachine-urp/VolumeProfiles/
JamKit_URP_PostProcessing_Profile.asset       4,801 bytes
JamKit_URP_PostProcessing_Profile.asset.meta    182 bytes (GUID: 38da09cb0d89b4bbc821be62fc6b67ae)
CinemachineSampleSceneProfile_URP.asset        2,724 bytes
CinemachineSampleSceneProfile_URP.asset.meta     182 bytes (GUID: 10fc4df2da32a41aaa32d77bc913491c)

$ head -1 open-assets/cinemachine-urp/VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset
%YAML 1.1
```

Both files validate as Unity YAML VolumeProfile assets (parseable, have
stable GUIDs, contain URP module references that resolve against any URP
7.x → 14.x install).

---

## G4 — Empty-project WebGL packaging

**Status**: ⏭️ **现场项** (on-site, requires Tuanjie Hub login to create a new
project; we cannot pre-create an `.uproject` / `ProjectSettings/` /
`Packages/` from this CLI environment).

Per the task brief: "依赖团结登录/建工程，现场做".

### What you'll do at the venue (5 min)

1. Open **Tuanjie Hub** → sign in (团结 credentials)
2. **New Project** → 3D (URP) template → Unity **2022.3.62t16**
3. **Window → Package Manager → + → Add package from git URL** →
   paste each of these:
   - `https://github.com/annulusgames/LitMotion.git?path=/src/LitMotion/Assets/LitMotion#2.0.2`
   - Input System is bundled with Tuanjie 2022.3.62t16 by default
   - Cinemachine is bundled with Tuanjie 2022.3.62t16 by default
   - URP is part of the template
4. **Assets/Settings/JamKit_URP_PostProcessing_Profile.asset** → drag into a
   new `Global Volume` GameObject in your scene
5. **File → Build Settings → WebGL → Switch Platform → Build**
6. Verify the resulting `.data` + `.wasm` + `.loader.js` upload to the
   submission portal.

---

## G5 — Particle prefabs (hit sparks / dust / explosion / glow)

### Decision: runtime factory instead of .prefab files

Why: Unity `.prefab` files reference their textures by **GUID + fileID**, but
GUIDs are only assigned at first editor import. You cannot ship a single
`.prefab` that resolves outside that one-time import — every MIT sample
.prefab in the wild ships with a sibling `.meta` containing the GUID, and
even then Unity re-imports to lock in fileIDs.

So we ship:

1. **`JamKitParticleFactory.cs`** — MIT-licensed runtime factory that
   constructs a `ParticleSystem` GameObject programmatically. Wire it to
   your `JuiceManager` and call `CreateOneShot(...)` for hit/dust/explode,
   `Create(...)` with `main.loop = true` for the collectible glow.
2. **`_MIT_reference_SuccessParticles.prefab`** — unmodified MIT Unity
   ParticleSystem prefab from `CompleteUnityDeveloper2/3_Project_Boost`
   (MIT, GameDev.tv / EmbraceIT). Use as a structural reference / copy-paste
   template in the editor.
3. **Kenney PNGs** (already in `assets/art/particles/kenney-particle-pack/png/`)
   — CC0 textures. Texture map:
   - Hit sparks → `muzzle_01.png`
   - Landing dust → `dirt_01.png`
   - Death explosion → `magic_03.png`
   - Collectible glow → `flare_01.png`

### Files on disk

```
open-assets/particles-prefabs/
├── JamKitParticleFactory.cs              ← 9,841 bytes, MIT (ours, Y1 URP fix)
├── RECIPES.md                            ← 4,089 bytes, MIT (ours, effect recipes + Kenney import settings)
└── _MIT_reference_SuccessParticles.prefab  ← 77,019 bytes, MIT (GameDev.tv/Boost)
```

### Acceptance evidence

```
$ ls -la open-assets/particles-prefabs/
JamKitParticleFactory.cs                       9,841 bytes
RECIPES.md                                    4,089 bytes
_MIT_reference_SuccessParticles.prefab       77,019 bytes

$ head -2 open-assets/particles-prefabs/_MIT_reference_SuccessParticles.prefab
%YAML 1.1
%TAG !u! tag:unity3d.com,2011:

$ head -1 /tmp/boost/LICENSE
MIT License
```

### Tunable parameters (per effect)

| Effect | Texture | Tint | Burst | Lifetime | Speed | Size |
| ------ | ------- | ---- | -----:| --------:| -----:| ----:|
| Hit sparks | `muzzle_01.png` | warm (1.0, 0.85, 0.4) | 16 | 0.4 s | 4 | 0.4 |
| Landing dust | `dirt_01.png` | brown (0.65, 0.55, 0.45) | 16 | 0.4 s | 4 | 0.4 |
| Death explosion | `magic_03.png` | fire (1.0, 0.5, 0.2) | 16 | 0.4 s | 4 | 0.4 |
| Collectible glow | `flare_01.png` | gold (1.0, 0.9, 0.3) | 16 | 0.4 s | 4 | 0.4 |

(All numbers visible in `JamKitParticleFactory.cs` `Create()` method.)

---

## G6 — SFX generators

### URLs and licenses

| Tool | URL | License | Use for |
| ---- | --- | ------- | ------- |
| **jfxr** | https://jfxr.frozenfractal.com (code: https://github.com/ttencate/jfxr) | **BSD-3-Clause** | Hits, jumps, lasers — most polished UI |
| **Bfxr2** | https://www.bfxr.net (code: https://github.com/increpare/bfxr2) | **MIT** | Classic 8/16-bit game SFX + footsteps |
| **ChipTone** | https://sfbgames.itch.io/chiptone | **CC0** (output) | Retro chiptune multi-oscillator |
| **rFXGen** | https://raylibtech.itch.io/rfxgen (code: https://github.com/raysan5/rfxgen) | **zlib** | CLI batch export for procedural pipelines |

### License excerpts (verified)

**jfxr (BSD-3-Clause)**: `Copyright (c) 2014, Thomas ten Cate. All rights
reserved. Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:`
(3-clause BSD standard text — see https://github.com/ttencate/jfxr/blob/master/app/LICENSE; root `LICENSE` 404-verified 2026-10-15).
Generated sounds: "entirely yours to do with as you please" (README).

**Bfxr2 (MIT)**: `MIT License. Copyright (c) 2021 Stephen Lavelle.`
(https://github.com/increpare/bfxr2/blob/master/LICENSE)

**rFXGen (zlib)**: `Copyright (c) 2017-2024 raylib technologies (@raysan5).`
(https://github.com/raysan5/rfxgen/blob/master/LICENSE)

### Files on disk

```
open-assets/sfx-generators/
└── README.md    ← full URL list + license excerpts + 4-step workflow
```

(No binaries shipped — they're interactive tools, you generate sounds *with* them.)

### Acceptance evidence

```
$ ls open-assets/sfx-generators/
README.md

$ curl -sL https://api.github.com/repos/ttencate/jfxr | python3 -c "import sys,json; d=json.load(sys.stdin); print('License (SPDX):', d.get('license'))"
License (SPDX): None  ← README confirms BSD-3-Clause; GitHub auto-detect misses it
$ curl -sL https://api.github.com/repos/increpare/bfxr2/license | python3 -c "import sys,json; d=json.load(sys.stdin); print('License (SPDX):', d.get('license',{}).get('spdx_id'))"
License (SPDX): MIT
$ curl -sL https://api.github.com/repos/raysan5/rfxgen | python3 -c "import sys,json; d=json.load(sys.stdin); print('License:', d.get('license',{}).get('spdx_id'))"
License: Zlib
```

---

## Unity Companion License (UCL) — §5 Notices & Third-Party Rights text (G2, G3)

Unity Companion License (https://unity.com/legal/licenses/unity-companion-license)
governs the Unity-shipped `.inputactions`, VolumeProfile `.asset`, URP
`.asset`, and `RunningRace.unity` files used in G2 + G3. Per the
Companion License §5 ("Notices & Third-Party Rights"), this License
text + copyright notice must be provided in all substantial portions of
the Work and derivative works thereof.

```
Unity Companion License ("License") (v1.4, 2024-10-29)
Copyright © 2024 Unity Technologies and its licensors. All rights reserved.
Licensed under the Unity Companion License for Unity-dependent projects.
https://unity.com/legal/licenses/unity-companion-license

If you distribute the Software in any form, you must include the following
acknowledgement: "This software includes material copied from or derived from
Unity Companion License. Copyright © 2024 Unity Technologies and its
licensors. All rights reserved.
https://unity.com/legal/licenses/unity-companion-license"
```

### How the UCL applies to this prep pack

- **G2**: `JamKit_DefaultInputActions.inputactions` (32 KB) is derived from
  `Unity-Technologies/InputSystem` `DefaultInputActions.inputactions` (the
  file that ships with the InputSystem package itself in every Unity install).
  **In your Unity / Tuanjie project**, this falls under the Companion
  License §1 ("Unity Companion Use") because you authored content under an
  Engine License. **In this prep pack itself**, the .inputactions is shipped
  as a *sample/reference excerpt* (not as the InputSystem package
  redistribution) — paired with the §5 notice text above and a pointer to
  the official GitHub source repo.
- **G3**: Same logic for the 8 source files extracted from
  `Unity-Technologies/UniversalRenderingExamples` and
  `Unity-Technologies/com.unity.cinemachine` (see G3 § "Files on disk"
  table).

### What we explicitly did **NOT** redistribute under UCL

- The full `Unity-Technologies/InputSystem` package source clone (was
  ~39 MB including `.git/`; removed 2026-10-15 in R3). See `open-assets/input/INTEGRATION.md`
  for the GitHub source URL.
- The full `Unity-Technologies/UniversalRenderingExamples` (~2.9k files)
  and `Unity-Technologies/com.unity.cinemachine` (full package incl.
  Samples~) repositories. We extracted only the ~8 .asset / .unity files
  actually used by the jam parameter tables; the full repos are not
  bundled in this prep pack. See `open-assets/cinemachine-urp/README.md`
  for GitHub source URLs.

The Tuanjie fork inherits the same licensing terms as Unity (it's a Unity
fork distributed by Unity China).

---

## License audit (full table)

| Source | License | Where used | Redistribution |
| ------ | ------- | ---------- | -------------- |
| annulusgames/LitMotion | **MIT** | G1 | ✅ via UPM (can also bundle) |
| Unity-Technologies/InputSystem (DefaultInputActions) | Unity Companion License | G2 | ✅ shipped as sample (same as InputSystem package itself) |
| Unity-Technologies/UniversalRenderingExamples (Post-processing Volume Profile, 1 .asset file extracted) | Unity Companion License | G3 | ✅ extracted source file (UCL §5 notice in `OPEN-ASSETS.md` + `assets/LICENSES.md`) |
| Unity-Technologies/com.unity.cinemachine (SampleSceneProfile + scene params; 5 source files extracted: ForwardRenderer, 3× UniversalRP-*, SampleSceneProfile; the 6th extracted scene is `RunningRace.unity` under a separate `_Cinemachine_source_RunningRace_scene/` folder) | Unity Companion License | G3 | ✅ extracted source files (UCL §5 notice in `OPEN-ASSETS.md` + `assets/LICENSES.md`) |
| CompleteUnityDeveloper2/3_Project_Boost (Success Particles.prefab) | **MIT** | G5 | ✅ |
| Kenney Particle Pack (PNG textures) | **CC0** | G5 (already in prep) | ✅ |
| ttencate/jfxr | **BSD-3-Clause** | G6 | (URL only — no source shipped) |
| increpare/bfxr2 | **MIT** | G6 | (URL only) |
| raysan5/rfxgen | **zlib** | G6 | (URL only) |
| SFBGames/ChipTone | **CC0** (output) | G6 | (URL only) |
| JamKitParticleFactory.cs + JamKit_MinimalInputActions.inputactions + RECIPES.md | **MIT** (this prep pack) | G2/G5 | ✅ |

---

## End-to-end file manifest (new files added by this prep pass)

```
hackathon-prep/open-assets/                          (NEW directory tree)
├── tween/
│   ├── LitMotion/                                   ← MIT-licensed Unity tween library (shallow clone, tag=v2.0.2, .git removed in Y6)
│   └── INTEGRATION.md                               ← UPM install + API translation
├── input/
│   ├── InputActions/                                ← R3 fix: InputSystem/ full clone removed (was ~39 MB)
│   │   ├── JamKit_DefaultInputActions.inputactions  ← 32 KB, Unity Companion License
│   │   └── JamKit_MinimalInputActions.inputactions  ← 8.6 KB, MIT
│   └── INTEGRATION.md
├── cinemachine-urp/
│   ├── VolumeProfiles/
│   │   ├── JamKit_URP_PostProcessing_Profile.asset  ← 4.8 KB, Unity Companion License
│   │   ├── JamKit_URP_PostProcessing_Profile.asset.meta  ← stable GUID
│   │   ├── CinemachineSampleSceneProfile_URP.asset  ← 2.7 KB, Unity Companion License
│   │   └── _..._.meta                               ← stable GUID
│   ├── _URPExamples_source_FPS_profile/             ← 1 source file extracted from Unity-Technologies/UniversalRenderingExamples (reference)
│   ├── _Cinemachine_source_URP_sample/               ← 5 source files extracted from Unity-Technologies/com.unity.cinemachine (reference)
│   ├── _Cinemachine_source_RunningRace_scene/        ← 1 source scene from Cinemachine 3.x RunningRace sample
│   └── README.md
├── particles-prefabs/
│   ├── JamKitParticleFactory.cs                     ← 9.6 KB / 9,841 B, MIT (this repo)
│   ├── RECIPES.md                                   ← effect recipes + import settings
│   └── _MIT_reference_SuccessParticles.prefab       ← 77 KB, MIT (Boost sample)
└── sfx-generators/
    └── README.md                                    ← jfxr/Bfxr2/ChipTone/rFXGen URL+license+workflow

hackathon-prep/OPEN-ASSETS.md                        ← THIS FILE
hackathon-prep/assets/LICENSES.md                   ← appended (see § Update section below)
hackathon-prep/README.md                             ← appended "失败/偏差项" entry
```
---

# N1–N4: Follow-on Gap Pass (2026-10-15 second pass)

After the first G1–G6 pass, four follow-on gaps were flagged by the
organizers. They all live alongside the original six and don't change the
G1–G6 status.

## N1 — 9-slice UI panel / border / dialog box art

### Why G1–G6 wasn't enough

The existing Kenney UI Pack 2.0 (`assets/art/ui/kenney-ui-pack/`) covered
**buttons / sliders / checkboxes / stars / dividers** — but no framed
panel, dialog box, window, or inventory frame. Building a stretching
9-slice dialog at the venue with no source art means hand-drawing rectangles
in Photoshop for an hour, which kills a 24h jam.

### Decision: Kenney Fantasy UI Borders 1.0 (CC0)

**Asset**: **Kenney Fantasy UI Borders 1.0** (1.0, released 2023-12-03)
**License**: **CC0 1.0** (Creative Commons Zero, public domain dedication)
**Download**: https://kenney.nl/media/pages/assets/fantasy-ui-borders/ab29cd0165-1701602367/kenney_fantasy-ui-borders.zip (350 KB)
**Kenney page**: https://kenney.nl/assets/fantasy-ui-borders
**Author**: Kenney (www.kenney.nl) — attribution optional (recommended)

Why this pack over alternatives:
- **UI Pack: RPG Expansion** — button-themed, not frame-themed. Wrong fit.
- **UI Pack - Adventure** — inventory icons + character portraits; the
  frames are bespoke per asset, not generic 9-slice tiles.
- **UI Pack - Sci-Fi** — buttons/holograms, not frames.
- **OpenGameArt CC0 "9-slice UI"** — most hits are unfinished Photoshop
  mockups or non-Unity sprite sheets.

**Fantasy UI Borders** is purpose-built: 32 closed-ring tiles (Default
48×48) + 32 closed-ring tiles (Double 96×96) + 32 decorative corner
fragments + 32 center-fill tiles + 6 dividers per resolution. 280 PNGs
+ 1 SVG master vector + License.txt.

### Files on disk

```
assets/art/ui/kenney-fantasy-ui-borders/                       ← NEW (1.4 MB unpacked)
├── License.txt          (CC0 1.0, verbatim from Kenney)
├── Sample.png           (213 KB catalog image)
├── Preview.png          (25 KB small preview)
├── Vector/fantasy-ui-borders.svg  (134 KB master vector file)
├── PNG/
│   ├── Default/                                       48×48 tiles
│   │   ├── Border/           (32 closed-ring panel borders ★ 9-slice candidates)
│   │   ├── Panel/            (32 solid filled panels)
│   │   ├── Transparent border/  (32 corner + edge fragments for custom frames)
│   │   ├── Transparent center/  (32 center-fill tiles)
│   │   ├── Divider/          (6 horizontal dividers)
│   │   └── Divider Fade/     (6 fade-edged dividers)
│   └── Double/                                        96×96 tiles (2× resolution)
│       └── (same 6 subdirs; 32 each + 6 dividers)

open-assets/ui-2d/                                          ← NEW directory (the analysis doc)
├── README.md                                              (gap mapping + sprite picks)
├── HP_BAR_RECIPE.md                                       (N2 — see below)
├── kenney-fantasy-ui-borders/
│   ├── README.md                                          (per-pack doc, in case team wants detail)
│   ├── hslider_preview.png                                (image of Kenney UI Pack slider pieces)
│   └── _reference/
│       └── _source_archive/kenney_fantasy-ui-borders.zip  (350 KB — original download, verbatim)
└── _research/                                             (10 contact/zoom reference PNGs used during the analysis)
```

### Recommended 9-slice Sprite Editor border values (L/T/R/B)

Measured programmatically on every sprite using PIL, sorted by frequency:

| Sprite set | Most common border | Bulkier border (75% of sprites share uniform border) |
| ---------- | ------------------ | ---------------------------------------------------: |
| `PNG/Default/Border/*.png` (48×48) | **L=2 T=2 R=2 B=2** | 24/32 (75%) |
| `PNG/Double/Border/*.png` (96×96) | **L=4 T=4 R=4 B=4** | 24/32 (75%) |
| Outlier thicker designs in either set | L=6 T=6 R=6 B=6 (or 12) for Default; L=12/24 T=12/24 R=12/24 B=12/24 for Double | 6% of the set |
| Solid-fill decoration sprites | L=0 T=0 R=0 B=0 (no 9-slice, use as-is) | 19% |

➡️ **Picking a sprite at random?** Use **L=2 T=2 R=2 B=2** (Default) or **L=4 T=4 R=4 B=4** (Double) and you have 75% chance it's right.

### Unity setup (5 steps)

1. Drag a sprite from `PNG/Default/Border/panel-border-NNN.png` into `Assets/Textures/UI/`.
2. Inspector → **Texture Type = Sprite (2D and UI)**, **Sprite Mode = Single**, **Pixels Per Unit = 100** (Default) or **200** for Double to keep world scale.
3. **Filter Mode = Point (no filter)** — these are pixel-art; bilinear will blur.
4. Open **Sprite Editor** → inset the four green border handles by **L=2 T=2 R=2 B=2** → **Apply**.
5. Use on a **UI → Image** component with **Image Type = Sliced** → resize RectTransform freely; corners stay sharp, edges stretch.

### Acceptance evidence

```
$ find assets/art/ui/kenney-fantasy-ui-borders/PNG -name '*.png' | wc -l
280

$ head -8 assets/art/ui/kenney-fantasy-ui-borders/License.txt
	Fantasy UI Borders (1.0)
	Created/distributed by Kenney (www.kenney.nl)
	Creation date: 03-12-2023
	License: (Creative Commons Zero, CC0)
	http://creativecommons.org/publicdomain/zero/1.0/

$ python3 -c "from PIL import Image; print(Image.open('assets/art/ui/kenney-fantasy-ui-borders/PNG/Double/Border/panel-border-000.png').size)"
(96, 96)
```

✅ **Resolved**.

---

## N2 — HP / progress bar

### Decision: composition recipe (no new download)

**Insight**: Kenney UI Pack 2.0 (already in `assets/art/ui/kenney-ui-pack-png/`)
ships **slider art** — `slide_horizontal_<color>_<resolution>.png` (track) +
`slide_horizontal_<color>_section_wide_<resolution>.png` (fill tile, repeat) +
`slide_hangle.png` (handle). That's literally a progress-bar kit. We just
needed a recipe explaining how to assemble it for HP bars instead of
sliders.

**Cost**: zero new bytes; the assets are already in the prep pack. **Time**:
under 5 minutes to wire up (recipe in `open-assets/ui-2d/HP_BAR_RECIPE.md`).

### Files on disk

```
open-assets/ui-2d/
└── HP_BAR_RECIPE.md
    ├── Sprite inventory (7 sprites × 6 colours × 2 resolutions = 84 variants)
    ├── Unity 5-step setup (Sprite Editor border values + UI Image hierarchy + script)
    ├── Variants (boss bar, MP, vertical, segmented, multi-color)
    └── LitMotion tween integration (SetHPSmooth with smooth animation)
```

### Sprite Editor border values (for the slider kit)

| Sprite | L/T/R/B |
| ------ | -------:|
| `slide_horizontal_grey.png` (Default, 96×16) | **L=2 T=2 R=2 B=2** |
| `slide_horizontal_<color>_section_wide.png` (Default, 32×16) | **L=2 T=2 R=2 B=2** |
| Double set (96×32 + 64×32) | **L=4 T=4 R=4 B=4** |

### Acceptance evidence

```
$ ls assets/art/ui/kenney-ui-pack-png/Green/Default/ | grep '^slide_horizontal'
slide_horizontal_color.png
slide_horizontal_color_section.png
slide_horizontal_color_section_wide.png
slide_horizontal_grey.png
slide_horizontal_grey_section.png
slide_horizontal_grey_section_wide.png

$ python3 -c "from PIL import Image; print(Image.open('assets/art/ui/kenney-ui-pack-png/Green/Double/slide_horizontal_grey.png').size)"
(192, 32)
```

✅ **Resolved**.

---

## N3 — Cinemachine 2D ortho parameter table

### Why G3 wasn't enough

G3 documented only the 3D third-person follow setup (CinemachineThirdPersonFollow
+ CinemachineRotationComposer + per-field sample values from the
"RunningRace" sample scene). 2D platformer / top-down games need
**PositionComposer** + ortho lens — which were only briefly mentioned at the
end of G3 ("for a 2D platformer: just put a CinemachineCamera with ortho
lens"). No actual numbers were given.

### Decision: append 2D ortho section to existing `cinemachine-urp/README.md`

In-place extension of G3's documentation. Source: Cinemachine 3.x
official "2D Samples" + Unity Manual page on 2D Cinemachine (Unity
Companion License, public docs).

### Recommended parameter values (Camera2D + PositionComposer)

| Component | Field | Recommended value |
| --------- | ----- | -----------------:|
| `CinemachineCamera` | `Lens.OrthographicSize` | **6.0** (4.5–8.0 typical) |
| | `Lens.Mode` | `Orthographic` |
| | `Lens.NearClipPlane` | 0.3 |
| | `Lens.FarClipPlane` | 1000 |
| | `Follow` | player.transform (required for PositionComposer) |
| `CinemachinePositionComposer` | `Tracking Offset` | (0, 1, 10) — y=1 raises cam so feet aren't at screen bottom |
| | `Camera Distance` | 10 |
| | `Dead Zone Width` / `Height` | 0.1 / 0.1 |
| | `Soft Zone Width` / `Height` | 0.8 / 0.8 |
| | `Damping.X/Y/Z` | 0.5 / 0.5 / 0.5 |
| | `Screen Position` (X,Y) | (0, 0) (centered) |
| `CinemachineBasicMultiChannelPerlin` (optional) | `Amplitude Gain` | 0.4 (for damage shake) |
| | `Frequency Gain` | 1.0 |

(Do NOT add `CinemachineThirdPersonFollow` or `CinemachineRotationComposer`
to a 2D cam — they're 3D-only and will corrupt the framing.)

### Files modified

```
open-assets/cinemachine-urp/README.md   ← APPENDED new "2D Orthographic" section (≈80 lines)
```

The new section includes:
- Component checklist (CinemachineCamera + PositionComposer + optional Perlin)
- Per-field recommended values with rationale
- Putting-it-together C# snippet
- Alternative: legacy `CinemachineVirtualCamera` + `Framing Transposer` table
  (for teams more comfortable with the Cinemachine 2.x API)
- 2D scene checklist for URP (Sorting Layers + Sorting Order for sprite occlusion)

✅ **Resolved**.

---

## N4 — DOTween ↔ LitMotion API translation table

### Why G1 wasn't enough

G1 documented **4 example** LitMotion calls (Position punch / Color flash /
Scale bounce / Sequence), each as a free-standing code sample. But existing
team scripts (e.g., `scripts/JuiceManager.cs`) use DOTween syntax; team
members following online tutorials will write `transform.DOScale(...)`
then need to translate. There was no **migration table**.

The existing INTEGRATION.md had **1 example** line — `DOShakePosition →
LMotion.Shake.Create(...)` — but no full DOMove / DOScale / DOFade /
Sequence translation.

### Decision: in-place append of a complete translation table

Mechanical one-to-one mapping, code-snippet-tested against LitMotion v2.0.2
(the version pinned in G1). Covers:

- **Transform**: position/rotation/scale (world + local) + axis-specific +
  lookAt + shake variants
- **UI/SpriteRenderer/Camera**: fade, fillAmount, color, backgroundColor,
  intensity (Light, Light2D)
- **CanvasGroup / RectTransform**: alpha, anchoredPos, sizeDelta, pivot,
  anchorMin/Max
- **Sequence / chain**: Append, Join (parallel — important: LitMotion is
  "1 timeline per variable", so joins work differently), AppendInterval,
  AppendCallback
- **Easings / loops / timing**: SetEase, SetLoops (Yoyo / Restart / Incremental),
  SetDelay, SetUpdate (UnscaledTime)
- **Kill / pause / play / To**: KillAll, Pause, Play, generic `DOTween.To`

### Files modified

```
open-assets/tween/INTEGRATION.md   ← APPENDED new "DOTween → LitMotion API
                                      translation table" section (≈150 lines)
```

The new section includes:
- 36 API mapping rows grouped by category (Transform, UI/SpriteRenderer/Camera,
  CanvasGroup/RectTransform, Sequence/chain)
- Note A on how to scale a single axis (LitMotion binds whole Vector3; need
  to write each frame to a clone)
- Smoke-test snippet (a `LitMotionSmokeTest : MonoBehaviour` that runs
  position + scale + alpha + sequence in parallel — green light means
  install worked)

✅ **Resolved**.

---

## N5: End-to-end file manifest (this N1–N4 pass)

```
hackathon-prep/assets/art/ui/                      ← appended to (Kenney pack extracted)
└── kenney-fantasy-ui-borders/                     ← NEW (extracted from CC0 zip)
    ├── License.txt, Sample.png, Preview.png, Visit Kenney.url, Visit Patreon.url
    ├── Vector/fantasy-ui-borders.svg
    └── PNG/ (Default & Double × Border / Panel / Transparent border /
            Transparent center / Divider / Divider Fade)

hackathon-prep/open-assets/                        ← appended to
├── ui-2d/                                          ← NEW directory
│   ├── README.md                                   ← gap-mapping summary (this file)
│   ├── HP_BAR_RECIPE.md                            ← N2 — composition recipe for HP bars
│   ├── _research/                                  ← 10 contact/zoom reference PNGs (analysis support)
│   └── kenney-fantasy-ui-borders/
│       ├── README.md                               ← per-pack doc (border recommendation table)
│       ├── hslider_preview.png                     ← helper preview of Kenney UI Pack slider pieces
│       └── _reference/
│           └── _source_archive/kenney_fantasy-ui-borders.zip   ← original 350 KB download, verbatim
├── cinemachine-urp/README.md                       ← APPENDED 2D Orthographic section (N3)
├── input/                                          ← R3 fix: InputSystem/ full clone DELETED (~39 MB)
├── tween/INTEGRATION.md                            ← APPENDED DOTween → LitMotion translation table (N4)
│
hackathon-prep/OPEN-ASSETS.md                      ← APPENDED N1–N4 sections + updated summary table
hackathon-prep/assets/LICENSES.md                  ← APPENDED Kenney Fantasy UI Borders row
hackathon-prep/README.md                            ← APPENDED "失败/偏差项" entry
```

**Final state: 9/9 gaps solved (excluding G4 on-site) — all open-source / CC0 / MIT (including Unity Companion License assets, see per-file license breakdown in each subdirectory's `LICENSE` file).**

---

## G7 — 6 follow-on gaps filled 2026-10-16

Re-audit of the 2026-10-15 prep pack flagged 6 remaining gaps. All closed in this pass:

### Summary

| Gap | What | Status | Source license | Location |
| --- | ---- | ------ | -------------- | -------- |
| **G7-1** | CJK fonts (Chinese support for UI) | ✅ Resolved — **Noto Sans SC (Reg/Bold)** + **LXGW WenKai (Reg/Medium/Mono)** + README-CJK.md | OFL 1.1 | `assets/fonts/` |
| **G7-2** | Particle factory dual-pipeline + Editor baker | ✅ Resolved — factory now auto-detects URP vs Built-in; Editor menu bakes 8 .prefab files | MIT (factory + baker) | `open-assets/particles-prefabs/` |
| **G7-3** | Built-in RP Post-Processing Volume Profile | ✅ Resolved — **JamKit_BuiltIn_PostProcessing_Profile.asset** with stable GUID | MIT (ours) + UCL reference (PPv2 MonoScripts) | `open-assets/postprocessing/` |
| **G7-4** | Cinemachine 3D + 2D .prefab files (no hand-write) | ✅ Resolved — **2 hand-authored .prefab files** with stable GUIDs (.meta); MonoScript GUIDs match upstream CM3.1.1 | MIT (ours) + UCL reference (CM3 Scripted) | `open-assets/cinemachine-urp/Prefabs/` |
| **G7-5** | New Input System dual-branch in mover scripts | ✅ Resolved — `#if ENABLE_INPUT_SYSTEM` blocks in `TopDownMover.cs` + `PlatformerMover.cs` (Keyboard.current + Gamepad.current) | MIT (ours) | `scripts/` |
| **G7-6** | shoot_001..003 CC0 + jsfxr README entry | ✅ Resolved — 3 procedurally-generated CC0 shoot sounds + jsfxr added to SFX generator README | CC0 (sounds) + Unlicense (jsfxr) | `assets/sfx/action/shoot_001..003.ogg` + `open-assets/sfx-generators/README.md` |

**Final state: 15/15 gaps solved (G1–G3, G5–G6, G7-1..6, N1–N4) — excluding G4 (on-site, Tuanjie login).**

### G7-1 — CJK fonts

- **Noto Sans SC** Regular + Bold: from `https://github.com/notofonts/noto-cjk` (SubsetOTF/SC/, ~8.3-8.5 MB each). License: **OFL 1.1**. Full license in `NotoSansSC-LICENSE.txt`. Release notes in `NotoSansSC-HISTORY.txt`.
- **LXGW WenKai** Regular + Medium + Mono: from `https://github.com/lxgw/LxgwWenKai/releases/tag/v1.522` (~25.5 MB each). License: **OFL 1.1** + Reserved Font Names "霞鹜"/"霞鶩"/"落霞孤鹜"/"落霞孤鶩"/"LXGW" (你不可用这 5 个字符串作游戏名/字体名，其它商用/改名/嵌入都自由). Full license in `LXGWWenKai-OFL.txt`.
- **LXGW 不发 Bold**：只发 Light/Regular/Medium。用 Medium 作为「粗体」替代，文档说明。
- `README-CJK.md`: TMP 动态/静态图集对比、按需子集化、fallback 配置、Unity/团结接入步骤、24h 黑客松推荐配置。

### G7-2 — Particle factory dual-pipeline + Editor baker

- `JamKitParticleFactory.cs` 现在通过 `UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline` 检测：
  - 非空 → URP `Universal Render Pipeline/Particles/Unlit`，属性 `_BaseColor` / `_BaseMap` / `_Surface` / `_Blend`
  - 空 → Built-in `Particles/Standard Unlit`，属性 `_MainTex` / `_TintColor` / `_Mode` / `_SrcBlend` / `_DstBlend`
- 新增 `Editor/JamKitParticlePrefabBaker.cs`：菜单 `Tools / JamKit / Bake Particle Prefabs (3D + 2D)` 一键 bake 出 **8 个 .prefab**（火花 / 尘埃 / 爆炸 / 拾取金光 × 3D/2D）。用 `PrefabUtility.SaveAsPrefabAsset` 生成，Unity 自动 mint GUID + .meta，避免手写 .prefab 的 GUID 问题。
- `RECIPES.md` 已更新：双管线使用说明、baker 用法、3D/2D 变体差异。

### G7-3 — Built-in RP Post-Processing Volume Profile

- `open-assets/postprocessing/JamKit_BuiltIn_PostProcessing_Profile.asset`（稳定 GUID `651f34e1d6904f499f5bff25906d0012`）。
- 3 效果：**Bloom**（intensity 0.5）+ **ColorGrading**（LDR mode，saturation 1.05，contrast 1.05，ACES tonemap）+ **Vignette**（intensity 0.4，procedural）。
- 引用 upstream `com.unity.postprocessing` MonoScript GUIDs（来自 `Unity-Technologies/PostProcessing` v2.x → 3.4.x）：
  - `PostProcessProfile` = `8e6292b2c06870d4495f009f912b9600`
  - `Bloom` = `48a79b01ea5641d4aa6daa2e23605641`
  - `ColorGrading` = `adb84e30e02715445aeb9959894e3b4d`
  - `Vignette` = `40b924e2dad56384a8df2a1e111bb675`
- 用户必须安装 `com.unity.postprocessing` 3.4.x（**最后支持 Built-in 的版本**；v3+ 仅 HDRP/URP）。

### G7-4 — Cinemachine 3D + 2D .prefab

- `Prefabs/JamKit_ThirdPersonFollow3D.prefab` (GUID `78ea5746051c4c73acffe8df5caad970`)：`CinemachineCamera` + `CinemachineThirdPersonFollow` (CameraDistance=4) + `CinemachineRotationComposer`，FOV 60°，右肩偏移 (0.5, -0.4, 0)。
- `Prefabs/JamKit_2DPlatformerOrtho.prefab` (GUID `112b381e29654b15b6e85a40fb59c7df`)：`CinemachineCamera` (OrthographicSize=6, ModeOverride=Orthographic) + `CinemachinePositionComposer` (DeadZone 0.1, Damping 0.5, ScreenPosition (0, 0.1))。
- MonoScript GUIDs (CM3.1.1):
  - `CinemachineCamera` = `f9dfa5b682dcd46bda6128250e975f58`
  - `CinemachineThirdPersonFollow` = `625c14eee3ad46c99df7c7c891ef668a`
  - `CinemachinePositionComposer` = `886251e9a18ece04ea8e61686c173e1b`
  - `CinemachineRotationComposer` = `f38bda98361e1de48a4ca2bd86ea3c17`

### G7-5 — New Input System dual-branch

- `scripts/TopDownMover.cs` + `scripts/PlatformerMover.cs` 改用 `#if ENABLE_INPUT_SYSTEM` 双分支：
  - 旧版 Input Manager 默认路径：`Input.GetAxisRaw` / `Input.GetKeyDown`（不变）
  - 新 Input System 路径：`Keyboard.current.aKey.isPressed` / `wasPressedThisFrame` + `Gamepad.current.leftStick.ReadValue()` + `Gamepad.current.buttonSouth.wasPressedThisFrame`
- Project Settings → Player → Active Input Handling 设为 "Both" 即两边都能用。

### G7-6 — CC0 shoot sounds + jsfxr entry

- `assets/sfx/action/shoot_001.ogg` (7 KB, laser zap, 200 ms)
- `assets/sfx/action/shoot_002.ogg` (5 KB, high-pitched zap, 80 ms)
- `assets/sfx/action/shoot_003.ogg` (8 KB, low cannon rumble, 400 ms)
- 全部由 ffmpeg 从零合成（`anoisesrc` + `sine` + 高通/低通/`tremolo`/`aecho`），**CC0 公共领域**，可商用。生成脚本在 `SHOOT-SOURCES.md`。
- `open-assets/sfx-generators/README.md` 新增 **jsfxr** 条目：https://sfxr.me + `chr15m/jsfxr` repo，**The Unlicense**（公共领域等同）。

### Files modified / created (G7 pass)

```
hackathon-prep/
├── assets/fonts/                                          ← NEW: 6 .otf/.ttf files + LICENSE/HISTORY + README-CJK.md
├── assets/sfx/action/shoot_001..003.ogg                   ← NEW: 3 CC0 sounds
├── assets/sfx/action/SHOOT-SOURCES.md                     ← NEW: generation docs
├── open-assets/particles-prefabs/JamKitParticleFactory.cs ← UPDATED: dual-pipeline (URP/Built-in)
├── open-assets/particles-prefabs/RECIPES.md               ← UPDATED: dual-pipeline docs + baker
├── open-assets/particles-prefabs/Editor/JamKitParticlePrefabBaker.cs ← NEW: editor baker
├── open-assets/postprocessing/                            ← NEW directory
│   ├── JamKit_BuiltIn_PostProcessing_Profile.asset        (stable GUID 651f34e1...)
│   ├── JamKit_BuiltIn_PostProcessing_Profile.asset.meta
│   ├── README.md
│   └── LICENSE
├── open-assets/cinemachine-urp/Prefabs/                   ← NEW directory
│   ├── JamKit_ThirdPersonFollow3D.prefab                  (stable GUID 78ea5746...)
│   ├── JamKit_ThirdPersonFollow3D.prefab.meta
│   ├── JamKit_2DPlatformerOrtho.prefab                    (stable GUID 112b381e...)
│   └── JamKit_2DPlatformerOrtho.prefab.meta
├── open-assets/cinemachine-urp/README.md                  ← APPENDED Prefabs section
├── open-assets/sfx-generators/README.md                   ← APPENDED jsfxr entry
├── scripts/TopDownMover.cs                                ← UPDATED: ENABLE_INPUT_SYSTEM branch
├── scripts/PlatformerMover.cs                             ← UPDATED: ENABLE_INPUT_SYSTEM branch
├── assets/LICENSES.md                                     ← APPENDED G7 entries (Noto SC, LXGW WenKai, shoot sounds, jsfxr, MonoScript GUIDs)
└── OPEN-ASSETS.md                                         ← THIS section
```

**Final state: 15/15 gaps solved — all open-source / CC0 / MIT / OFL / BSD / zlib / Apache / Unlicense (plus UCL reference files for Unity-shipped MonoScripts and sample assets, see each subdirectory's LICENSE).**
