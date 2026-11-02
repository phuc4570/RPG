using System.Collections.Generic;
using UnityEngine;

public class BossController : SingletonObject<BossController>
{
    [SerializeField] List<BossSpawnPoint> spawnPoints;

    private BossSpawnPoint currentBossSpawnPoint;

    public void SpawnBosses()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnBoss();
        }
    }

    public void RespawnBosses()
    {
        if(currentBossSpawnPoint != null)
        {
            currentBossSpawnPoint.SpawnBoss();
            currentBossSpawnPoint.Boss.MovementController.Target = null;
            currentBossSpawnPoint = null;
            GameEvent.RaiseResetBossFight();
        }
    }

    public void ResetTargetBoss()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.Boss != null && spawnPoint.Boss.IsAlive)
            {
                spawnPoint.Boss.MovementController.Target = null;
            }
        }
    }

    public void TriggerBoss(BossSpawnPoint bossSpawnPoint)
    {
        currentBossSpawnPoint = bossSpawnPoint;
        bossSpawnPoint.Boss.MovementController.Target = MainCharacterController.Ins.MainCharacter.gameObject;
        GameEvent.RaiseTriggerBossFight(bossSpawnPoint.Boss.Data);
    }
}
