using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UiController : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image healthBar, strengthBar;
        private PlayerStats _playerStats;
        
        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.HealthChanged += ChangeHeath;
            _playerStats.StrengthChanged += ChangeStrength;
        }

        private void Awake()
        {
            ChangeHeath(_playerStats.Health);
            ChangeStrength(_playerStats.Strength);
        }

        private void ChangeHeath(int health)
        {
            healthBar.fillAmount = (float) health / _playerStats.MaxHeath;
        }

        private void ChangeStrength(int strength)
        {
            strengthBar.fillAmount = (float) strength / _playerStats.MaxStrength;
        }

        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            _playerStats.HealthChanged -= ChangeHeath;
            _playerStats.StrengthChanged -= ChangeStrength;
        }
    }
}