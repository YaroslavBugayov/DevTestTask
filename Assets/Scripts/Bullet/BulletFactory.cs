using UnityEngine;
using Zenject;

namespace Bullet
{
    public class BulletFactory : PlaceholderFactory<GameObject, Vector3, Quaternion, BulletEntity>
    {
        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public override BulletEntity Create(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            return _diContainer
                .InstantiatePrefabForComponent<BulletEntity>(bulletPrefab, position, rotation, null);
        }
    }
}