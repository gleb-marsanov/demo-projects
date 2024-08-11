using System.Linq;
using Editor.Utilities;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor.StaticData
{
    [CustomEditor(typeof(GameConfig))]
    public class GameConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameConfig = (GameConfig)target;

            if (GUILayout.Button("Collect Levels"))
                CollectLevels(gameConfig);
        }

        private static void CollectLevels(GameConfig gameConfig)
        {
            string[] sceneAssets = EditorUtilities.GetSceneAssets();
            gameConfig.Levels = sceneAssets.Select(LevelNames).Where(x => x.StartsWith("Level_")).ToArray();
        }

        private static string LevelNames(string arg)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(arg);
            return System.IO.Path.GetFileNameWithoutExtension(scenePath);
        }
    }
}