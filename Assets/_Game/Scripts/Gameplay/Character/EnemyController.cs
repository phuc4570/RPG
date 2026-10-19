using UnityEngine;

public class EnemyController : SingletonObject<EnemyController>
{
    [SerializeField] Enemy enemy;

    private void Start()
    {
        SetUpEnemy();
    }

    private void SetUpEnemy()
    {
        enemy.SetUp();
    }

    public void ActiveEnemy(bool flag)
    {
        enemy.gameObject.SetActive(flag);
    }
}
