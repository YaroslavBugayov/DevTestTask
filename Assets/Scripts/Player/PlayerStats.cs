using System;
using Interfaces;

namespace Player
{
    public class PlayerStats
    {
        public float Speed { get; private set; } = 2.5f;
        public float TurnSpeed { get; private set; } = 20f;
        public float JumpForce { get; private set; } = 2.5f;
        public float BulletSpeed { get; private set; } = 5f;
        public float FireRate { get; private set; } = 2f;
        public int Health { get; private set; } = 100;
        public int Strength { get; private set; } = 50;
        private const int MaxHeath = 100;
        private const int MaxStrength = 100;

        public void TakeDamage(int damage) => Health = Math.Clamp(Health - damage, 0, MaxHeath);
        public void AddStrength(int strength) => Strength = Math.Clamp(Strength + strength, 0, MaxStrength);
    }
}