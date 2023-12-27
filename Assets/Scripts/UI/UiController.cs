using System;
using InputReader;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UiController : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image healthBar, strengthBar;
        [SerializeField] private GameObject pausePanel, gameOverPanel;
        private PlayerStats _playerStats;
        private IInputReader _inputReader;
        
        [Inject]
        public void Construct(PlayerStats playerStats, IInputReader inputReader)
        {
            _playerStats = playerStats;
            _inputReader = inputReader;
            
            _playerStats.HealthChanged += ChangeHeath;
            _playerStats.StrengthChanged += ChangeStrength;
            
            
        }

        private void Awake()
        {
            ChangeHeath(_playerStats.Health);
            ChangeStrength(_playerStats.Strength);
        }

        private void ChangeHeath(int health) => healthBar.fillAmount = (float) health / _playerStats.MaxHeath;

        private void ChangeStrength(int strength) => strengthBar.fillAmount = (float) strength / _playerStats.MaxStrength;
        
        

        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            _playerStats.HealthChanged -= ChangeHeath;
            _playerStats.StrengthChanged -= ChangeStrength;
        }
    }
}