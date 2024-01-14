using _SaveTheVillage.Scripts.StaticData.Timers;

namespace _SaveTheVillage.Scripts.UI.Elements.Progressors
{
    public class HarvestingProgressor : GameLoopProgressor
    {
        protected override float MaxValue => StaticData.ForTimer(TimerId.Harvesting).Duration;
        protected override float CurrentValue => PlayerProgress.WorldData.HarvestingData.HarvestingTimer.Time;
        
        protected override void OnStart() => 
            PlayerProgress.WorldData.HarvestingData.HarvestingTimer.OnChange += UpdateProgressor;
    }
}