using _SaveTheVillage.Scripts.Gameplay.Battles;
using _SaveTheVillage.Scripts.Gameplay.Feeding;
using _SaveTheVillage.Scripts.Gameplay.Harvesting;

namespace _SaveTheVillage.Scripts.Infrastructure.States.GameStates
{
    public class GameLoopState : IExitableState, IUpdatable
    {
        private readonly IHarvestingService _harvestingService;
        private readonly IBattleService _battleService;
        private readonly IFeedingService _feedingService;
        private readonly ICoroutineRunner _coroutineRunner;

        public GameLoopState
        (
            IHarvestingService harvestingService,
            IBattleService battleService,
            IFeedingService feedingService,
            ICoroutineRunner coroutineRunner
        )
        {
            _harvestingService = harvestingService;
            _battleService = battleService;
            _feedingService = feedingService;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _coroutineRunner.StopAllCoroutines();
        }

        public void Update()
        {
            _harvestingService.Update();
            _battleService.Update();
            _feedingService.Update();
        }

    }
}