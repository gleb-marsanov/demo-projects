using UnityEngine;

namespace _SaveTheVillage.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string assetPath) where T : Object => 
             Resources.Load<T>(assetPath);

        public T[] LoadAll<T>(string assetPath) where T : Object => 
            Resources.LoadAll<T>(assetPath);
    }
}