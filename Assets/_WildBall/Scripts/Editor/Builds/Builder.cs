using System;
using System.Linq;
using Editor.Utilities;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Editor.Builds
{
    public static class Builder
    {
        [MenuItem("Build/🔃 Update build scenes")]
        public static void UpdateBuildScenes()
        {
            string[] sceneAssets = EditorUtilities.GetSceneAssets();
            AddScenesToBuild(sceneAssets);
        }

        [MenuItem("Build/📦 Windows 64")]
        public static void BuildWin64()
        {
            string[] sceneAssets = EditorUtilities.GetSceneAssets();

            BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions()
            {
                target = BuildTarget.StandaloneWindows64,
                locationPathName = "artifacts/Win64/WildBall.exe",
                scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray(),
            });

            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception("Windows64 build failed. See log for details.");
        }

        private static void AddScenesToBuild(string[] sceneAssets)
        {
            EditorBuildSettings.scenes = sceneAssets.Select(AssetDatabase.GUIDToAssetPath)
                .Select(x => new EditorBuildSettingsScene(x, true)).ToArray();
        }
    }
}