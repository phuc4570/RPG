using UnityEngine;

public class BossSpawnPoint : MonoBehaviour
{
    [SerializeField] private BossTypeEnum bossType;
    [SerializeField] private Collider2D bossFightTrigger;
    [SerializeField] private GameObject bossFightGateObject;

    private BossConfigData bossConfigData;
    public Boss Boss { get; private set; }

    public void SpawnBoss()
    {
        bossFightTrigger.enabled = true;
        bossFightGateObject.SetActive(false);
        if (bossConfigData == null)
        {
            bossConfigData = BossConfig.Ins.GetData(bossType);
        }
        if (bossConfigData != null)
        {
            if (Boss != null)
            {
                Boss.EnableEntity();
                Boss.transform.position = transform.position;
            }
            else
            {
                GameObject enemyPrefab = bossConfigData.Prefab;
                if (enemyPrefab != null)
                {
                    GameObject enemyInstance = PoolSystem.Ins.RequiredObject(enemyPrefab) as GameObject;
                    Boss = enemyInstance.GetComponent<Boss>();
                    Boss.EnableEntity();
                    Boss.transform.position = transform.position;
                }
            }
            Boss.MovementController.Target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossFightTrigger.enabled = false;
        bossFightGateObject.SetActive(true);
        BossController.Ins.TriggerBoss(this);
    }
}
