using System;

namespace _SaveTheVillage.Scripts.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        void LoadScene(string name, Action onLoaded = null);
    }
}