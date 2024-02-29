using UnityEngine;

namespace Infrastructure.Services.Providers
{
    public interface IHeroProvider
    {
        public GameObject Hero { get; }

        public void SetHero(GameObject hero);
    }

}