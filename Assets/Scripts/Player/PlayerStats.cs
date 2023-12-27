using System;

namespace Player
{
    public class PlayerStats
    {
        public Action<int> HealthChanged;
        public Action<int> StrengthChanged;
        public Action GameWasOver;
        
        public float Speed { get; private set; } = 2.5f;
        public float TurnSpeed { get; private set; } = 2f;
        public float JumpForce { get; private set; } = 2.5f;
        public float BulletSpeed { get; private set; } = 10f;
        public float FireRate { get; private set; } = 2f;
        public int Health { get; private set; } = 100;
        public int Strength { get; private set; } = 50;
        public int MaxHeath { get; private set; } = 100;
        public int MaxStrength { get; private set; } = 100;
        public int Killed { get; private set; } = 0;

        public bool TakeDamage(int damage)
        {
            Health = Math.Clamp(Health - damage, 0, MaxHeath);
            HealthChanged?.Invoke(Health);
            if (Health == 0)
            {
                GameWasOver?.Invoke();
                return true;
            }

            return false;
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

        public void ResetStrength()
        {
            Strength = 0;
            StrengthChanged?.Invoke(Strength);
        }

        public void AddKill() => Killed++;
    }
}