using System;
using Core.Services;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UiController : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image healthBar, strengthBar;
        [SerializeField] private GameObject pausePanel, gameOverPanel;
        [SerializeField] private TMP_Text enemiesKilledText;
        private PlayerStats _playerStats;
        private IProjectUpdater _projectUpdater;
        
        [Inject]
        public void Construct(PlayerStats playerStats, IProjectUpdater projectUpdater)
        {
            _playerStats = playerStats;
            _projectUpdater = projectUpdater;
            
            _playerStats.HealthChanged += ChangeHeath;
            _playerStats.StrengthChanged += ChangeStrength;
            _playerStats.GameWasOver += OnGameOver;

            _projectUpdater.PauseStateChanged += TogglePauseMenu;
        }

        private void Awake()
        {
            ChangeHeath(_playerStats.Health);
            ChangeStrength(_playerStats.Strength);
        }

        private void ChangeHeath(int health) => healthBar.fillAmount = (float) health / _playerStats.MaxHeath;

        private void ChangeStrength(int strength) => strengthBar.fillAmount = (float) strength / _playerStats.MaxStrength;

        private void OnGameOver()
        {
            enemiesKilledText.text += _playerStats.Killed;
            gameOverPanel.SetActive(true);
            Dispose();
            _projectUpdater.IsPaused = true;
        }

        private void TogglePauseMenu(bool isPause) => pausePanel.SetActive(isPause);

        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            _playerStats.HealthChanged -= ChangeHeath;
            _playerStats.StrengthChanged -= ChangeStrength;
            _projectUpdater.PauseStateChanged -= TogglePauseMenu;
        }
    }
}