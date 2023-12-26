using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;

namespace Enemy.Factory
{
    public class EnemyFactory : PlaceholderFactory<EnemyType, Vector3, Transform, IEnemyEntity> { }
}