using Enemy.Entities;
using Enemy.Enums;
using Zenject;

namespace Enemy.Factory
{
    public class EnemyFactory : PlaceholderFactory<EnemyTypes, IEnemyEntity> { }
}