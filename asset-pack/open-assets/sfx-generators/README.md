# SFX Generators — Quick-Reference

> **TL;DR**: jfxr / Bfxr / ChipTone / rFXGen are open-source procedural SFX
> generators. Generate sounds in your browser, download as `.wav`, drop into
> `Assets/SFX/`, load with `AudioManager.PlaySFX(clip)`.

## Pick one

| Tool | URL | License | Best for |
| ---- | --- | ------- | -------- |
| **jfxr** | https://jfxr.frozenfractal.com (code: https://github.com/ttencate/jfxr) | BSD-3-Clause | Game-feel juice, hits, jumps, lasers — most polished UI of the bunch |
| **jsfxr** | https://sfxr.me (code: https://github.com/chr15m/jsfxr) | The Unlicense (public domain) | Browser-friendly port of sfxr (DrPetter's original) — simpler UI, fewer parameters than jfxr; ships as a single HTML file usable offline |
| **Bfxr2** | https://www.bfxr.net (code: https://github.com/increpare/bfxr2) | MIT | Classic 8/16-bit game SFX — also generates footsteps (snow/grass/dirt/gravel/wood) |
| **ChipTone** | https://sfbgames.itch.io/chiptone | Generated sounds: **CC0** (tool: 源码不公开 / source not publicly available) | Retro chiptune SFX, multiple oscillators per sound |
| **rFXGen** | https://raylibtech.itch.io/rfxgen (code: https://github.com/raysan5/rfxgen) | zlib | Wave-based synthesis with command-line batch export (`rfxgen --export-all`) — best for procedural pipelines |

## When to use which

- **Just need a "pickup" or "hit"**: jfxr → randomize → mutate → tweak → export.
- **Need offline / on a tablet**: jsfxr → same algorithm, single HTML file works without network.
- **Need footstep variety**: Bfxr2 → Footsteppr → choose surface (snow/grass/etc.).
- **Need chiptune vibe**: ChipTone → layer oscillators → export WAV.
- **Need to batch-generate 50+ sounds at build time**: rFXGen CLI.

## Workflow

1. Open the tool URL above (jfxr / Bfxr2 are browser-only, no install).
2. Click "Randomize" (or use a preset like "Pickup/Coin", "Hit", "Laser").
3. Tweak parameters until you like it. Mutate a few times for variety.
4. Export as `.wav` (16-bit PCM, 44.1 kHz mono is plenty for SFX).
5. Drop into `<unity-project>/Assets/SFX/<category>/<sound>.wav`.
6. AudioManager picks it up via `Resources.Load` or drag onto a serialized array.

Or use **AudioManager.cs** as-is (already in `scripts/`): it loads from a
serialized `AudioClip[]` array you populate by drag-dropping the WAVs in
the inspector.

## Saving sounds for offline use

jfxr / Bfxr2 / ChipTone all generate **client-side** — no server needed.
Your generated sounds are entirely yours (per their README + the BSD/MIT
licenses). You don't need to attribute the tool in your game.

If you find a sound in Bfxr2 / jfxr's "preset library" you particularly like,
the generated WAV is yours — the underlying tool is BSD/MIT licensed, not
the output. (Same as how Photoshop-generated PNGs are yours, Photoshop
itself isn't.)

## License summary (verbatim)

### jfxr — BSD-3-Clause

```
Copyright (c) 2014, Thomas ten Cate
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice...
```

Full text: https://github.com/ttencate/jfxr/blob/master/app/LICENSE (also in
`app/LICENSE` and `lib/LICENSE` within the cloned repo — the root no longer ships a top-level `LICENSE` file, 2026-10-15 verification).

Output (generated sounds): "entirely yours to do with as you please"
(README).

### jsfxr — The Unlicense (public domain)

```
This is free and unencumbered software released into the public domain.
Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.
```

The original JavaScript port of sfxr (DrPetter/Tomas Pettersson's
algorithm), hosted at https://sfxr.me. Source: https://github.com/chr15m/jsfxr

UI is simpler than jfxr (no envelope graph; presets are randomized by
mutating a few parameters). Ships as a single HTML file — usable offline
once cached, useful on tablets or restricted networks during the jam.

Full text: https://github.com/chr15m/jsfxr/blob/master/UNLICENSE

Output (generated sounds): completely unencumbered (Unlicense + sfxr's
algorithm is public domain). Same as jfxr / Bfxr2: the WAV you generate
is yours.

### Bfxr2 — MIT

```
MIT License
Copyright (c) 2021 Stephen Lavelle
```

Full text: https://github.com/increpare/bfxr2/blob/master/LICENSE

### ChipTone — Output CC0, tool source unavailable

Sounds you make are CC0 (the only thing that ships, since the tool is
a closed-source itch.io web app — `https://sfbgames.itch.io/chiptone`).
We previously listed a `github.com/SFBGames/ChipTone-Haxe` source link
that does **not** exist; use the generated WAVs without concern.
To rebuild / inspect the tool you'd need to contact the author
(SFBGames / Steven F. Brown) via itch.io directly.

### rFXGen — zlib

```
Copyright (c) 2017-2024 raylib technologies (@raysan5)
This software is provided "as-is", without any express or implied warranty.
```

Full text: https://github.com/raysan5/rfxgen/blob/master/LICENSE

## Why we don't ship the binaries

- They're interactive tools, not assets — you generate sounds *with* them.
- License-wise, redistributing the executables would require bundling LICENSE
  text + keeping copyright notices. Cleaner to point to the URLs and let
  you grab the binary at jam time (5 seconds of work, fully offline-capable
  for jfxr/Bfxr2 once loaded).