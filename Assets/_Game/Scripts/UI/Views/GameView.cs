using UnityEngine;

public class GameView : UIView
{
#region Static Method
	public static GameView Show(params object[] @params) => ViewController.Ins.Show(ViewDefine.Game, @params) as GameView;
#endregion

	protected override void OnShow()
    {
        base.OnShow();
	}

	protected override void OnHide()
    {
        base.OnHide();
	}
}