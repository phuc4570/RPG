using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIBossInfo : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private RectTransform healthBarFillRect;
    [SerializeField] private RectTransform poiseBarFillRect;
    [SerializeField] private float originHealthBarFillRectWidth;
    [SerializeField] private float originPoiseBarFillRectWidth;

    private void OnEnable()
    {
        GameEvent.OnUpdateBossHealthBar += UpdateHealthBar;
        GameEvent.OnUpdateBossPoiseBar += UpdatePoiseBar;
    }

    private void OnDisable()
    {
        GameEvent.OnUpdateBossHealthBar -= UpdateHealthBar;
        GameEvent.OnUpdateBossPoiseBar -= UpdatePoiseBar;
    }

    public void SetUp(string bossName)
    {
        bossNameText.SetText(bossName);
        healthBarFillRect.sizeDelta = new Vector2(originHealthBarFillRectWidth, healthBarFillRect.sizeDelta.y);
        poiseBarFillRect.sizeDelta = new Vector2(originPoiseBarFillRectWidth, poiseBarFillRect.sizeDelta.y);
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.5f);
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        float newWidth = originHealthBarFillRectWidth * healthPercentage;
        healthBarFillRect.sizeDelta = new Vector2(newWidth, healthBarFillRect.sizeDelta.y);
    }

    private void UpdatePoiseBar(float poisePercentage)
    {
        float newWidth = originPoiseBarFillRectWidth * poisePercentage;
        poiseBarFillRect.sizeDelta = new Vector2(newWidth, poiseBarFillRect.sizeDelta.y);
    }
}
