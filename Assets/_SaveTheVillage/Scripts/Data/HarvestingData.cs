using System;

namespace _SaveTheVillage.Scripts.Data
{
    public class HarvestingData
    {
        public readonly TimerData HarvestingTimer;
        private int _totalWheatHarvested;

        public HarvestingData(TimerData harvestingTimer, int totalWheatHarvested)
        {
            HarvestingTimer = harvestingTimer;
            _totalWheatHarvested = totalWheatHarvested;
        }

        public Action OnChange;

        public int TotalWheatHarvested
        {
            get => _totalWheatHarvested;
            set
            {
                _totalWheatHarvested = value;
                OnChange?.Invoke();
            }
        }
    }
}