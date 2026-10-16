# Asset Licenses — Hackathon Prep Pack

> 来源表 / Source + License table for the **团结 Codely 上海站 24h 黑客松** (10/17–18) prep pack.
>
> Assets fall into one of these licenses (each explicitly permits redistribution
> under the conditions documented per source below):
> - **CC0** (public domain) — Kenney packs, OpenGameArt music, ChipTone output
> - **OFL 1.1** (SIL Open Font License) — fonts
> - **Apache 2.0** — Roboto font
> - **MIT** — LitMotion, our hand-authored files (`JamKit_*`, `JamKitParticleFactory.cs`, `RECIPES.md`, `INTEGRATION.md`, etc.), Boost `SuccessParticles.prefab`, Bfxr2, `JamKit_MinimalInputActions.inputactions`
> - **BSD-3-Clause** — jfxr
> - **zlib** — rFXGen
> - **Unity Companion License (UCL)** — Unity-shipped `.inputactions`, VolumeProfile `.asset`, URP `.asset`, sample `.unity` files (G2 + G3; full §5 Notices text reproduced in this file)
>
> 任何 CC-BY 来源都已记录署名要求。

---

## 1. Kenney (https://kenney.nl) — CC0 (Public Domain)

| Pack | URL | Local path |
| --- | --- | --- |
| UI Pack (2.0) | https://kenney.nl/assets/ui-pack | `assets/art/ui/kenney-ui-pack/` |
| Input Prompts (1.5) | https://kenney.nl/assets/input-prompts | `assets/art/input-prompts/` |
| Particle Pack | https://kenney.nl/assets/particle-pack | `assets/art/particles/kenney-particle-pack/` |
| Interface Sounds | https://kenney.nl/assets/interface-sounds | `assets/sfx/{ui,pickup,win,lose}/*.ogg` |
| Impact Sounds | https://kenney.nl/assets/impact-sounds | `assets/sfx/action/*.ogg` |
| Music Jingles | https://kenney.nl/assets/music-jingles | (jingles only, not used as BGM) |
| **Fantasy UI Borders (1.0)**  *(added 2026-10-15, N1)* | https://kenney.nl/assets/fantasy-ui-borders | `assets/art/ui/kenney-fantasy-ui-borders/` |

- **License**: CC0 1.0 — https://creativecommons.org/publicdomain/zero/1.0/
- **Attribution**: Not required, but appreciated. Credit: "Kenney" or "kenney.nl"
- **Notes**:
  - UI Pack: SVG vector + PNG raster, multiple colour themes (Blue/Green/Red/Yellow/Grey)
  - Input Prompts: Per-console vectors (Xbox, PS, Switch, Switch 2, Wii, Gamecube, Meta Quest, Keyboard/Mouse, Generic) — use **Keyboard & Mouse** + **Generic** subsets for max portability
  - Particle Pack: PNG (Transparent) + Unity samples; use transparent PNG for non-Unity preview, the .prefab samples for actual Unity import
  - Interface Sounds: 100 short UI clips (click/open/close/confirmation/error/drop/bong/etc.) — selected `click_*`, `confirmation_*`, `close_*`, `open_*`, `drop_*`, `error_*`, `bong_*` into the SFX tree
  - Impact Sounds: 130 impact clips (footstep_*, impactMetal_*, impactGlass_*, impactGeneric_*, impactBell_*, impactPlate_*, impactPlank_*, impactMining_*) — selected `impactMetal_medium_*`, `impactGeneric_light_*`, `impactBell_heavy_*`, `impactPlank_medium_*`, `footstep_wood_*`, `footstep_concrete_*`, `impactPlate_heavy_*`, `impactMining_*` into `assets/sfx/action/`
  - Music Jingles: 17 short stings × 5 styles (NES/Sax/Steel/Pizzicato/Hit). **Too short for BGM looping** — keep as `win_/lose_/pickup_/transition_` stings if desired, but BGM uses OpenGameArt tracks below.
  - **Fantasy UI Borders**: 280 PNGs (48×48 + 96×96 in Default/Double) + 1 SVG master vector — closed-ring panel borders, solid filled panels, corner/edge fragments, center fill tiles, dividers. Use the 32× `Border/*.png` files in either resolution for Sliced Unity Images (recommended Sprite Editor border 2 px for Default, 4 px for Double).

## 2. OpenGameArt CC0 music — CC0 (Public Domain)

| Track | Author | Local path | Mood |
| --- | --- | --- | --- |
| Slow Stride Loop | Pro Sensory | `assets/bgm/calm/slow_stride_loop.ogg` | calm |
| A Place I Call Home | Pro Sensory (Calm1) | `assets/bgm/calm/a_place_i_call_home.ogg` | calm |
| Home Town | Pro Sensory (Town1) | `assets/bgm/calm/home_town.ogg` | calm |
| Sirens in Darkness | Pro Sensory (012) | `assets/bgm/tense/sirens_in_darkness.mp3` | tense |
| Dust | Pro Sensory | `assets/bgm/tense/dust.mp3` | tense |
| A Simple Trifle | Pro Sensory | `assets/bgm/ambient/simple_trifle_loop.ogg` | ambient |
| Heavenly Loop | Pro Sensory | `assets/bgm/ambient/heavenly_loop.ogg` | ambient |

- **License**: CC0 1.0 — https://creativecommons.org/publicdomain/zero/1.0/
- **Source page**: https://opengameart.org/content/cc0-calm-relaxing-music
- **Attribution**: Not legally required for CC0; **recommended**: `Music by Pro Sensory — CC0 (opengameart.org)`
- **Notes**:
  - "Slow Stride Loop", "A Simple Trifle", "Heavenly Loop", "A Place I Call Home", "Home Town" are explicitly loopable — drop straight into `AudioManager.PlayMusic(clip, fadeDuration: 0.5f, volume: 1f)` (loop is set internally on the music sources; callers don't pass it)
  - "Sirens in Darkness" and "Dust" are longer narrative pieces — works for tense / boss / climax scenes

## 3. Fonts

### Roboto — Apache License 2.0

- **Files**: `assets/fonts/Roboto-{Regular,Bold,Medium}.ttf` + `Roboto-LICENSE.txt` + `Roboto-NOTICE.txt`
- **Source**: https://github.com/googlefonts/roboto (Apache 2.0)
- **License**: https://www.apache.org/licenses/LICENSE-2.0
- **Attribution**: Required by Apache-2.0 § 4(d) — when redistributing the font binaries you MUST keep the copyright notice, the LICENSE text (or a URI pointing to it), and the NOTICE file alongside them. Both `Roboto-LICENSE.txt` and `Roboto-NOTICE.txt` ship next to the `.ttf` files. Courtesy credit: "Roboto by Christian Robertson — Apache 2.0".

### Inter — SIL Open Font License 1.1 (OFL)

- **Files**: `assets/fonts/Inter-{Variable,Medium,Bold}.ttf` + `Inter-LICENSE.txt`
- **Source**: https://github.com/rsms/inter/releases/tag/v4.0
- **License**: https://scripts.sil.org/OFL
- **Attribution**: "Inter by Rasmus Andersson — SIL OFL 1.1"

### Press Start 2P — SIL Open Font License 1.1 (OFL)

- **Files**: `assets/fonts/PressStart2P-Regular.ttf` + `PressStart2P-LICENSE.txt`
- **Source**: https://github.com/google/fonts/tree/main/ofl/pressstart2p
- **License**: https://scripts.sil.org/OFL (full text in `PressStart2P-LICENSE.txt`)
- **Attribution**: "Press Start 2P by CodeMan38 — SIL OFL 1.1"

### Bebas Neue — SIL Open Font License 1.1 (OFL)

- **Files**: `assets/fonts/BebasNeue-Regular.ttf` + `BebasNeue-LICENSE.txt`
- **Source**: https://github.com/dharmatype/Bebas-Neue
- **License**: https://scripts.sil.org/OFL (full text in `BebasNeue-LICENSE.txt`)
- **Attribution**: "Bebas Neue by Ryoichi Tsunekawa, Dharma Type — SIL OFL 1.1"

---

## Summary

| 来源 | License | 数量 | 是否需要署名 |
| --- | --- | --- | --- |
| Kenney UI Pack | CC0 | 1 pack (vector + PNG) | 否 (推荐) |
| Kenney Input Prompts | CC0 | 1 pack | 否 |
| Kenney Particle Pack | CC0 | 1 pack | 否 |
| Kenney Interface Sounds | CC0 | 100 clips → 24 selected into SFX tree | 否 |
| Kenney Impact Sounds | CC0 | 130 clips → 16 selected into `sfx/action/` | 否 |
| OpenGameArt (Pro Sensory) | CC0 | 7 tracks | 否 (推荐) |
| Roboto | Apache 2.0 | 3 weights + LICENSE + NOTICE | 保留版权/LICENSE/NOTICE (§ 4d) |
| Inter | OFL | Variable + 2 statics | 否 |
| Press Start 2P | OFL | 1 | 否 |
| Bebas Neue | OFL | 1 | 否 |

**总计**: 24h 黑客松合规 — 无任何来源需要付费授权、无任何来源禁止商业/竞赛用途。
---

# Addendum — open-source assets added 2026-10-15 (this prep pass)

See `OPEN-ASSETS.md` for the full gap-by-gap inventory. Quick license table:

| Gap | Asset | Source | License | Local path |
| --- | ----- | ------ | ------- | ---------- |
| G1 | **LitMotion v2.0.2** (tween library) | https://github.com/annulusgames/LitMotion | **MIT** © 2023 Yusuke Nakada | `open-assets/tween/LitMotion/` (full clone) — install via UPM |
| G2 | **DefaultInputActions.inputactions** | Unity-Technologies/InputSystem (Unity Companion License) | Unity Companion License | `open-assets/input/InputActions/JamKit_DefaultInputActions.inputactions` |
| G2 | **JamKit_MinimalInputActions.inputactions** (3 actions: Move/Jump/Fire) | this repo | **MIT** | `open-assets/input/InputActions/JamKit_MinimalInputActions.inputactions` |
| G3 | **JamKit_URP_PostProcessing_Profile.asset** | Unity-Technologies/UniversalRenderingExamples | Unity Companion License | `open-assets/cinemachine-urp/VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset` |
| G3 | **CinemachineSampleSceneProfile_URP.asset** | Unity-Technologies/com.unity.cinemachine | Unity Companion License | `open-assets/cinemachine-urp/VolumeProfiles/CinemachineSampleSceneProfile_URP.asset` |
| G3 | Cinemachine parameter recommendations (extracted) | Unity-Technologies/com.unity.cinemachine Samples~/3D Samples/RunningRace.unity | Unity Companion License (parametric values; no copyrighted material) | `open-assets/cinemachine-urp/README.md` (table) |
| G5 | **JamKitParticleFactory.cs** (runtime particle builder) | this repo | **MIT** | `open-assets/particles-prefabs/JamKitParticleFactory.cs` |
| G5 | **Success Particles.prefab** (MIT sample, reference template) | CompleteUnityDeveloper2/3_Project_Boost (© 2017 EmbraceIT Ltd trading as GameDev.tv) | **MIT** | `open-assets/particles-prefabs/_MIT_reference_SuccessParticles.prefab` |
| G5 | Kenney Particle Pack PNG textures (already in prep) | Kenney (kenney.nl) | **CC0** | `assets/art/particles/kenney-particle-pack/png/` |
| G6 | **jfxr** (URL) | https://jfxr.frozenfractal.com — code https://github.com/ttencate/jfxr | **BSD-3-Clause** (tool) + generated sounds unencumbered per README | (URL only in `open-assets/sfx-generators/README.md`) |
| G6 | **Bfxr2** (URL) | https://www.bfxr.net — code https://github.com/increpare/bfxr2 | **MIT** | (URL only) |
| G6 | **ChipTone** (URL) | https://sfbgames.itch.io/chiptone | Generated sounds **CC0** | (URL only) |
| G6 | **rFXGen** (URL) | https://raylibtech.itch.io/rfxgen — code https://github.com/raysan5/rfxgen | **zlib** | (URL only) |

## MIT license texts (in-repo, full)

### LitMotion (G1)

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

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

Full text at `open-assets/tween/LitMotion/LICENSE` (1070 bytes).

### Boost (GameDev.tv Success Particles prefab, G5)

```
MIT License

Copyright (c) 2017 EmbraceIT Ltd. trading as GameDev.tv

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

Source repo: https://github.com/CompleteUnityDeveloper2/3_Project_Boost

### jfxr (BSD-3-Clause, G6)

```
Copyright (c) 2014, Thomas ten Cate
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its contributors
   may be used to endorse or promote products derived from this software
   without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED...
```

Source repo: https://github.com/ttencate/jfxr

## Unity Companion License note (G2, G3) — Compliance statement

> **Status (2026-10-15)**: Compliant **for use inside your own Unity / Tuanjie project**.
> This prep pack ships the Unity Companion License files as **samples /
> excerpts / reference material** (per the Companion License §5 "Notices &
> Third-Party Rights" requirement that this License text be provided in all
> substantial portions of the Work and derivative works thereof). The pack
> itself is **not** a redistribution of the Unity InputSystem / URPExamples /
> Cinemachine packages as standalone libraries — we deliberately deleted the
> 39 MB `open-assets/input/InputSystem/` full-clone that was in the prior
> draft (R3, 2026-10-15 fix) and replaced it with a pointer to the GitHub
> source repo. Only the small handful of `.inputactions` / `.asset` /
> `.unity` files actually needed for the jam are kept in this pack.

### §5 Notices text (verbatim, UCL v1.4, 2024-10-29)

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

### Companion License summary (informational; see full text above for legal terms)

- **§1 Unity Companion Use.** Permitted as long as it's in connection with
  authoring/distributing applications under a valid Unity Engine License
  (i.e. inside any normal Unity / Tuanjie game project).
- **§3.2 Unity owns its content.** Unity owns all derivative works of the Work.
- **§4 Trademarks.** No right to use Unity's trademarks, service marks, or
  branding. Descriptive uses OK per Unity's Trademark Guidelines.
- **§5 Notices & Third-Party Rights.** This License + copyright notice must
  be provided in all substantial portions of the Work and derivative works
  thereof. — **Required notice text reproduced verbatim above.**
- **§6 Disclaimer.** Provided "AS IS" without warranty.
- **§7 USE IS ACCEPTANCE.** Your use constitutes acceptance.

### How this pack uses Unity Companion-licensed material

| Source | How used in this pack | Status |
| ------ | ---------------------- | ------ |
| Unity-Technologies/InputSystem → `DefaultInputActions.inputactions` | 1 file copied verbatim into `open-assets/input/InputActions/JamKit_DefaultInputActions.inputactions` | Compliant for use inside a Unity project; §5 notice reproduced above |
| Unity-Technologies/UniversalRenderingExamples → `FPSCameraDemo/Post-process Volume Profile.asset` | 1 file copied into `open-assets/cinemachine-urp/_URPExamples_source_FPS_profile/...` (reference) + a derivative `VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset` (with stable GUID) | Compliant for use inside a Unity project; §5 notice reproduced above |
| Unity-Technologies/com.unity.cinemachine → `Projects/URP/Assets/Settings/SampleSceneProfile.asset` (+ ForwardRenderer + 3× UniversalRP-*.asset) + `Samples~/3D Samples/RunningRace.unity` | 7 source files copied into `open-assets/cinemachine-urp/_Cinemachine_source_*/...` (reference) + a derivative `VolumeProfiles/CinemachineSampleSceneProfile_URP.asset` | Compliant for use inside a Unity project; §5 notice reproduced above |

The Tuanjie fork inherits the same licensing terms as Unity (it's a Unity
fork distributed by Unity China).

---

# G7 Addendum — new assets added 2026-10-16

See `OPEN-ASSETS.md` § G7 for the full gap-by-gap inventory. Quick license table:

| Gap | Asset | Source | License | Local path |
| --- | ----- | ------ | ------- | ---------- |
| G7-1 | **Noto Sans SC** Regular + Bold (OTF Subset) | https://github.com/notofonts/noto-cjk `Sans/SubsetOTF/SC/` | **OFL 1.1** © Google + Adobe CJK 团队 | `assets/fonts/NotoSansSC-{Regular,Bold}.otf` + LICENSE/HISTORY |
| G7-1 | **LXGW WenKai** Regular + Medium + Mono (TTF) | https://github.com/lxgw/LxgwWenKai v1.522 | **OFL 1.1** + Reserved Font Names "霞鹜"/"霞鶩"/"落霞孤鹜"/"落霞孤鶩"/"LXGW" © LXGW | `assets/fonts/LXGWWenKai-{Regular,Medium,Mono}.ttf` (No Bold exists; Medium substitutes) |
| G7-1 | `README-CJK.md` (CJK font integration guide) | this repo | **MIT** | `assets/fonts/README-CJK.md` |
| G7-2 | `JamKitParticleFactory.cs` (updated to dual-pipeline URP + Built-in) | this repo | **MIT** | `open-assets/particles-prefabs/JamKitParticleFactory.cs` |
| G7-2 | `Editor/JamKitParticlePrefabBaker.cs` (one-click baker for 8 .prefab) | this repo | **MIT** | `open-assets/particles-prefabs/Editor/JamKitParticlePrefabBaker.cs` |
| G7-2 | `RECIPES.md` (updated docs) | this repo | **MIT** | `open-assets/particles-prefabs/RECIPES.md` |
| G7-3 | `JamKit_BuiltIn_PostProcessing_Profile.asset` (Built-in RP Volume Profile) | this repo (derived from upstream class names, MIT content) | **MIT** | `open-assets/postprocessing/JamKit_BuiltIn_PostProcessing_Profile.asset` (+ .meta stable GUID 651f34e1...) |
| G7-3 | `README.md` + `LICENSE` (Built-in RP profile docs) | this repo | **MIT** | `open-assets/postprocessing/{README.md, LICENSE}` |
| G7-4 | `JamKit_ThirdPersonFollow3D.prefab` (Cinemachine 3.x 3D TPS prefab) | this repo | **MIT** | `open-assets/cinemachine-urp/Prefabs/JamKit_ThirdPersonFollow3D.prefab` (+ .meta stable GUID 78ea5746...) |
| G7-4 | `JamKit_2DPlatformerOrtho.prefab` (Cinemachine 3.x 2D ortho prefab) | this repo | **MIT** | `open-assets/cinemachine-urp/Prefabs/JamKit_2DPlatformerOrtho.prefab` (+ .meta stable GUID 112b381e...) |
| G7-5 | `TopDownMover.cs` (updated with `#if ENABLE_INPUT_SYSTEM` dual-branch) | this repo | **MIT** | `scripts/TopDownMover.cs` |
| G7-5 | `PlatformerMover.cs` (updated with `#if ENABLE_INPUT_SYSTEM` dual-branch) | this repo | **MIT** | `scripts/PlatformerMover.cs` |
| G7-6 | `shoot_001.ogg` (laser zap, 200 ms) | procedurally generated via ffmpeg from `anoisesrc` + filters | **CC0 1.0** (public domain) | `assets/sfx/action/shoot_001.ogg` |
| G7-6 | `shoot_002.ogg` (high-pitched zap, 80 ms) | procedurally generated via ffmpeg from `sine` + filters | **CC0 1.0** (public domain) | `assets/sfx/action/shoot_002.ogg` |
| G7-6 | `shoot_003.ogg` (low cannon rumble, 400 ms) | procedurally generated via ffmpeg from `anoisesrc` + filters | **CC0 1.0** (public domain) | `assets/sfx/action/shoot_003.ogg` |
| G7-6 | `SHOOT-SOURCES.md` (generation commands + license) | this repo | **MIT** | `assets/sfx/action/SHOOT-SOURCES.md` |
| G7-6 | **jsfxr** (SFX generator URL) | https://sfxr.me — code https://github.com/chr15m/jsfxr | **The Unlicense** (public domain) — tool only; generated sounds unencumbered | (URL only in `open-assets/sfx-generators/README.md`) |

## MonoScript GUIDs referenced (UCL reference files, not bundled)

These are upstream Unity-shipped script GUIDs that the G7-3 / G7-4 assets
reference. The MonoScript .cs files themselves ship with the upstream package
(UPM install), not with this prep pack.

| Asset class | MonoScript GUID | Source repo |
| ----------- | --------------- | ----------- |
| `PostProcessProfile` | `8e6292b2c06870d4495f009f912b9600` | Unity-Technologies/PostProcessing v2.x → 3.4.x |
| `Bloom` | `48a79b01ea5641d4aa6daa2e23605641` | (same) |
| `ColorGrading` | `adb84e30e02715445aeb9959894e3b4d` | (same) |
| `Vignette` | `40b924e2dad56384a8df2a1e111bb675` | (same) |
| `CinemachineCamera` | `f9dfa5b682dcd46bda6128250e975f58` | Unity-Technologies/com.unity.cinemachine v3.0.x → 3.1.1 |
| `CinemachineThirdPersonFollow` | `625c14eee3ad46c99df7c7c891ef668a` | (same) |
| `CinemachinePositionComposer` | `886251e9a18ece04ea8e61686c173e1b` | (same) |
| `CinemachineRotationComposer` | `f38bda98361e1de48a4ca2bd86ea3c17` | (same) |

(Unity Companion License for the PPv2 / Cinemachine packages; install via UPM.)

## CJK font Reserved Font Names (LXGW WenKai)

LXGW WenKai is OFL 1.1 with the following **Reserved Font Names** (per the
LICENSE file): `霞鹜`, `霞鶩`, `落霞孤鹜`, `落霞孤鶩`, `LXGW`. Per OFL § 2
"Reserved Font Name(s)", these names **may not be used** to promote,
endorse, or advertise Modified Versions of the font. The same names
**may be used in modified versions that are subsetted/converted (e.g.
WOFF/WOFF2) for web font delivery** but not for desktop installables.
This means:
- ✅ You may call a derivative file `LXGWWenKai-Regular-subset.ttf` (or
  any name that doesn't contain the Reserved Font Names)
- ✅ You may use the font in a game whose name is unrelated (e.g.
  "My Game Title")
- ❌ You may NOT name your game "霞鹜历险记" or your font asset "LXGW Bold"
- ❌ You may NOT sell the unmodified font binaries standalone

(See full OFL text in `LXGWWenKai-OFL.txt` for complete terms.)
