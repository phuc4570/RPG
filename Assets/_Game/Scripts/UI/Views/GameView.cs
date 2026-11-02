using UnityEngine;

public class GameView : UIView
{
#region Static Method
	public static GameView Show(params object[] @params) => ViewController.Ins.Show(ViewDefine.Game, @params) as GameView;
	#endregion

	[SerializeField] UIBossInfo uIBossInfoObject;

	protected override void OnShow()
    {
        base.OnShow();
	}

	protected override void OnHide()
    {
        base.OnHide();
	}

    private void OnEnable()
    {
        GameEvent.OnTriggerBossFight += OnTriggerBossFight;
        GameEvent.OnResetBossFight += OnResetBossFight;
    }

    private void OnDisable()
    {
        GameEvent.OnTriggerBossFight -= OnTriggerBossFight;
        GameEvent.OnResetBossFight -= OnResetBossFight;
    }

    private void OnTriggerBossFight(BossConfigData data)
    {
        uIBossInfoObject.gameObject.SetActive(true);
        uIBossInfoObject.SetUp(data);
    }

    private void OnResetBossFight()
    {
        uIBossInfoObject.gameObject.SetActive(false);
    }
}