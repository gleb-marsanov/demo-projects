using _SaveTheVillage.Scripts.Data.Quests;

namespace _SaveTheVillage.Scripts.Data
{
    public class PlayerProgress
    {
        public readonly WorldData WorldData;
        public readonly PlayerBalance PlayerBalance;
        public readonly QuestData QuestData;

        public PlayerProgress(PlayerBalance playerBalance, WorldData worldData, QuestData questData)
        {
            PlayerBalance = playerBalance;
            WorldData = worldData;
            QuestData = questData;
        }
    }

}