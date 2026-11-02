using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SingletonObject<EnemyController>
{
    [SerializeField] List<EnemySpawnPoint> spawnPoints;

    public void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }
    }

    public void RespawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }
    }

    public void ResetTargetEnemy()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.Enemy != null && spawnPoint.Enemy.IsAlive)
            {
                spawnPoint.Enemy.MovementController.Target = null;
            }
        }
    }
}
