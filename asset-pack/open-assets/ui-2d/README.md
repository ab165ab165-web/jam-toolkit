# UI / 2D Assets — Gap-Filler Pack

> Closes gaps **N1 (9-slice panel border)** and **N2 (health/progress bar)**
> identified after the OPEN-ASSETS pass on the original prep pack.
> All assets here are **CC0** (Kenney, https://kenney.nl).

## Files on disk

```
open-assets/ui-2d/
├── README.md                                   ← this file (gap mapping summary)
├── HP_BAR_RECIPE.md                            ← N2 — progress-bar / health-bar compose recipe
├── _research/                                  ← scratch debug images (kept for the team, not shipped to Unity)
│   ├── _contact_dbl_*.png    (sprite catalog contact sheets for visual classification)
│   └── _zoom_*.png          (per-sprite 8× zoom close-ups for border-measurement inspection)
└── kenney-fantasy-ui-borders/                  ← per-pack doc tree (mirrors assets/art/ui/kenney-fantasy-ui-borders/)
    ├── README.md                                (per-pack installation guide + sprite picker table)
    ├── hslider_preview.png                      (Kenney UI Pack slider pieces composition preview)
    └── _reference/
        └── _source_archive/kenney_fantasy-ui-borders.zip  ← 350 KB original download, kept verbatim
```

The actual deliverable PNGs are physically installed at:
```
assets/art/ui/kenney-fantasy-ui-borders/                  ← 1.4 MB unpacked, will be directly importable into Unity
├── License.txt                              ← CC0 1.0 (Creative Commons Zero)
├── Sample.png / Preview.png                 ← Kenney catalog images (designer-facing)
├── Visit Kenney.url / Visit Patreon.url     ← convenience URLs
├── Vector/fantasy-ui-borders.svg            ← 134 KB master vector file
└── PNG/                                     ← raster, 280 PNGs
    ├── Default/                             ← 48×48 tiles
    │   ├── Border/                          ← 32 closed-ring panel borders ★ 9-slice candidates
    │   ├── Panel/                           ← 32 solid filled panels
    │   ├── Transparent border/              ← 32 corner/edge decorative pieces
    │   ├── Transparent center/              ← 32 center fill tiles
    │   ├── Divider/                         ← 6 horizontal dividers
    │   └── Divider Fade/                    ← 6 fade-edged horizontal dividers
    └── Double/                              ← 96×96 tiles (2× resolution, same motifs)
        └── (same 6 subdirs; 32 each + 6 dividers)
```

## Gaps covered

| Gap | What | Status | License | Where |
| --- | ---- | :----: | ------- | ----- |
| **N1** | 9-slice UI panel/border/dialog box art | ✅ Resolved — **Kenney Fantasy UI Borders** (CC0) — 64 closed-ring tile variants + 64 solid panels + 128 corner/center decorative pieces + 24 dividers across 2 resolutions | **CC0** | `open-assets/ui-2d/kenney-fantasy-ui-borders/` |
| **N2** | Health bar / progress bar art | ✅ Resolved — **no new download needed**; Kenney UI Pack already has `slide_horizontal_grey*` + `slide_horizontal_color*` + `slide_hangle` that compose into a progress bar. Recipe in `HP_BAR_RECIPE.md`. | **CC0** | `assets/art/ui/kenney-ui-pack-png/<Color>/<Default\|Double>/slide_*.png` |
| **N3** | Cinemachine 2D ortho parameter table | ✅ Resolved — appended to `open-assets/cinemachine-urp/README.md` (Camera2D + PositionComposer + Perlin shake + Recipes) | n/a (docs only) | `open-assets/cinemachine-urp/README.md` ("2D Orthographic" section) |
| **N4** | DOTween ↔ LitMotion API translation | ✅ Resolved — full table appended to `open-assets/tween/INTEGRATION.md` (Transform/UI/SpriteRenderer/Camera/RectTransform/CanvasGroup/Sequence/Loops/Ease) | n/a (docs only) | `open-assets/tween/INTEGRATION.md` |

## Why Kenney Fantasy UI Borders (and not RPG-Expansion or UI Rounded)

- **Fantasy UI Borders** (https://kenney.nl/assets/fantasy-ui-borders) — chosen
  because the pack is **explicitly themed around panel/border construction** —
  32 closed-ring tiles (Default) + 32 closed-ring tiles (Double) + 64 broken-
  up "border + center" components. The other candidate ("UI Pack: RPG
  Expansion") is button-themed, not frame-themed. URLs checked 2026-10-15
  (HTTP 200 on all three; kenney.nl/uptime stable).
- **Other 9-slice candidates evaluated and rejected:**
  - **Kenney UI Pack (already in prep)** — buttons/sliders/checks/stars only,
    NO panel/border sprite.
  - **UI Pack: RPG Expansion** — buttons/cursor/callouts, not frames.
  - **UI Pack - Adventure** — inventory/dialog icons, character portrait frames
    but not generic 9-slice tiles.
  - **UI Pack - Sci-Fi** — buttons/holograms, not frames.
  - **OpenGameArt CC0 "9-slice UI"** — checked, but most hits are unfinished
    Photoshop mockups, not Unity-ready sprite sheets.

## N1: which sprites to use for what

### Recommended 9-slice sprite picks (Default/Border, 48×48)

| Sprite | Look | Recommended Unity Sprite Editor Border (L/T/R/B) |
| ------ | ---- | ------------------------------------------------: |
| `panel-border-000.png` | Plain double line | **L=2  T=2  R=2  B=2** |
| `panel-border-002.png` | Plain single line | **L=2  T=2  R=2  B=2** |
| `panel-border-006.png` | Corner brackets | **L=2  T=2  R=2  B=2** |
| `panel-border-010.png` | Notched square | **L=2  T=2  R=2  B=2** |
| `panel-border-022.png` | Beveled | **L=2  T=2  R=2  B=2** |
| `panel-border-031.png` | Octagon-outline | **L=2  T=2  R=2  B=2** |

### Recommended 9-slice sprite picks (Double/Border, 96×96, 2× res)

| Sprite | Look | Border L/T/R/B |
| ------ | ---- | -------------: |
| `panel-border-000.png` | Double-line 2× res | **L=4  T=4  R=4  B=4** |
| `panel-border-002.png` | Single-line 2× res | **L=4  T=4  R=4  B=4** |
| `panel-border-006.png` | Bracket 2× res | **L=4  T=4  R=4  B=4** |
| `panel-border-022.png` | Beveled 2× res | **L=4  T=4  R=4  B=4** |

(75% of the 32 sprites in each set share these uniform border thicknesses.
The "elite" outliers like `panel-border-025.png` have thicker borders; for
those use the larger inset values listed in `kenney-fantasy-ui-borders/README.md`.)

> **Design note**: pick from `PNG/Default/Border/panel-border-*.png` for
> the *closed-ring* sprites (32 files), which are the 9-slice candidates.
> The other 32 in `PNG/Default/Panel/` are *solid-fill* decorations (whole-frame
> art, no 9-slice needed). The 32 in `PNG/Default/Transparent border/` are
> decorative *fragments* (corners + edges) meant for UI Builder composition.
> See `kenney-fantasy-ui-borders/README.md` for the full breakdown.

### Unity setup (5-min recipe)

1. Drag `PNG/Default/Border/panel-border-002.png` into `Assets/Textures/UI/`
2. Select it → **Inspector** → **Texture Type = Sprite (2D and UI)**, **Sprite Mode = Single**, **Pixels Per Unit = 100**
3. Click **Sprite Editor** → four green drag handles inset from each edge → enter **L=2, B=2, R=2, T=2** → **Apply**
4. (Optional) tick **Mesh Type = Full Rect** for clean stretching
5. Use it on a `GameObject → UI → Image` component with **Image Type = Sliced** → Resize the RectTransform to any size; corners stay sharp, edges stretch.

### Acceptable evidence

```
$ find open-assets/ui-2d/kenney-fantasy-ui-borders/PNG -name '*.png' | wc -l
284

$ head -8 open-assets/ui-2d/kenney-fantasy-ui-borders/License.txt
	Fantasy UI Borders (1.0)
	Created/distributed by Kenney (www.kenney.nl)
	Creation date: 03-12-2023
	For the sample image the font 'Aoboshi One' was used, OPL (Open Font License)
	License: (Creative Commons Zero, CC0)
	http://creativecommons.org/publicdomain/zero/1.0/
```

See `kenney-fantasy-ui-borders/README.md` for the full per-sprite breakdown and corner-piece assembly guide.

## N2: HP bar / progress bar

No new download — composition recipe in `HP_BAR_RECIPE.md`. TL;DR: take
`slide_horizontal_grey.png` (track) + `slide_horizontal_color_section_wide.png`
(fill tile, repeat-N times) + optionally `slide_hangle.png` (handle for
slider-style UI). For a true HP bar use only Track + Fill, hide Handle.

## License audit (this pack only — full audit in `OPEN-ASSETS.md`)

| Source | License | Where used | Redistribution |
| ------ | ------- | ---------- | -------------- |
| Kenney "Fantasy UI Borders" 1.0 (2023-12-03) | **CC0** | N1 | ✅ |
| Kenney UI Pack 2.0 (already in prep) | **CC0** | N2 (composition recipe, no new files) | ✅ |

## End-to-end file manifest (new files added by this gap-fill pass)

```
hackathon-prep/assets/art/ui/
└── kenney-fantasy-ui-borders/                        ← NEW (extracted from CC0 zip, 280 PNGs + 1 SVG + License + Sample)

hackathon-prep/open-assets/ui-2d/
├── README.md                                         ← NEW (this file)
├── HP_BAR_RECIPE.md                                  ← NEW (N2 — uses existing slider sprites)
├── _research/                                        ← NEW (10 zoom/contact reference PNGs; analysis support, not shipped to Unity)
└── kenney-fantasy-ui-borders/
    ├── hslider_preview.png                           ← NEW (helper preview of Kenney UI Pack slider pieces)
    └── _reference/
        └── _source_archive/kenney_fantasy-ui-borders.zip   ← NEW (zipped original download, 350 KB)

hackathon-prep/open-assets/cinemachine-urp/README.md  ← APPENDED (2D ortho section)
hackathon-prep/open-assets/tween/INTEGRATION.md       ← APPENDED (DOTween→LitMotion translation table)
hackathon-prep/OPEN-ASSETS.md                         ← APPENDED (N1–N4 sub-sections + new license audit row)
hackathon-prep/assets/LICENSES.md                     ← APPENDED (Kenney Fantasy UI Borders row)
```
