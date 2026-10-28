using UnityEngine;

public class BossController : SingletonObject<BossController>
{
    [SerializeField] Boss boss;

    public void ActiveBoss(bool flag)
    {
        boss.gameObject.SetActive(flag);
        SetUpBoss();
        GameEvent.RaiseTriggerBossFight(boss.Data);
    }

    private void SetUpBoss()
    {
        boss.EnableEntity();
        boss.MovementController.Target = MainCharacterController.Ins.MainCharacter.gameObject;
    }
}
