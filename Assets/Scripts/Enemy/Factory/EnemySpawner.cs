using System;
using Core.Services;
using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy.Factory
{
    public class EnemySpawner : IDisposable
    {
        private EnemyFactory _factory;
        private IProjectUpdater _projectUpdater;
        private float _spawnInterval = 10f;
        private float _timeSinceLastSpawn;

        [Inject]
        public void Construct(EnemyFactory factory, IProjectUpdater projectUpdater)
        {
            _factory = factory;
            _projectUpdater = projectUpdater;
            _timeSinceLastSpawn = _spawnInterval;
            _projectUpdater.FixedUpdateCalled += OnFixedUpdate;
        }
        
        private void OnFixedUpdate()
        {
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= _spawnInterval)
            {
                SpawnEnemy();
                _timeSinceLastSpawn = 0;
            }
        }

        private void SpawnEnemy()
        {
            EnemyTypes type = Random.Range(0f, 1f) <= 0.2f ? EnemyTypes.EnemyBlue : EnemyTypes.EnemyRed;
            IEnemyEntity enemy = _factory.Create(type);
        }

        public void Dispose()
        {
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
        }
    }
}