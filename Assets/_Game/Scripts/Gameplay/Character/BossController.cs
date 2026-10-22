using UnityEngine;

public class BossController : SingletonObject<BossController>
{
    [SerializeField] Boss boss;

    public void ActiveBoss(bool flag)
    {
        boss.gameObject.SetActive(flag);
        SetUpBoss();
        GameEvent.RaiseTriggerBossFight(null);
    }

    private void SetUpBoss()
    {
        boss.SetUp();
        boss.MovementController.Target = MainCharacterController.Ins.MainCharacter.gameObject;
    }
}
