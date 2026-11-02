using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : UIView
{
#region Static Method
	public static ResultView Show(params object[] @params) => ViewController.Ins.Show(ViewDefine.Result, @params) as ResultView;
	#endregion

	[SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    public bool IsWin { get; private set; }

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
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public void SetUp(bool isWin)
    {
        IsWin = isWin;
        title.text = IsWin ? "You Win!" : "You Lose!";
    }

    private void OnRestartButtonClicked()
    {
        Hide();
        GameView.Show();
        if (IsWin)
        {
            MainCharacterController.Ins.RestartSetUp();
        }
        else
        {
            MainCharacterController.Ins.RespawnMainCharacter();
        }
        GameManager.Ins.RespawnEnemyAndBoss();
    }

    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Hide();
    }
}