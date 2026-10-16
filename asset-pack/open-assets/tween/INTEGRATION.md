# Tween Library — LitMotion (MIT, DOTween replacement)

> **TL;DR**: LitMotion is MIT-licensed (unlike DOTween which is "free but no
> redistribution"). Install via UPM by adding the Git URL to your
> `Packages/manifest.json`. It's a single line of code per tween, allocation-free,
> works with Unity 2022.3 / 团结 2022.3.

## Why LitMotion over DOTween / PrimeTween / LeanTween

| Lib | License | Maintainer | Notes |
| --- | ------- | ---------- | ----- |
| **LitMotion** | **MIT** ✓ | annulusgames, 2.3k stars, pushed Sept 2026 | ✅ pick this |
| LeanTween | MIT ✓ | Dented Pixel, last push 2021-12 (stale) | OK but unmaintained |
| PrimeTween | Custom (no redistribution, similar to DOTween) | Kyrylo Kuzyk | ❌ can't bundle in our pack |
| DOTween | Custom (free for use, **no redistribution**, you can't bundle the source/.tgz) | Demigiant | ❌ user already flagged this |

LitMotion wins on license (real MIT, can redistribute via UPM), on speed
("Lightning-fast and Zero Allocation Tween Library for Unity" — official
README), and on freshness (Sept 2026 release, vs 2021 for LeanTween).

## Install (UPM, 1 line)

Add to your `<unity-project>/Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.unity.modules.image": "1.0.0",
    "com.annulusgames.lit-motion": "https://github.com/annulusgames/LitMotion.git?path=/src/LitMotion/Assets/LitMotion#2.0.2"
  }
}
```

> **Why `com.annulusgames.lit-motion`?** That's the package name in
> `LitMotion/src/LitMotion/Assets/LitMotion/package.json` (verified
> 2026-10-15). The earlier draft of this doc used `com.lit-motion` which is
> wrong — UPM will reject it. The version is pinned to tag `2.0.2`.

(Or in the editor: `Window → Package Manager → + → Add package from git URL`
→ paste the URL above. Pin the `2.0.2` tag for stability.)

The full source we cloned (in `open-assets/tween/LitMotion/`) is here for
**reference** — you do NOT need to copy it manually into the Unity project,
UPM handles it. The folder is only here so you can grep/inspect without a
network call.

## API examples (the 4 you'll actually use)

```csharp
using LitMotion;
using UnityEngine;

public class JuiceTweens : MonoBehaviour
{
    public Transform target;
    public Camera cam;

    void Start()
    {
        // 1) Position punch
        LMotion.Create(Vector3.zero, new Vector3(2, 2, 0), 0.3f)
            .WithEase(Ease.OutBack)
            .BindToPosition(target);

        // 2) Color flash on camera BACKGROUND
        //    NOTE: BindToColor only exists for SpriteRenderer / Graphic / TMP_Text
        //          — for the camera background you need BindToBackgroundColor.
        LMotion.Create(Color.red, Color.white, 0.2f)
            .WithEase(Ease.OutQuad)
            .BindToBackgroundColor(cam);

        // 3) Scale bounce (looping)
        LMotion.Create(Vector3.one, new Vector3(1.2f, 1.2f, 1.2f), 0.6f)
            .WithEase(Ease.InOutSine)
            .WithLoops(-1, LoopType.Yoyo)
            .BindToLocalScale(target);

        // 4) Sequence (chain 3 tweens)
        //    LSequence.Create() returns a MotionSequenceBuilder; the terminator
        //    is .Run() (NOT .Play()), and it returns MotionHandle. The motion
        //    starts immediately when Run() is called.
        var seqHandle = LSequence.Create()
            .Append(LMotion.Create(transform.position, transform.position + Vector3.up * 2f, 0.2f).BindToPosition(transform))
            .Append(LMotion.Create(transform.position, transform.position + Vector3.right * 2f, 0.3f).BindToPosition(transform))
            .Append(LMotion.Create(transform.position, Vector3.zero, 0.4f).BindToPosition(transform))
            .Run();
        // (Cancel the sequence later via seqHandle.Cancel() — see MotionHandleExtensions.)
    }

    // Stop all when destroyed.
    // IMPORTANT: MotionManager is `internal static` in v2.0.2 (Runtime/Internal/MotionManager.cs:7)
    // and exposes NO public `CancelAll()`. The public cancel API is per-handle:
    //   motionHandle.Cancel();   // MotionHandleExtensions.cs:106
    // So instead of "CancelAll", store each MotionHandle you create and cancel it on destroy.
    void OnDestroy()
    {
        // store handles in fields; nothing to do at OnDestroy if you used
        // `.AddTo(gameObject)` — LitMotion auto-cancels on GameObject destroy.
    }
}
```

## Use it INSTEAD of DOTween for these scripts

If you grab the existing `scripts/JuiceManager.cs` and want to swap DOTween →
LitMotion (recommended — DOTween is non-redistributable, we can't ship it
pre-installed):

```csharp
// Before (DOTween):
// transform.DOShakePosition(0.3f, 0.5f).SetEase(Ease.OutQuad);

// After (LitMotion):
// Note: there is no `LMotion.Shake.Create(0f, new Vector3(...), duration)`
// overload — the Vector3 overload signature is
// `LMotion.Shake.Create(Vector3 startValue, Vector3 strength, float duration)`
// (verified `Runtime/LMotion.Shake.cs:46`). The `float` overload
// (`Create(float startValue, float strength, float duration)`) is per-axis.
LMotion.Shake.Create(Vector3.zero, new Vector3(0.5f, 0.5f, 0f), 0.3f)
    .WithEase(Ease.OutQuad)
    .BindToPosition(transform);
```

The translation is mechanical — every DOTween method has a LitMotion
equivalent (`DOLocalMove` → `BindToLocalPosition`, `DORotate` → `BindToRotation`,
`DOScale` → `BindToLocalScale`, etc.). See the LitMotion docs:
https://annulusgames.github.io/LitMotion/

## DOTween → LitMotion API translation table (team copy-paste reference)

Mechanical one-to-one mapping. **Verified against LitMotion v2.0.2 source**
(this folder contains a shallow clone of
`https://github.com/annulusgames/LitMotion` at tag `v2.0.2`; every row below
cites the source file:line of the actual LitMotion API).

DOTween API column is the left-hand string the team is likely to find in
`JuiceManager.cs` / tutorials / StackOverflow snippets; LitMotion column is
the read-and-replace name. `LMotion.Create` is the entry-point;
`.BindTo...` (or `.RunWithoutBinding()` for fire-and-forget) is the
**terminator** that materializes the motion and returns a `MotionHandle`.

> **Reading the source-line column**: `path:line` references inside the
> cloned LitMotion tree at `open-assets/tween/LitMotion/src/LitMotion/Assets/LitMotion/`.

### Transform — position / rotation / scale

| DOTween | LitMotion | Notes / Source |
| ------- | --------- | -------------- |
| `transform.DOMove(target, 0.5f)` | `LMotion.Create(transform.position, target, 0.5f).BindToPosition(transform)` | World-space. Source: `Runtime/Extensions/General/LitMotionTransformExtensions.cs:18` |
| `transform.DOLocalMove(target, 0.5f)` | `LMotion.Create(transform.localPosition, target, 0.5f).BindToLocalPosition(transform)` | Parent-relative. Source: `LitMotionTransformExtensions.cs:166` |
| `transform.DOMoveX(x, 0.5f)` | `LMotion.Create(transform.position.x, x, 0.5f).BindToPositionX(transform)` | Source: `LitMotionTransformExtensions.cs:37` |
| `transform.DOMoveY(y, 0.5f)` | `LMotion.Create(transform.position.y, y, 0.5f).BindToPositionY(transform)` | Source: `LitMotionTransformExtensions.cs:58` |
| `transform.DOMoveZ(z, 0.5f)` | `LMotion.Create(transform.position.z, z, 0.5f).BindToPositionZ(transform)` | Source: `LitMotionTransformExtensions.cs:79` |
| `transform.DORotate(target, 0.5f)` | `LMotion.Create(transform.eulerAngles, target, 0.5f).BindToEulerAngles(transform)` | Euler angles, world. Source: `LitMotionTransformExtensions.cs:353` |
| `transform.DORotateQuaternion(rot, 0.5f)` | `LMotion.Create(transform.rotation, rot, 0.5f).BindToRotation(transform)` | Quaternion, world. Source: `LitMotionTransformExtensions.cs:315` |
| `transform.DOLocalRotate(rot, 0.5f)` | `LMotion.Create(transform.localRotation, rot, 0.5f).BindToLocalRotation(transform)` | Source: `LitMotionTransformExtensions.cs:334` |
| `transform.DOLookAt(pos, 0.5f)` | `LMotion.Create(transform.eulerAngles, Quaternion.LookRotation(pos - transform.position).eulerAngles, 0.5f).BindToLocalEulerAngles(transform)` | Manual; verify axis mode. Source: `LitMotionTransformExtensions.cs:501` |
| `transform.DOScale(target, 0.5f)` | `LMotion.Create(transform.localScale, target, 0.5f).BindToLocalScale(transform)` | Source: `LitMotionTransformExtensions.cs:649` |
| `transform.DOScaleX(x, 0.5f)` | `LMotion.Create(transform.localScale.x, x, 0.5f).BindToLocalScaleX(transform)` | Per-axis helper exists. Source: `LitMotionTransformExtensions.cs:668` |
| `transform.DOShakePosition(0.3f, 0.5f)` | `LMotion.Shake.Create(Vector3.zero, new Vector3(0.5f,0.5f,0f), 0.3f).BindToPosition(transform)` | Built-in helper. **No `(float, Vector3, float)` overload exists** — only `(float, float, float)`, `(Vector2, Vector2, float)`, `(Vector3, Vector3, float)` (see `Runtime/LMotion.Shake.cs:46`). Use `(Vector3.zero, vec, dur)`. |
| `transform.DOShakeRotation(0.3f, 45f)` | `LMotion.Shake.Create(Vector3.zero, new Vector3(0f,0f,45f), 0.3f).BindToEulerAngles(transform)` | Shake uses absolute Euler delta. Source: `LMotion.Shake.cs:46` |
| `transform.DOShakeScale(...)` | **No built-in helper** — compose with 3 separate `LMotion.Create` + per-axis random (`Random.insideUnitSphere`) | Verified: no Shake overload for `Vector3` + Scale |

**Note A** — LitMotion provides per-axis scale helpers (`.BindToLocalScaleX/Y/Z/XY/XZ/…`),
so a Vector3 math dance is NOT required. (Earlier draft of this doc said it was;
that was wrong. Source: `LitMotionTransformExtensions.cs:668–820`.)

### UI / SpriteRenderer / Camera — color & alpha

| DOTween | LitMotion | Notes / Source |
| ------- | --------- | -------------- |
| `img.DOFade(0f, 0.3f)` | `LMotion.Create(img.color.a, 0f, 0.3f).BindToColorA(img)` | `img : Graphic`. Source: `Runtime/Extensions/uGUI/LitMotionUGUIExtensions.cs:106` |
| `img.DOFillAmount(1f, 0.5f)` | `LMotion.Create(img.fillAmount, 1f, 0.5f).BindToFillAmount(img)` | **There IS a built-in `BindToFillAmount`** for `Image`. Source: `Runtime/Extensions/uGUI/LitMotionUGUIExtensions.cs:127`. (Use raw `Bind(x => img.fillAmount = x)` only if you can't reference the UGUI extensions assembly.) |
| `img.DOColor(Color.red, 0.3f)` | `LMotion.Create(img.color, Color.red, 0.3f).BindToColor(img)` | Source: `LitMotionUGUIExtensions.cs:24` |
| `sr.DOFade(0f, 0.3f)` | `LMotion.Create(sr.color.a, 0f, 0.3f).BindToColorA(sr)` | Source: `Runtime/Extensions/General/LitMotionSpriteRendererExtensions.cs:100` |
| `sr.DOColor(Color.red, 0.3f)` | `LMotion.Create(sr.color, Color.red, 0.3f).BindToColor(sr)` | Source: `LitMotionSpriteRendererExtensions.cs:18` |
| `cam.DOColor(Color.red, 0.3f)` | `LMotion.Create(cam.backgroundColor, Color.red, 0.3f).BindToBackgroundColor(cam)` | **NOT `BindToColor`** — Camera has `BindToBackgroundColor`. Source: `Runtime/Extensions/General/LitMotionCameraExtensions.cs:151` |
| `light.DOIntensity(2f, 0.3f)` | `LMotion.Create(light.intensity, 2f, 0.3f).Bind(x => light.intensity = x)` | **No `BindToIntensity` exists in v2.0.2** — use raw `Bind`. Verified: no `LitMotionLightExtensions.cs` in `Runtime/Extensions/General/`. Works for URP `Light2D` (same `Light.intensity` field). |

> **Note**: `BindToColor*` (no `*`) on `Image`, `SpriteRenderer`,
> `Material`, `LineRenderer`, `TMP_Text`, etc. — confirmed at
> `Runtime/Extensions/uGUI/LitMotionUGUIExtensions.cs:24`,
> `Runtime/Extensions/General/LitMotionSpriteRendererExtensions.cs:18`,
> `Runtime/Extensions/TextMeshPro/LitMotionTextMeshProExtensions.cs:178`.
> LitMotion ships an extension for each — there's `BindToColor`,
> `BindToColorA` (alpha-only), `BindToColorR/G/B` (channel-only), plus
> `BindToBackgroundColor` for Camera (verified
> `Runtime/Extensions/General/LitMotionCameraExtensions.cs:151`).

### CanvasGroup / RectTransform — alpha & anchored position

| DOTween | LitMotion | Notes / Source |
| ------- | --------- | -------------- |
| `cg.DOFade(0f, 0.3f)` | `LMotion.Create(cg.alpha, 0f, 0.3f).BindToAlpha(cg)` | Source: `Runtime/Extensions/uGUI/LitMotionUGUIExtensions.cs:146` |
| `rt.DOAnchorPos(target, 0.5f)` | `LMotion.Create(rt.anchoredPosition, target, 0.5f).BindToAnchoredPosition(rt)` | Source: `Runtime/Extensions/uGUI/LitMotionRectTransformExtensions.cs:18` |
| `rt.DOSizeDelta(s, 0.5f)` | `LMotion.Create(rt.sizeDelta, s, 0.5f).BindToSizeDelta(rt)` | Source: `LitMotionRectTransformExtensions.cs:200` |
| `rt.DOPivot(v, 0.5f)` | `LMotion.Create(rt.pivot, v, 0.5f).BindToPivot(rt)` | Source: `LitMotionRectTransformExtensions.cs:261` |
| `rt.DOAnchorMin(...)` / `DOAnchorMax(...)` | `.BindToAnchorMin(rt)` / `.BindToAnchorMax(rt)` | Source: `LitMotionRectTransformExtensions.cs:161, 180` |

### Sequence / chain

| DOTween | LitMotion | Notes / Source |
| ------- | --------- | -------------- |
| `DOTween.Sequence().Append(a).Append(b)` | `LSequence.Create().Append(aHandle).Append(bHandle).Run()` | **Terminator is `.Run()`, NOT `.Play()`**. `.Run()` returns `MotionHandle`. Source: `Runtime/MotionSequenceBuilder.cs:152` |
| `DOTween.Sequence().Append(a).Join(b)` | `.Append(aHandle).Append(bHandle).Run()` (sequential) OR `LSequence.Create().Append(aHandle).Insert(0f, bHandle).Run()` (parallel) | `.Join()` also exists (`MotionSequenceBuilder.cs:144`) and runs in parallel with the preceding `.Append`. Verified: Insert(0,b) and Join(b) are both parallel-with-preceding-Append. |
| `DOTween.Sequence().AppendInterval(0.5f)` | `LSequence.Create().AppendInterval(0.5f)` | Source: `MotionSequenceBuilder.cs:128` |
| `DOTween.Sequence().AppendCallback(() => f())` | **No `AppendCallback` in LitMotion v2.0.2** — verified empty `grep -rn AppendCallback Runtime/` in the cloned tree. Use one of: (a) schedule a zero-duration motion and bind a side-effect: `LSequence.Create().Append(LMotion.Create(0f, 0f, 0f).Bind(_ => f()))`, (b) fire the call manually after the sequence completes by storing the `MotionHandle` from `.Run()` and awaiting/cancelling, or (c) use a Tween/TMP callback on the bound property. |
| `tween.SetEase(Ease.OutBounce)` | `LMotion.Create(...).WithEase(Ease.OutBounce)` | Same enum (LitMotion re-exports `Ease`). Source: `Runtime/MotionBuilder.cs:134` |
| `tween.SetLoops(-1, LoopType.Yoyo)` | `LMotion.Create(...).WithLoops(-1, LoopType.Yoyo)` | Source: `MotionBuilder.cs:180` |
| `tween.SetDelay(0.2f)` | `LMotion.Create(...).WithDelay(0.2f)` | Source: `MotionBuilder.cs:164` |
| `tween.SetUpdate(true)` (use unscaled) | `LMotion.Create(...).WithScheduler(MotionScheduler.UpdateIgnoreTimeScale).BindTo...` | **`WithUpdate(Settings.UpdateMode.UnscaledTime)` does NOT exist in v2.0.2**. Use `WithScheduler` + `MotionScheduler.UpdateIgnoreTimeScale`. Source: `MotionBuilder.cs:272` + `Runtime/MotionScheduler.cs:71` (the `UpdateIgnoreTimeScale` static field is at `:71`, not `:58` — verified against the cloned tree) |
| `DOTween.KillAll()` | **No public `CancelAll` API.** Per-handle: keep references to your `MotionHandle`s and call `.Cancel()` on each. | Verified: `MotionManager` is `internal static` (`Runtime/Internal/MotionManager.cs:7`); public cancel is `MotionHandle.Cancel()` (`Runtime/MotionHandleExtensions.cs:106`). Auto-cancel on GameObject destroy: use `.AddTo(gameObject)` (`MotionHandleExtensions.cs:138`). |
| `tween.Pause()` / `.Play()` | **Not exposed on `MotionHandle` in v2.0.2.** | Verified: no `Pause`/`Play` in `Runtime/MotionHandleExtensions.cs`. Pause/resume can be implemented by `Cancel` + re-`Create`. |
| `DOTween.To(() => x, v => x = v, 1f, 0.3f)` | `LMotion.Create(0f, 1f, 0.3f).Bind(x => myField = x)` | Source: `MotionBuilder.cs:311` |
| `DOTween.ToArray()` | Not built-in. Iterate your own `MotionHandle` references and call `.Cancel()`. | (Same reason — no `MotionManager.CancelAll`.) |
| `DOTween.timeScale` | Not directly exposed. Use `WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)` per motion, or write your own `IMotionScheduler` that scales delta-time. | `MotionScheduler` provides `Update`, `UpdateIgnoreTimeScale`, `UpdateRealtime` etc. Source: `Runtime/MotionScheduler.cs` |

### Quick sanity test (copy into a scratch MonoBehaviour and press Play)

```csharp
using LitMotion;
using UnityEngine;

public class LitMotionSmokeTest : MonoBehaviour
{
    void Start()
    {
        // 1) move + scale + fade at once (3 parallel tweens)
        LMotion.Create(Vector3.zero, new Vector3(2, 2, 0), 0.4f)
            .WithEase(Ease.OutBack)
            .BindToPosition(transform);
        LMotion.Create(Vector3.one, new Vector3(1.4f, 1.4f, 1.4f), 0.4f)
            .WithEase(Ease.OutBack)
            .BindToLocalScale(transform);
        var sr = GetComponent<SpriteRenderer>();
        if (sr) LMotion.Create(sr.color.a, 0f, 0.4f).BindToColorA(sr);

        // 2) sequence — terminator is .Run(), returns MotionHandle
        //    NOTE: there is no `AppendCallback` on `MotionSequenceBuilder` in
        //    LitMotion v2.0.2. We emulate the callback by appending a
        //    zero-duration motion whose side-effect is the log call.
        var seqHandle = LSequence.Create()
            .Append(LMotion.Create(transform.position, transform.position + Vector3.up * 2f, 0.2f).BindToPosition(transform))
            .Append(LMotion.Create(transform.position, transform.position + Vector3.right * 2f, 0.3f).BindToPosition(transform))
            .Append(LMotion.Create(0f, 0f, 0f).Bind(_ => Debug.Log("chain done")))
            .Run();
    }
}
```

If the transform moves up-and-right + scales up + alpha-fades, the install is correct.

## License

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

Full text: https://github.com/annulusgames/LitMotion/blob/master/LICENSE
(also at `open-assets/tween/LitMotion/LICENSE` in this prep pack).

## Files we cloned

- `LitMotion/LICENSE` (MIT, full text above)
- `LitMotion/README.md`, `README_JA.md`
- `LitMotion/src/LitMotion/Assets/LitMotion/` — the actual runtime package
  (Editor/ + Runtime/ + Tests/ + package.json → `name: "com.annulusgames.lit-motion"`)
- `LitMotion/src/LitMotion/Assets/LitMotion.Animation/` — animation track
  binding extension (optional)
- `LitMotion/src/LitMotion/Assets/Plugins/` — collection extensions
- `LitMotion/samples/` — official LitMotion.Samples Unity project (reference;
  you do NOT need to copy this into your Unity project)
- (Note: the LitMotion `docs/` folder from upstream was NOT cloned; this prep
  pack references the GitHub Pages docs at https://annulusgames.github.io/LitMotion/
  instead, since Markdown docs would be dead weight inside a Unity project.)

Total unpacked (without `.git`): ~5 MB. Install via UPM is the recommended
path; the clone is here so you can grep `BindToPosition` etc. without
network. The upstream `.git/` directory has been removed from this clone
(`open-assets/tween/LitMotion/.git` → gone) — see `OPEN-ASSETS.md` Y6.