using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Managers
{
    public class EnemyManager
    {
        EnemySpawner enemySpawner;

        public void InitEnemySpawn(EnemySpawner target)
        {
            enemySpawner = target;
        }

        public void SpawnEnemy(int index)
        {
            enemySpawner.Spawn(index);
        }
    }
}