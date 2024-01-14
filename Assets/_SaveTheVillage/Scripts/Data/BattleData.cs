using System;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Data
{
    public class BattleData
    {
        public readonly TimerData BattleTimer;
        private int _nextBattleIndex;
        private int _completedBattlesCount;

        public BattleData(TimerData battleTimer, int completedBattlesCount, int nextBattleIndex)
        {
            BattleTimer = battleTimer;
            _completedBattlesCount = completedBattlesCount;
            _nextBattleIndex = nextBattleIndex;
        }

        public Action OnChange;

        public int CompletedBattlesCount
        {
            get => _completedBattlesCount;
            set
            {
                _completedBattlesCount = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }

        public int NextBattleIndex
        {
            get => _nextBattleIndex;
            set
            {
                _nextBattleIndex = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }
    }
}