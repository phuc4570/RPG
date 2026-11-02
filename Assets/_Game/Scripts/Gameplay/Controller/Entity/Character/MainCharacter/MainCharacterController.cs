using UnityEngine;

public class MainCharacterController : SingletonObject<MainCharacterController>
{
    [SerializeField] private MainCharacter mainCharacter;
    [SerializeField] private Transform defaultSpawnPoint;
    public MainCharacter MainCharacter => mainCharacter;

    private Transform lastestSpawnPoint;

    protected override void Awake()
    {
        base.Awake();
        lastestSpawnPoint = defaultSpawnPoint;
    }

    public void RestartSetUp()
    {
        lastestSpawnPoint = defaultSpawnPoint;
        ActiveMainCharacter();
    }

    public void ActiveMainCharacter()
    {
        RespawnMainCharacter();
    }

    public void SetLatestSpawnPoint(Transform spawnPoint)
    {
        lastestSpawnPoint = spawnPoint;
        mainCharacter.EnableEntity();
        GameManager.Ins.RespawnEnemyAndBoss();
    }

    public void RespawnMainCharacter()
    {
        if (lastestSpawnPoint != null)
        {
            mainCharacter.transform.position = lastestSpawnPoint.position;
        }
        mainCharacter.EnableEntity();
    }
}
