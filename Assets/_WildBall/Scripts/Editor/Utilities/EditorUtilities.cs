using UnityEditor;

namespace Editor.Utilities
{
    public abstract class EditorUtilities
    {
        public static string[] GetSceneAssets()
        {
            string[] sceneAssets = AssetDatabase.FindAssets("t:Scene", new[]
            {
                "Assets/_WildBall/Scenes"
            });
            return sceneAssets;
        }
    }
}