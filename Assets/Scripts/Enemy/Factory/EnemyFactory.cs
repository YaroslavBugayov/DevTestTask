using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;

namespace Enemy.Factory
{
    public class EnemyFactory : PlaceholderFactory<EnemyTypes, Vector3, Transform, IEnemyEntity> { }
}