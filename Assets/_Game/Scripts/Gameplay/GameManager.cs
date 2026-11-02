using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SingletonObject<GameManager>
{
    [SerializeField] private GameObject map;
    [SerializeField] private MainCharacterController mainCharacterController;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private BossController bossController;

    protected override void Awake()
    {
        base.Awake();
        GameEvent.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        map.SetActive(true);
        mainCharacterController.ActiveMainCharacter();
        enemyController.SpawnEnemies();
        bossController.SpawnBosses();
    }

    public void RespawnEnemyAndBoss()
    {
        enemyController.RespawnEnemies();
        bossController.RespawnBosses();
    }

    public void ResetTargetEnemyAndBoss()
    {
        enemyController.ResetTargetEnemy();
        bossController.ResetTargetBoss();
    }

    public void OnWin()
    {
        ResultView resultView = ResultView.Show();
        resultView.SetUp(true);
    }

    public void OnLose()
    {
        ResetTargetEnemyAndBoss();
        ResultView resultView = ResultView.Show();
        resultView.SetUp(false);
    }
}
