using _SaveTheVillage.Scripts.StaticData.Timers;

namespace _SaveTheVillage.Scripts.UI.Elements.Progressors
{
    public class FeedingProgressor : GameLoopProgressor
    {
        protected override float MaxValue => StaticData.ForTimer(TimerId.Feeding).Duration;
        protected override float CurrentValue => PlayerProgress.WorldData.FeedingData.FeedingTimer.Time;
        
        protected override void OnStart() => 
            PlayerProgress.WorldData.FeedingData.FeedingTimer.OnChange += UpdateProgressor;
    }
}