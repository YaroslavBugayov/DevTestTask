using Player;
using UnityEngine;
using Zenject;

namespace Enemy.Entities
{
    public class RedEnemyEntity : MonoBehaviour, IEnemyEntity
    {
        private PlayerEntity _player;

        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            _player = playerEntity;
        }
    }
}