using System;
using Core.Services;
using Enemy.Entities;
using Enemy.Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy.Factory
{
    public class EnemySpawner : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform parent;
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
            if (parent.childCount >= 30) return;

            if (_spawnInterval > 6) _spawnInterval -= 2;
            
            EnemyType type = Random.Range(0f, 1f) <= 0.2f ? EnemyType.EnemyBlue : EnemyType.EnemyRed;
            Vector3 position;
            position = spawnPoints.Length > 0 
                ? spawnPoints[Random.Range(0, spawnPoints.Length)].position 
                : Vector3.zero;
            IEnemyEntity enemy = _factory.Create(type, RandomizePosition(position), parent);
        }

        private Vector3 RandomizePosition(Vector3 position)
        {
            position.x += Random.Range(-1f, 1f);
            position.z += Random.Range(-1f, 1f);
            return position;
        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _projectUpdater.FixedUpdateCalled -= OnFixedUpdate;
        }
    }
}