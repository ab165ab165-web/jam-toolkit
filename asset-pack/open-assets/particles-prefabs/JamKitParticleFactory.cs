// JamKitParticleFactory.cs
// SPDX-License-Identifier: MIT
// Copyright (c) 2026 hackathon-prep contributors
//
// Runtime factory that produces "prefab-equivalent" ParticleSystem GameObjects
// for the 4 jam-essential effects (hit sparks, landing dust, death explosion,
// collectible glow), using the Kenney Particle Pack PNGs already in
// assets/art/particles/kenney-particle-pack/png/ (CC0).
//
// Why a factory instead of .prefab files?
//   - .prefab files reference textures by GUID + fileID. GUIDs are only
//     generated at first Unity import, so we cannot pre-write prefabs that
//     load reliably outside the editor's first-pass import. (This is why
//     every sample .prefab in the InputSystem repo ships with a sibling
//     .meta file — you cannot ship a single .prefab in isolation.)
//   - The MIT Sample Project Boost "Success Particles.prefab" is included
//     here as a *structural reference* so you can see the full
//     ParticleSystem YAML format and copy settings in the inspector.
//   - At runtime / when you instantiate these factories, Unity assigns a
//     stable GUID, so this works as a "live prefab" — feed it through
//     ObjectPool.cs to get a real pooled spawner.
//
// Texture mapping (Kenney CC0 → effect):
//   Hit sparks        → muzzle_01.png  (small radial flare)
//   Landing dust      → dirt_01.png    (brown blob, looks like dust)
//   Death explosion   → magic_03.png   (sharp burst)
//   Collectible glow  → flare_01.png   (soft golden glow)
//
// All 4 are in: Assets/art/particles/kenney-particle-pack/png/
// Set Kenney Import Settings in inspector (Texture Type = Default, Alpha = true,
// Mesh Type = Full Rect, Wrap = Clamp, Filter = Bilinear).
//
// DUAL-RENDER-PIPELINE SUPPORT (2026-10-16 G2 fix):
//   Detects the active Render Pipeline at runtime via
//   `UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline`.
//
//   - If non-null (URP / HDRP / custom SRP) → uses
//     Shader.Find("Universal Render Pipeline/Particles/Unlit") +
//     properties `_BaseColor` (Color tint) and `_BaseMap` (Texture).
//     The URP Particles/Unlit shader does NOT honor _TintColor / _Mode.
//     To get additive blending on URP, we set keyword `_SURFACE_TYPE_TRANSPARENT`
//     and `material.SetFloat("_Blend", 2f)` (2 = Additive).
//
//   - If null (Built-in Render Pipeline) → falls back to
//     Shader.Find("Particles/Standard Unlit") + properties `_TintColor` (Color)
//     and `_Mode` (Float: 0=Opaque, 1=Cutout, 2=Fade, 3=Transparent).
//     For additive blending in Built-in we use `_Mode = 3` + a second pass
//     triggered by SetFloat("_SrcBlend", (float)BlendMode.One) +
//     SetFloat("_DstBlend", (float)BlendMode.One) — Unity's default
//     particle additive setup.
//
//   Use `JamKitParticleFactory.IsUsingURP` to query which path is active.
//   The same factory works in either pipeline without recompilation.

using UnityEngine;
using UnityEngine.Rendering;

namespace JamKit
{
    /// <summary>
    /// Drop this on any GameObject or call its static helpers at startup to
    /// spawn the 4 jam-essential particle effects programmatically.
    /// </summary>
    public static class JamKitParticleFactory
    {
        public enum KenneyTex { Muzzle01, Dirt01, Magic03, Flare01 }

        // ----- shader names (both pipelines) -----
        private const string URPParticleShader       = "Universal Render Pipeline/Particles/Unlit";
        private const string BuiltInParticleShader  = "Particles/Standard Unlit";

        // ----- shader property IDs (cached for fast access) -----
        // URP
        private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
        private static readonly int BaseMapID   = Shader.PropertyToID("_BaseMap");
        private static readonly int SurfaceID   = Shader.PropertyToID("_Surface");
        private static readonly int BlendID     = Shader.PropertyToID("_Blend");
        // Built-in
        private static readonly int TintColorID = Shader.PropertyToID("_TintColor");
        private static readonly int ModeID      = Shader.PropertyToID("_Mode");
        private static readonly int SrcBlendID  = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlendID  = Shader.PropertyToID("_DstBlend");
        private static readonly int ZWriteID    = Shader.PropertyToID("_ZWrite");

        /// <summary>
        /// True when the active render pipeline is NOT the legacy default
        /// (i.e. URP, HDRP, or any custom ScriptableRenderPipeline asset).
        /// </summary>
        public static bool IsUsingURP
        {
            get
            {
                // GraphicsSettings.currentRenderPipeline is null when the
                // project is using the legacy Built-in render pipeline.
                // For URP, this returns the URP asset (UniversalRenderPipelineAsset).
                return GraphicsSettings.currentRenderPipeline != null;
            }
        }

        /// <summary>Returns texture path under Resources/ — wire your own
        /// Resources/Particles folder if you put the Kenney PNGs there.
        /// Default looks under Assets/art/particles/.../ which is non-Resources,
        /// so use the explicit overload below with an injected texture.</summary>
        public static ParticleSystem Create(Transform parent, KenneyTex tex, ParticleSystem prefabTemplate = null)
        {
            // Build a fresh empty GameObject; particle systems usually
            // want their own object so PlayOnAwake is clean.
            var go = new GameObject($"JamKit_{tex}");
            go.transform.SetParent(parent, worldPositionStays: false);
            var ps = go.AddComponent<ParticleSystem>();

            // --- main module ---
            var main = ps.main;
            main.duration = 0.5f;
            main.loop = false;
            main.playOnAwake = true;
            main.startLifetime = 0.4f;
            main.startSpeed = 4f;
            main.startSize = 0.4f;
            main.startColor = Color.white;
            main.maxParticles = 100;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.gravityModifier = 0.6f;

            // --- emission: burst ---
            var emission = ps.emission;
            emission.rateOverTime = 0;
            emission.SetBursts(new[] {
                new ParticleSystem.Burst(0f, 16)
            });

            // --- shape: small cone ---
            var shape = ps.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 25f;
            shape.radius = 0.1f;

            // --- size over lifetime: shrink ---
            var sol = ps.sizeOverLifetime;
            sol.enabled = true;
            var curve = new AnimationCurve(
                new Keyframe(0f, 1f),
                new Keyframe(1f, 0f));
            sol.size = new ParticleSystem.MinMaxCurve(1f, curve);

            // --- color over lifetime: alpha fade ---
            var col = ps.colorOverLifetime;
            col.enabled = true;
            var grad = new Gradient();
            grad.SetKeys(
                new[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(Color.white, 1f) },
                new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) });
            col.color = new ParticleSystem.MinMaxGradient(grad);

            // --- renderer: dual-pipeline shader + texture ---
            var renderer = go.GetComponent<ParticleSystemRenderer>();
            renderer.renderMode = ParticleSystemRenderMode.Billboard;

            // Pick the right shader for the active render pipeline.
            string shaderName = IsUsingURP ? URPParticleShader : BuiltInParticleShader;
            var shader = Shader.Find(shaderName);
            if (shader == null)
            {
                // Either: (a) URP expected but not installed, or (b) Built-in
                // shader missing (unlikely — it's built-in). Surface a clear
                // error and skip material construction entirely. Constructing
                // `new Material(null)` would throw ArgumentNullException and
                // leave the renderer without a material (Unity would display
                // the magenta error material on every particle).
                Debug.LogError(
                    $"[JamKitParticleFactory] Shader '{shaderName}' not found " +
                    $"(current pipeline: {(IsUsingURP ? "SRP (URP/HDRP)" : "Built-in")}). " +
                    "Ensure URP is installed and the active Render Pipeline Asset is set, " +
                    "or you're on Built-in and the Particles/Standard Unlit shader is available.");
            }
            else
            {
                var mat = new Material(shader);
                var texture = LoadKenneyTexture(tex);
                var tint    = EffectTint(tex);

                if (IsUsingURP)
                {
                    // URP Particles/Unlit: _BaseMap + _BaseColor; additive blend via:
                    //   * EnableKeyword("_SURFACE_TYPE_TRANSPARENT")  (the shader feature)
                    //   * _Surface = 1 (Transparent)
                    //   * _Blend   = 2 (Additive — handled by shader feature branch)
                    //   * _SrcBlend / _DstBlend = One/One  (force the actual GPU blend)
                    // Without the keyword + blend floats the additive branch
                    // is dead-stripped and particles render opaque white.
                    mat.SetTexture(BaseMapID, texture);
                    mat.SetColor(BaseColorID, tint);
                    mat.SetFloat(SurfaceID, 1f);   // 1 = Transparent
                    mat.SetFloat(BlendID, 2f);     // 2 = Additive
                    mat.SetFloat(SrcBlendID, (float)BlendMode.One);
                    mat.SetFloat(DstBlendID, (float)BlendMode.One);
                    mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                }
                else
                {
                    // Built-in Particles/Standard Unlit: _MainTex + _TintColor;
                    // additive blend via _Mode (3) + _SrcBlend/_DstBlend = One/One.
                    mat.SetTexture("_MainTex", texture);
                    mat.SetColor(TintColorID, tint);
                    mat.SetFloat(ModeID, 3f);                  // 3 = Transparent (additive-friendly)
                    mat.SetFloat(SrcBlendID, (float)BlendMode.One);
                    mat.SetFloat(DstBlendID, (float)BlendMode.One);
                    mat.SetFloat(ZWriteID, 0f);
                    mat.EnableKeyword("_ALPHABLEND_ON");
                }

                renderer.material = mat;
            }

            return ps;
        }

        /// <summary>
        /// Create and emit once, then auto-destroy after duration.
        /// Use for one-shot effects (hit, dust, explosion).
        /// </summary>
        public static ParticleSystem CreateOneShot(Transform parent, KenneyTex tex, float autoDestroyAfter = 1.5f)
        {
            var ps = Create(parent, tex);
            ps.Emit(16); // immediate burst
            Object.Destroy(ps.gameObject, autoDestroyAfter);
            return ps;
        }

        private static Texture2D LoadKenneyTexture(KenneyTex tex)
        {
            string assetPath = tex switch
            {
                KenneyTex.Muzzle01 => "Assets/art/particles/kenney-particle-pack/png/muzzle_01.png",
                KenneyTex.Dirt01   => "Assets/art/particles/kenney-particle-pack/png/dirt_01.png",
                KenneyTex.Magic03  => "Assets/art/particles/kenney-particle-pack/png/magic_03.png",
                KenneyTex.Flare01  => "Assets/art/particles/kenney-particle-pack/png/flare_01.png",
                _ => null
            };
#if UNITY_EDITOR
            // Editor-only: load asset by path so we don't need to duplicate PNGs into Resources/
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
#else
            // At runtime, you must have a Resources/Particles/ folder with the 4 PNGs.
            // Fall back to Resources if direct asset path fails.
            return Resources.Load<Texture2D>("Particles/" + System.IO.Path.GetFileNameWithoutExtension(assetPath));
#endif
        }

        private static Color EffectTint(KenneyTex tex) => tex switch
        {
            KenneyTex.Muzzle01 => new Color(1f, 0.85f, 0.4f, 1f),   // warm sparks
            KenneyTex.Dirt01   => new Color(0.65f, 0.55f, 0.45f, 1f),// brown dust
            KenneyTex.Magic03  => new Color(1f, 0.5f, 0.2f, 1f),    // fire burst
            KenneyTex.Flare01  => new Color(1f, 0.9f, 0.3f, 1f),    // gold glow
            _ => Color.white
        };
    }
}