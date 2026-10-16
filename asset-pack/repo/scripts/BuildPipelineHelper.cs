// BuildPipelineHelper.cs — JamKit
// 用途: 给 build.sh 调用的命令行构建入口。
//       Unity / 团结 启动时通过 -executeMethod JamKit.Editor.BuildPipelineHelper.BuildFromCLI 触发。
//       通过 -customArg:outputPath=... 与 -customArg:buildConfig=... 接收参数。
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#。
// 用法: 见 ../repo/build.sh

#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JamKit.Editor
{
    public static class BuildPipelineHelper
    {
        public static void BuildFromCLI()
        {
            string outputPath = GetCustomArg("outputPath", "Build/Output/Game.exe");
            string buildConfig = GetCustomArg("buildConfig", "Debug");

            Debug.Log($"[BuildPipelineHelper] target={EditorUserBuildSettings.activeBuildTarget}, output={outputPath}, config={buildConfig}");

            // 收集场景：Build Settings 里所有 enabled 的场景
            var scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Select(s => s.path)
                .ToArray();

            if (scenes.Length == 0)
            {
                Debug.LogError("[BuildPipelineHelper] No enabled scenes in Build Settings. Aborting.");
                EditorApplication.Exit(1);
                return;
            }

            var options = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = outputPath,
                target = EditorUserBuildSettings.activeBuildTarget,
                targetGroup = BuildTargetGroup.Standalone,
                options = BuildOptions.None
            };

            if (string.Equals(buildConfig, "Release", StringComparison.OrdinalIgnoreCase))
                options.options |= BuildOptions.StrictMode;

            BuildReport report = BuildPipeline.BuildPlayer(options);
            BuildResult result = report.summary.result;
            Debug.Log($"[BuildPipelineHelper] result={result}, size={report.summary.totalSize}, errors={report.summary.totalErrors}, warnings={report.summary.totalWarnings}");

            EditorApplication.Exit(result == BuildResult.Succeeded ? 0 : 1);
        }

        private static string GetCustomArg(string name, string fallback)
        {
            // Args look like:  -customArg:outputPath="/path/JamGame.exe"
            // Split on the FIRST '=' only so values containing '=' (rare, but possible
            // on Windows paths like C:\builds\foo=Baz\game.exe) round-trip cleanly.
            string prefix = $"-customArg:{name}=";
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                string a = args[i];
                if (a.StartsWith(prefix, StringComparison.Ordinal))
                    return a.Substring(prefix.Length);
            }
            return fallback;
        }
    }
}
#endif