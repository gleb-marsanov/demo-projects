using System;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Data
{
    public class TimerData
    {
        private float _time;

        public TimerData(float time)
        {
            _time = time;
        }

        public Action OnChange;

        public float Time
        {
            get => _time;
            set
            {
                _time = Mathf.Max(0, value);
                OnChange?.Invoke();
            }
        }
    }
}