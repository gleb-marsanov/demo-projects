using System;
using System.Collections.Generic;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Data
{
    public class HireData
    {
        private int _totalVillagersHired;
        private readonly Dictionary<VillagerType, float> _hireTime;

        public HireData(Dictionary<VillagerType, float> hireTime, int totalVillagersHired)
        {
            _hireTime = hireTime;
            _totalVillagersHired = totalVillagersHired;
        }

        public Action OnChange;

        public int TotalVillagersHired
        {
            get => _totalVillagersHired;
            set
            {
                _totalVillagersHired = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }

        public float GetTimeUntilHiringFor(VillagerType villagerType) =>
            _hireTime.TryGetValue(villagerType, out float time) ? time : 0;

        public void SetHireTime(VillagerType villagerType, float value) =>
            _hireTime[villagerType] = Mathf.Max(0, value);
    }
}