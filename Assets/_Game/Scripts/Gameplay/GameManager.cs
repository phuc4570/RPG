using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SingletonObject<GameManager>
{
    [SerializeField] private GameObject map;
    [SerializeField] private MainCharacterController mainCharacterController;
    [SerializeField] private EnemyController enemyController;

    protected override void Awake()
    {
        base.Awake();
        GameEvent.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        map.SetActive(true);
        mainCharacterController.ActiveMainCharacter(true);
        enemyController.ActiveEnemy(true);
    }
}
