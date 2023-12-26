using Bullet.Entities;
using Bullet.Enums;
using UnityEngine;
using Zenject;

namespace Bullet.Factory
{
    public class BulletFactory : PlaceholderFactory<BulletType, Vector3, Quaternion, IBulletEntity> { }
}