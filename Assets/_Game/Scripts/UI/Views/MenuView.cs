using UnityEngine;
using UnityEngine.UI;

public class MenuView : UIView
{
#region Static Method
	public static MenuView Show(params object[] @params) => ViewController.Ins.Show(ViewDefine.Menu, @params) as MenuView;
	#endregion

	[SerializeField] private Button playButton;
	[SerializeField] private Button settingButton;
	[SerializeField] private Button quitButton;

	protected override void OnShow()
    {
        base.OnShow();
	}

	protected override void OnHide()
    {
        base.OnHide();
	}

	private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        Hide();
        GameEvent.RaiseGameStart();
        GameView.Show();
    }

    private void OnSettingButtonClicked()
    {
        SettingDialog.Show();
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}