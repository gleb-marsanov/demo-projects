using UnityEngine;

namespace _SaveTheVillage.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        T Load<T>(string assetPath) where T : Object;
        T[] LoadAll<T>(string assetPath) where T : Object;
    }
}