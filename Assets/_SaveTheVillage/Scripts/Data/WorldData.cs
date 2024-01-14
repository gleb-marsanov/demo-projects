namespace _SaveTheVillage.Scripts.Data
{
    public class WorldData
    {
        public readonly HireData HireData;
        public readonly BattleData BattleData;
        public readonly FeedingData FeedingData;
        public readonly HarvestingData HarvestingData;

        public WorldData(HarvestingData harvestingData, FeedingData feedingData, BattleData battleData, HireData hireData)
        {
            HireData = hireData;
            BattleData = battleData;
            FeedingData = feedingData;
            HarvestingData = harvestingData;
        }
    }

}