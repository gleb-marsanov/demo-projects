using UnityEngine;

namespace _SaveTheVillage.Scripts.Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}