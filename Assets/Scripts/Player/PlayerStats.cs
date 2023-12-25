namespace Player
{
    public class PlayerStats
    {
        public float Speed { get; private set; } = 2.5f;
        public float TurnSpeed { get; private set; } = 20f;
        public float JumpForce { get; private set; } = 2.5f;
        public float BulletSpeed { get; private set; } = 5f;
        public float FireRate { get; private set; } = 2f;
        public int Damage { get; private set; } = 5;
        public int Health { get; private set; } = 100;
        public int Strength { get; private set; } = 50;
    }
}