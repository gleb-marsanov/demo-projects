using UnityEngine;

namespace Infrastructure.Services.Providers
{
    public class HeroProvider : IHeroProvider
    {
        public GameObject Hero { get; private set; }

        public void SetHero(GameObject hero)
        {
            Hero = hero;
        }
    }
}