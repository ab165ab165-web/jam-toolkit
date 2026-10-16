# HP / Progress Bar Recipe (no new download — uses Kenney UI Pack 2.0)

> Closes gap **N2**. The team already shipped Kenney UI Pack 2.0 in
> `assets/art/ui/kenney-ui-pack-png/` (CC0). That pack contains slider art
> (`slide_*`) that composes into a health / progress / loading bar.
> **No new asset download required.**

## Sprite inventory (5 pieces × 6 colour themes × 2 resolutions)

| Sprite | Size (Default) | Size (Double) | Purpose |
| ------ | -------------- | ------------- | ------- |
| `slide_horizontal_grey.png` | 96 × 16 | 192 × 32 | **Empty track (background)** |
| `slide_horizontal_grey_section.png` | 16 × 16 | 32 × 32 | Fill end-cap (rounded) |
| `slide_horizontal_grey_section_wide.png` | 32 × 16 | 64 × 32 | **Fill tile (stretchable middle)** |
| `slide_horizontal_color.png` | 96 × 16 | 192 × 32 | Alternate "colored" track (for full HP) |
| `slide_horizontal_color_section.png` | 16 × 16 | 32 × 32 | Fill end-cap (color) |
| `slide_horizontal_color_section_wide.png` | 32 × 16 | 64 × 32 | Fill tile (color) |
| `slide_hangle.png` | 24 × 32 | 48 × 64 | Slider **handle** (omit for HP bar) |

(There is also a `slide_vertical_*` set for vertical bars — same naming,
just transpose the coordinates. The team should default to horizontal.)

Six colour themes: `Blue/`, `Green/`, `Grey/`, `Red/`, `Yellow/`, `Extra/` (Extras include patterned/artistic variants; HP bars come from the 5 solid-color themes above). Per-colour subdirectories `Default/` (1×) and `Double/` (2×) exist in each. **Path:**
`assets/art/ui/kenney-ui-pack-png/<color>/<Default\|Double>/slide_horizontal_*.png`.

### Recommended colour for HP bars

- `Red/` for **low HP / boss HP / enemy HP** (red feels dangerous)
- `Green/` for **player HP** (RPG standard)
- `Yellow/` for **stamina / mana / sanity**
- `Blue/` for **shield / armor / magic-mana**
- `Grey/` for **XP / generic "value" bar** (also plays well in any UI context)

## Unity setup (5-min recipe)

### Step 1 — Import the three sprites

Drag the three sprite PNGs into `Assets/Textures/UI/HPBar/`:
- `slide_horizontal_<color>_<resolution>.png` (track)
- `slide_horizontal_<color>_section_wide_<resolution>.png` (fill tile, the wider one — better for tiling)

### Step 2 — Configure both as 9-slice

For each sprite:
- **Texture Type = Sprite (2D and UI)**
- **Sprite Mode = Single**
- **Pixels Per Unit = 100** for Default, **200** for Double (gives 1:1 world scale)
- **Filter Mode = Point (no filter)** — these are pixel-art; bilinear will blur them
- **Compression = None** (keep crisp)

Then **Sprite Editor** → drag the four green border handles inset by **2 px each** (Default) or **4 px each** (Double). The visible rounded ends of the track and the tile endcaps need to remain undistorted; the inner content stretches.

| Sprite | Sprite Editor border L/T/R/B |
| ------ | -----:|
| `slide_horizontal_grey.png` (Default) | **L=2  T=2  R=2  B=2** (16-px tall track, edge=2) |
| `slide_horizontal_grey_section_wide.png` (Default) | **L=2  T=2  R=2  B=2** (same height) |
| Double set | **L=4  T=4  R=4  B=4** (32-px tall track, edge=4) |

### Step 3 — Build the GameObject hierarchy

```
Canvas (Screen Space - Overlay)
└── HealthBar (RectTransform 200×24)
    ├── Track (Image, sprite=slide_horizontal_grey, image-type=Sliced, Color=#303030)
    └── Fill (Image, sprite=slide_horizontal_grey_section_wide, image-type=Sliced,
              Color=#FF4444, Anchor Min=(0,0), Anchor Max=(1,1) — full-rect child,
              LEFT PAD 2 px, TOP PAD 2 px, RIGHT PAD 2 px, BOTTOM PAD 2 px)
```

The Track provides the empty-container "groove"; the Fill sits on top with its own RectTransform width = `(track_width - 4) * hp_percentage`.

### Step 4 — Update at runtime

```csharp
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;             // the inner colored Fill image
    public float maxHp = 100f;
    public float hp = 80f;

    public void SetHP(float value)
    {
        hp = Mathf.Clamp(value, 0f, maxHp);
        // Use anchor min/max trick — keeps the Fill stretched by % without
        // changing aspect ratio or pixel-perfect drift.
        fill.rectTransform.anchorMin = new Vector2(0f, 0f);
        fill.rectTransform.anchorMax = new Vector2(hp / maxHp, 1f);
        fill.rectTransform.offsetMin = new Vector2(2f, 2f);
        fill.rectTransform.offsetMax = new Vector2(-2f, -2f);
    }

    void OnValidate() => SetHP(hp);
}
```

The 2-px inset (`offsetMin/Max`) keeps the Fill clear of the Track's outer
ring — it shows the colored fill inside the empty groove.

### Step 5 (optional) — Smooth tween on damage

Wire to the LitMotion tween library (gap G1, MIT, in `open-assets/tween/`):

```csharp
using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public void SetHPSmooth(float value)
{
    var startRatio = fill.rectTransform.anchorMax.x;
    var endRatio = Mathf.Clamp01(value / maxHp);
    LMotion.Create(startRatio, endRatio, 0.25f)
        .WithEase(Ease.OutQuad)
        .Bind(r =>
        {
            fill.rectTransform.anchorMax = new Vector2(r, 1f);
        });
}
```

(That's `transform.DOAnchorMax(...)` in DOTween — see the full
`DOTween → LitMotion translation table` in `open-assets/tween/INTEGRATION.md`.)

## Variants

### Boss bar / enemy bar (Red)

Same recipe, just swap `Green/` theme in for the Fill sprite and color #FF3030.

### MP / Stamina / Sanity bar (Blue / Yellow)

Same recipe, swap the colour folder.

### Vertical bar (Slide vertical set)

Use `slide_vertical_*.png` series (96×16 → transposed to 16×96 for Default).
Set Fill's anchorMin = `(0, 0)`, anchorMax = `(1, hp/maxHp)` so it grows
bottom-up. Same Sprite Editor borders (L=2 T=2 R=2 B=2) apply.

### Segmented HP bar (mario/zelda style)

Use `slide_horizontal_<color>_section_wide.png` (the 32×16 fill tile) at
exact multiples of its width — i.e., set Fill RectTransform width to
`n * 32 + n * 2` for n segments. Each Fill instance is sized to a whole
number of "tile + gap" units. Mark the sprite as Tiled instead of Sliced
for crisper pixel boundaries.

### Multi-color HP bar (e.g., armor + health stacked)

Two Fill Images stacked on top of each other:
- Inner: armor (Blue)
- Outer: HP (Green or Red)

Set outer alpha to 1.0, inner alpha to 0.85 for a subtle layered look.

## Acceptance evidence

```
$ ls assets/art/ui/kenney-ui-pack-png/Green/Default/ | grep '^slide_horizontal'
slide_horizontal_color.png
slide_horizontal_color_section.png
slide_horizontal_color_section_wide.png
slide_horizontal_grey.png
slide_horizontal_grey_section.png
slide_horizontal_grey_section_wide.png

$ ls assets/art/ui/kenney-ui-pack-png/Green/Double/ | grep '^slide_horizontal'
slide_horizontal_color.png
... (same 6 PNGs, 2× resolution)

$ python3 -c "from PIL import Image; im=Image.open('assets/art/ui/kenney-ui-pack-png/Green/Double/slide_horizontal_grey.png'); print(im.size, im.mode)"
(192, 32) P
```

### License

Kenney UI Pack 2.0 is CC0 1.0. Already added to `assets/LICENSES.md` in the
original prep pass — no new license bookkeeping needed for this gap.
