using System;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class BalanceCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private IPersistentProgressService _persistentProgress;

        [Inject]
        public void Construct(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
        }

        private PlayerBalance PlayerBalance => _persistentProgress.Progress.PlayerBalance;

        private void Awake()
        {
            PlayerBalance.OnChange += UpdateBalance;
        }

        private void OnDestroy()
        {
            PlayerBalance.OnChange -= UpdateBalance;
        }

        private void Start()
        {
            UpdateBalance();
        }

        private void UpdateBalance() => 
            _text.text = $"{PlayerBalance.WheatCount}";
    }
}