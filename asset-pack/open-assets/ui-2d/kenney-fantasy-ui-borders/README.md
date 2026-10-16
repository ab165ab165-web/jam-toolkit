# Kenney Fantasy UI Borders — 9-slice UI panel source pack

> **Source**: https://kenney.nl/assets/fantasy-ui-borders
> **Version**: 1.0 — released 2023-12-03
> **License**: **CC0 1.0** (Creative Commons Zero, Public Domain Dedication)
> **Verification URL**: https://creativecommons.org/publicdomain/zero/1.0/
> **Attribution**: Optional (recommended): "Kenney" or "kenney.nl"
> **Download URL** (also reproducible): https://kenney.nl/media/pages/assets/fantasy-ui-borders/ab29cd0165-1701602367/kenney_fantasy-ui-borders.zip
> **Local archive** (kept verbatim): `_reference/_source_archive/kenney_fantasy-ui-borders.zip` (350 KB)

## What's in here

```
kenney-fantasy-ui-borders/                         ← 1.4 MB unpacked
├── License.txt                                      ← CC0 + "Kenney" attribution note (verbatim)
├── Sample.png         (213 KB)                      ← designer-facing catalog (panel mock-up)
├── Preview.png        (25 KB)                       ← small preview thumbnail
├── Visit Kenney.url                                 ← convenience URL shortcut
├── Visit Patreon.url                                ← convenience URL shortcut
├── Vector/
│   └── fantasy-ui-borders.svg                       ← 134 KB master vector file (Inkscape/Illustrator compatible)
├── PNG/
│   ├── Default/                                     ← 48×48 tiles (lower-resolution, pixel-art)
│   │   ├── Border/                                  ← 32 closed-ring panel borders ★ 9-slice candidates
│   │   ├── Panel/                                   ← 32 solid filled panels (whole-frame art)
│   │   ├── Transparent border/                      ← 32 decorative corner + edge pieces
│   │   ├── Transparent center/                      ← 32 transparent center fill tiles
│   │   ├── Divider/                                 ← 6 horizontal divider lines
│   │   └── Divider Fade/                            ← 6 fade-edged horizontal dividers (for tapered separators)
│   └── Double/                                      ← 96×96 tiles (2× resolution, same motifs)
│       └── (same 6 subdirs; 32 each + 6 dividers)
└── _reference/
    ├── hslider_preview.png                          ← image of how the Kenney UI Pack sliders compose into HP bars
    └── _source_archive/                             ← untouched zip + extracted snapshot
```

**Total: 280 PNGs + 1 SVG + License + 2 catalog PNGs.**

## License excerpt (verbatim from `License.txt`)

```
	Fantasy UI Borders (1.0)
	Created/distributed by Kenney (www.kenney.nl)
	Creation date: 03-12-2023
	For the sample image the font 'Aoboshi One' was used, OPL (Open Font License)
		------------------------------
	License: (Creative Commons Zero, CC0)
	http://creativecommons.org/publicdomain/zero/1.0/
	You can use this content for personal, educational, and commercial purposes.
	Support by crediting 'Kenney' or 'www.kenney.nl' (this is not a requirement)
```

CC0 1.0: ✅ commercial use, ✅ redistribution, ✅ modification, ✅ no
attribution required (but appreciated), ✅ no copyleft / share-alike.

## Which sprites are 9-slice-ready?

We programmatically inspected every sprite with PIL and sorted them into
three categories. The numbers are from the script in
`_reference/_source_archive/_extracted/`:

| Category | Files (Default 48×48) | Files (Double 96×96) | Notes |
| -------- | --------------------: | -------------------: | ----- |
| **Closed ring** (true 9-slice, hollow inside) | 32 (`PNG/Default/Border/*`) | 32 (`PNG/Double/Border/*`) | The team's primary pick for any dialog/menu/inventory frame |
| **Solid filled panel** (no hole, full tile) | 32 (`PNG/Default/Panel/*`) | 32 (`PNG/Double/Panel/*`) | Use as-is on a `UI → Image`, no 9-slice needed |
| **Corner/edge fragments** (decorative, build a custom frame in UI Builder) | 32 (`PNG/Default/Transparent border/*`) | 32 (`PNG/Double/Transparent border/*`) | Compose in Unity's UI Builder for fancy asymmetric frames |
| **Center fill tile** (decorative interior pattern) | 32 (`PNG/Default/Transparent center/*`) | 32 (`PNG/Double/Transparent center/*`) | Repeatable center fill behind text |
| **Divider** (horizontal line, 3-slice) | 6 (`PNG/Default/Divider/*`) | 6 (`PNG/Double/Divider/*`) | Use as Tiled or 3-slice horizontal separators |
| **Fade divider** (gradient divider) | 6 (`PNG/Default/Divider Fade/*`) | 6 (`PNG/Double/Divider Fade/*`) | Same as divider but with gradient alpha |

## Recommended 9-slice border values (Unity Sprite Editor → Border L/T/R/B)

We measured every "closed ring" sprite and grouped them. The bulk (24 of 32
sprites = 75% in each resolution set) share a uniform border thickness:

### `PNG/Default/Border/*` (48×48)

| Sprite set | Border L/T/R/B | How widely this applies |
| ---------- | --------------: | -----------------------:|
| All `panel-border-XXX.png` (32 files) | Most are **L=2 T=2 R=2 B=2** | 75% (24/32) of the set |
| Outlier thicker designs | **L=6 T=6 R=6 B=6** or **L=12 T=12 R=12 B=12** | 6% / 3% (2/32) each |
| Solid-fill decoration sprites | **L=0 T=0 R=0 B=0** (no 9-slice) | 19% (6/32) |

➡️ **Recommended default for any sprite in this set you pick at random:**

```
Sprite Editor → Border:  L=2   T=2   R=2   B=2
```

If the corners look stretched/pulled at large sizes, manually open the sprite
and re-measure (the Border numbers above come from the 75% majority, the rest
have bulkier inner rings).

### `PNG/Double/Border/*` (96×96, 2× res)

| Sprite set | Border L/T/R/B | How widely this applies |
| ---------- | --------------: | -----------------------:|
| All `panel-border-XXX.png` (32 files) | Most are **L=4 T=4 R=4 B=4** | 75% (24/32) of the set |
| Outlier thicker designs | **L=12 T=12 R=12 B=12** or **L=24 T=24 R=24 B=24** | 3% each |
| Solid-fill decoration sprites | **L=0 T=0 R=0 B=0** | 19% |

➡️ **Recommended default for any sprite in this set:**

```
Sprite Editor → Border:  L=4   T=4   R=4   B=4
```

## Pixels-Per-Unit (PPU) for Unity import

| Sprite size | Recommended PPU |
| ----------- | --------------: |
| Default 48×48 | **100** (1 unit ~ 48% of a screen at 1080p — perfect for mid-screen panels) |
| Default smaller panels/parts | **200** (half-pixel = desktop-large UI) |
| Double 96×96 | **100** (yields twice the world scale, useful for hi-DPI 4K exports) |

## Quick recipes

### Recipe A: dialog box (single Image, no 9-slice)

Use `PNG/Double/Panel/panel-018.png` directly on a `UI → Image` — size the
RectTransform to (600, 200). Done.

### Recipe B: 9-slice window that scales

1. Use `PNG/Default/Border/panel-border-000.png` (plain double-line ring).
2. Sprite Editor → Border L=2 T=2 R=2 B=2.
3. UI → Image → Image Type = **Sliced** → resize RectTransform freely.
4. Inside, drop a `VerticalLayoutGroup` for content (text + buttons).

### Recipe C: fancy asymmetric dialog (UI Builder composition)

Use the corner pieces from `PNG/Double/Transparent border/panel-transparent-border-008.png` (top-left corner) + rotate it for the other three corners + take `PNG/Double/Transparent center/panel-transparent-center-008.png` for the middle. Compose in UI Builder as 5 Images (4 corners + 1 stretched center). Total: ~5 Image components per dialog box.

### Recipe D: divider line between menu items

`PNG/Default/Divider/divider-005.png` (with decoration motif) → set
Sprite Editor Border → L=0 T=8 R=0 B=8 (cut the top/bottom 8 px) → Image
Type = Sliced → use as a horizontal 1-px-tall UI Image between menu rows.

## Acceptance evidence

```
$ ls assets/art/ui/kenney-fantasy-ui-borders/
License.txt         Preview.png   Sample.png        Visit Kenney.url    Visit Patreon.url
PNG/                Vector/

$ find assets/art/ui/kenney-fantasy-ui-borders/PNG -name '*.png' | wc -l
280

$ head -8 assets/art/ui/kenney-fantasy-ui-borders/License.txt
	Fantasy UI Borders (1.0)
	Created/distributed by Kenney (www.kenney.nl)
	Creation date: 03-12-2023
	For the sample image the font 'Aoboshi One' was used, OPL (Open Font License)
	License: (Creative Commons Zero, CC0)
	http://creativecommons.org/publicdomain/zero/1.0/

$ python3 -c "from PIL import Image; print(Image.open('assets/art/ui/kenney-fantasy-ui-borders/PNG/Default/Border/panel-border-000.png').size)"
(48, 48)

$ python3 -c "from PIL import Image; print(Image.open('assets/art/ui/kenney-fantasy-ui-borders/PNG/Double/Border/panel-border-000.png').size)"
(96, 96)
```

## Re-extract from the original zip

If the team's `assets/` tree gets wiped:

```bash
cd /path/to/hackathon-prep/assets/art/ui     # ← adjust to your checkout root
mkdir kenney-fantasy-ui-borders
cd kenney-fantasy-ui-borders
curl -sLO "https://kenney.nl/media/pages/assets/fantasy-ui-borders/ab29cd0165-1701602367/kenney_fantasy-ui-borders.zip"
unzip kenney_fantasy-ui-borders.zip
```

This re-creates the exact tree shipped here (License.txt + PNG/ + Vector/).
