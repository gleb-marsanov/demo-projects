using System;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.GameOver;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.States;
using _SaveTheVillage.Scripts.Infrastructure.States.GameStates;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Windows
{
    public class GameOverWindow : WindowBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _statsText;
        [SerializeField] private TMP_Text _reason;

        private string _statsTextFormat;
        private string _reasonFormat;

        private IGameStateMachine _gameStateMachine;
        private ITimeService _time;
        private IPersistentProgressService _progressService;
        private IGameOverService _gameOverService;

        [Inject]
        public void Construct
        (
            IGameStateMachine gameStateMachine,
            ITimeService time,
            IPersistentProgressService progressService,
            IGameOverService gameOverService
        )
        {
            _time = time;
            _gameOverService = gameOverService;
            _progressService = progressService;
            _gameStateMachine = gameStateMachine;
        }

        private PlayerProgress PlayerProgress => _progressService.Progress;

        protected override void Initialize()
        {
            _time.Pause();
            int villagersHired = PlayerProgress.WorldData.HireData.TotalVillagersHired;
            int battlesCompleted = PlayerProgress.WorldData.BattleData.CompletedBattlesCount;
            int wheatHarvested = PlayerProgress.WorldData.HarvestingData.TotalWheatHarvested;

            _statsTextFormat = _statsText.text;
            _statsText.text = string.Format(_statsTextFormat, villagersHired, battlesCompleted, wheatHarvested);
            _restartButton.onClick.AddListener(RestartGame);

            _reasonFormat = _reason.text;
            string gameOverReason = _gameOverService.GameOverReason switch
            {
                GameOverReason.NotEnoughFood => "not enough food",
                GameOverReason.EnemiesWin => "all warriors are dead",
                GameOverReason.NotSet => "unknown",
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _reason.text = string.Format(_reasonFormat, gameOverReason);
        }

        protected override void OnClose()
        {
            _gameStateMachine.Enter<QuitGameState>();
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}