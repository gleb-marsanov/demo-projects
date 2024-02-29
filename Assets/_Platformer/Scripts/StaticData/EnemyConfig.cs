using System;
using Gameplay.Enemy;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class EnemyConfig
    {
        public EnemyType EnemyType;
        public GameObject Prefab;
    }
}