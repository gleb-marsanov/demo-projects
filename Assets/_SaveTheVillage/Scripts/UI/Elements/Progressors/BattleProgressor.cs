using _SaveTheVillage.Scripts.StaticData.Timers;

namespace _SaveTheVillage.Scripts.UI.Elements.Progressors
{
    public class BattleProgressor : GameLoopProgressor
    {
        protected override float MaxValue => StaticData.ForTimer(TimerId.Battle).Duration;
        protected override float CurrentValue => PlayerProgress.WorldData.BattleData.BattleTimer.Time;
        
        protected override void OnStart() => 
            PlayerProgress.WorldData.BattleData.BattleTimer.OnChange += UpdateProgressor;
    }
}