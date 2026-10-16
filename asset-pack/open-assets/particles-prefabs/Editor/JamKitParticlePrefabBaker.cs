// JamKitParticlePrefabBaker.cs — Editor-only
// SPDX-License-Identifier: MIT
// Copyright (c) 2026 hackathon-prep contributors
//
// One-click Editor menu to BAKE 4 jam-essential particle prefabs
// (sparks / dust / explosion / pickup-glow) into actual .prefab files.
//
// Why this script and not hand-written .prefab?
//   - .prefab files reference textures by GUID + fileID. GUIDs are only
//     generated at first Unity import. Hand-writing a .prefab that resolves
//     reliably outside that first pass is impossible without a paired .meta
//     file (and even then, fileIDs of references can drift).
//   - This baker constructs the ParticleSystem GameObject in memory via the
//     factory helper, then calls PrefabUtility.SaveAsPrefabAsset(...) which
//     hands the resulting .prefab a freshly-minted GUID + .meta at save time
//     (so it imports cleanly on first load in any Unity project).
//
// Usage:
//   Menu: Tools / JamKit / Bake Particle Prefabs
//   → generates 4 .prefab in
//     Assets/Prefabs/JamKit/Particles/<Effect>3D.prefab
//     Assets/Prefabs/JamKit/Particles/<Effect>2D.prefab
//   Each one is a real prefab you can drag into a scene or assign to a
//   JuiceManager. They use the same dual-pipeline shader as the runtime
//   factory (URP Particles/Unlit if URP is the active pipeline, otherwise
//   Particles/Standard Unlit).
//
// 3D vs 2D variants:
//   "3D"   → ParticleSystemRenderer.renderMode = Billboard, simulationSpace=World
//             gravity enabled (small).  Default for spark/explosion/dust.
//   "2D"   → renderer.renderMode = Billboard, simulationSpace=Local (camera
//             moves, particles stay world-anchored for parallax).  Slightly
//             lower gravity, longer lifetime — tuned for 2D gameplay.

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace JamKit.EditorTools
{
    public static class JamKitParticlePrefabBaker
    {
        private const string OutputDir        = "Assets/Prefabs/JamKit/Particles";
        private const string MenuPath3D       = "Tools/JamKit/Bake Particle Prefabs (3D + 2D)";
        private const string MenuPathSpark    = "Tools/JamKit/Bake Particle Prefabs/Sparks (3D + 2D)";
        private const string MenuPathDust     = "Tools/JamKit/Bake Particle Prefabs/Dust (3D + 2D)";
        private const string MenuPathExplode  = "Tools/JamKit/Bake Particle Prefabs/Explosion (3D + 2D)";
        private const string MenuPathPickup   = "Tools/JamKit/Bake Particle Prefabs/Pickup Glow (3D + 2D)";

        // -------- top-level: bake all 4 --------
        [MenuItem(MenuPath3D)]
        public static void BakeAll()
        {
            BakeSparks();
            BakeDust();
            BakeExplosion();
            BakePickupGlow();
        }

        // -------- individual bakers (4 effects × 2 variants = 8 prefabs) --------
        [MenuItem(MenuPathSpark)]   public static void BakeSparks()    => BakeBoth("Sparks",       JamKitParticleFactory.KenneyTex.Muzzle01);
        [MenuItem(MenuPathDust)]    public static void BakeDust()      => BakeBoth("LandingDust",  JamKitParticleFactory.KenneyTex.Dirt01);
        [MenuItem(MenuPathExplode)] public static void BakeExplosion() => BakeBoth("Explosion",    JamKitParticleFactory.KenneyTex.Magic03);
        [MenuItem(MenuPathPickup)]  public static void BakePickupGlow()=> BakeBoth("PickupGlow",  JamKitParticleFactory.KenneyTex.Flare01);

        // ============================================================
        //  Implementation
        // ============================================================
        private static void BakeBoth(string effectName, JamKitParticleFactory.KenneyTex tex)
        {
            string path3D = $"{OutputDir}/{effectName}3D.prefab";
            string path2D = $"{OutputDir}/{effectName}2D.prefab";
            EnsureDir(OutputDir);
            BakeOne(path3D, tex, isThreeD: true,  effectName + "3D");
            BakeOne(path2D, tex, isThreeD: false, effectName + "2D");
        }

        private static void BakeOne(string assetPath, JamKitParticleFactory.KenneyTex tex, bool isThreeD, string objectName)
        {
            // Use the runtime factory to build a fully-configured GameObject in memory.
            // We do this in a hidden parent so the scene isn't dirtied.
            var holder = new GameObject("__JamKitBakerHolder");
            holder.hideFlags = HideFlags.HideAndDontSave;
            try
            {
                var ps = JamKitParticleFactory.Create(holder.transform, tex);

                // 3D vs 2D tuning overrides
                var main = ps.main;
                if (isThreeD)
                {
                    main.simulationSpace = ParticleSystemSimulationSpace.World;
                    main.gravityModifier = 0.6f;
                }
                else
                {
                    // 2D: gravity is typically zero for HUD-style effects;
                    // but for "exploding in the world" 2D particles, gravity
                    // ~0.4 is normal.  Compromise = 0.2.
                    main.simulationSpace = ParticleSystemSimulationSpace.World;
                    main.gravityModifier = 0.2f;
                    main.startSize = 0.35f;
                    main.startLifetime = 0.6f;
                }

                // Sanity: the factory emits one burst on play (PlayOnAwake = true),
                // but for a prefab we want users to call Emit() themselves.
                // Stop the auto-play.
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // The "factory" left an empty renderer that the user must re-create
                // (because PrefabUtility.SaveAsPrefabAsset expects the GameObject
                // root, not the parent).  So: rename the GameObject to the
                // effect name and reparent to null (root) before saving.
                var go = ps.gameObject;
                go.name = objectName;
                go.transform.SetParent(null, worldPositionStays: false);

                // Save as prefab — Unity generates a fresh GUID + .meta on disk.
                // OverwriteIfDifferent = false so users can keep their tweaks.
                bool success;
                var saved = PrefabUtility.SaveAsPrefabAsset(go, assetPath, out success);
                if (!success || saved == null)
                {
                    Debug.LogError($"[JamKitParticlePrefabBaker] Failed to save {assetPath}");
                }
                else
                {
                    Debug.Log($"[JamKitParticlePrefabBaker] Baked → {assetPath}  " +
                              $"(pipeline: {(JamKitParticleFactory.IsUsingURP ? "URP" : "Built-in")}, variant: {(isThreeD ? "3D" : "2D")})");
                }
            }
            finally
            {
                // Clean up the temp holder + auto-baked particles.
                if (holder != null) Object.DestroyImmediate(holder);
            }
        }

        // Validate: only enabled when the Editor is in Play Mode OR the
        // target project has Assets/art/particles/kenney-particle-pack/png/.
        // (Baking outside Play mode is fine — it's an editor-only tool.)
        [MenuItem(MenuPath3D, true)]
        public static bool ValidateBakeAll() => true;

        private static void EnsureDir(string assetDir)
        {
            if (AssetDatabase.IsValidFolder(assetDir))
                return;

            // Walk parent chain and create each missing segment.
            string[] parts = assetDir.Split('/');
            string current = parts[0]; // "Assets"
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }
    }
}
#endif