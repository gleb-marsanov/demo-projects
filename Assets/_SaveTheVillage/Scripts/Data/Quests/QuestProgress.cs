using System;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Data.Quests
{
    public class QuestProgress
    {
        private int _currentProgress;
        private bool _isCompleted;

        public QuestProgress(int currentProgress, bool isCompleted)
        {
            _currentProgress = currentProgress;
            _isCompleted = isCompleted;
        }

        public Action OnChange;

        public int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnChange?.Invoke();
            }
        }
    }
}