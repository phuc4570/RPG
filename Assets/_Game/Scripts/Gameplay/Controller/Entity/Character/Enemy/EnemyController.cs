using UnityEngine;

public class EnemyController : SingletonObject<EnemyController>
{
    [SerializeField] Enemy enemy;

    public void ActiveEnemy(bool flag)
    {
        enemy.gameObject.SetActive(flag);
        SetUpEnemy();
    }

    private void SetUpEnemy()
    {
        enemy.EnableEntity();
    }
}
