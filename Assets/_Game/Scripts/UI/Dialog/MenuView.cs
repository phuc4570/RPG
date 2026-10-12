using UnityEngine;

public class MenuView : UIView
{
#region Static Method
	public static MenuView Show(params object[] @params) => ViewController.Ins.Show(ViewDefine.Menu, @params) as MenuView;
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