using System;
using Interfaces;

namespace Player
{
    public class PlayerStats
    {
        public Action<int> HealthChanged;
        public Action<int> StrengthChanged;
        
        public float Speed { get; private set; } = 2.5f;
        public float TurnSpeed { get; private set; } = 20f;
        public float JumpForce { get; private set; } = 2.5f;
        public float BulletSpeed { get; private set; } = 5f;
        public float FireRate { get; private set; } = 2f;
        public int Health { get; private set; } = 100;
        public int Strength { get; private set; } = 50;
        private const int MaxHeath = 100;
        private const int MaxStrength = 100;

        public void TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            HealthChanged?.Invoke(Health);
        }
        
        public void TakeDamageToStrength(int strengthDamage)
        {
            Strength = Math.Clamp(Strength - strengthDamage, 0, MaxStrength);
            StrengthChanged?.Invoke(Strength);
        }

        public void AddStrength(int strength)
        {
            Strength = Math.Clamp(Strength + strength, 0, MaxStrength);
            StrengthChanged?.Invoke(Strength);
        }
    }
}