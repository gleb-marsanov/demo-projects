using UnityEngine;

namespace Infrastructure.Services.Providers
{
    public interface IHeroProvider
    {
        public GameObject Hero { get; }
        void SetHero(GameObject hero);
    }

}