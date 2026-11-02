using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyTypeEnum enemyType;
    [SerializeField] private bool isAlpha;

    private EnemyConfigData enemyConfigData;
    public Enemy Enemy { get; private set; }

    public void SpawnEnemy()
    {
        if(enemyConfigData == null)
        {
            enemyConfigData = EnemyConfig.Ins.GetData(enemyType);
        }
        if (enemyConfigData != null)
        {
            if(Enemy != null)
            {
                Enemy.EnableEntity(isAlpha);
                Enemy.transform.position = transform.position;
            }
            else
            {
                GameObject enemyPrefab = enemyConfigData.Prefab;
                if (enemyPrefab != null)
                {
                    GameObject enemyInstance = PoolSystem.Ins.RequiredObject(enemyPrefab) as GameObject;
                    Enemy = enemyInstance.GetComponent<Enemy>();
                    Enemy.EnableEntity(isAlpha);
                    Enemy.transform.position = transform.position;
                }
            }
            Enemy.MovementController.Target = null;
        }
    }
}
