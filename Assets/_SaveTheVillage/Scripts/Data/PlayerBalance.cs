using System;
using System.Collections.Generic;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Data
{
    public class PlayerBalance
    {
        private int _wheatCount;
        private readonly Dictionary<VillagerType, int> _villagers;
        
        public PlayerBalance(int wheatCount, Dictionary<VillagerType, int> villagers)
        {
            _wheatCount = wheatCount;
            _villagers = villagers;
        }

        public Action OnChange;
        
        public IEnumerable<VillagerType> Villagers => _villagers.Keys;
        public int WheatCount
        {
            get => _wheatCount;
            set
            {
                _wheatCount = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }

        public void AddVillager(VillagerType type)
        {
            _villagers[type] = GetVillagersCount(type) + 1;
            OnChange?.Invoke();
        }

        public int GetVillagersCount(VillagerType villagerType) =>
            _villagers.TryGetValue(villagerType, out int count) ? count : 0;

        public void RemoveVillagers(VillagerType villagerType, int count)
        {
            _villagers[villagerType] = Mathf.Max(0, GetVillagersCount(villagerType) - count);
            OnChange?.Invoke();
        }
    }
}