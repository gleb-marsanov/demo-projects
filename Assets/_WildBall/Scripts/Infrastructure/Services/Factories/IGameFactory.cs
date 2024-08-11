using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IGameFactory
    {
        GameObject CreateHero(Vector3 at);
        void Cleanup();
    }
}