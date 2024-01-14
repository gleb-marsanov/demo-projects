using System;
using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Sounds
{
    [Serializable]
    public class SoundConfig
    {
        public SoundId Id;
        public AudioClip Clip;
        public bool Loop;
        public float Volume = 1;
    }
}