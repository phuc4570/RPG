using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainCharacterCombatInfo : MonoBehaviour
{
    [SerializeField] private Image attackFillImage;
    [SerializeField] private List<Image> fillFocusImages;
    [SerializeField] private Color fullFocusColor;
    [SerializeField] private Color notFullFocusColor;

    private void OnEnable()
    {
        Reset();
        GameEvent.OnVisualAttackFillAnim += VisualAttackFillAnim;
        GameEvent.OnVisualFocusFillAnim += VisualFocusFillAnim;
    }

    private void OnDisable()
    {
        GameEvent.OnVisualAttackFillAnim -= VisualAttackFillAnim;
        GameEvent.OnVisualFocusFillAnim -= VisualFocusFillAnim;
    }

    public void Reset()
    {
        attackFillImage.fillAmount = 0f;
        foreach(var fillFocusImage in fillFocusImages)
        {
            fillFocusImage.fillAmount = 0f;
        }
    }

    private void VisualAttackFillAnim(float duration)
    {
        attackFillImage.fillAmount = 1f;
        attackFillImage.DOFillAmount(0f, duration);
    }

    private void VisualFocusFillAnim(float focusFill)
    {
        for(int i = 0; i < fillFocusImages.Count; i++)
        {
            fillFocusImages[i].fillAmount = 0f;
        }

        for (int i = 0; i < (int)focusFill; i++)
        {
            fillFocusImages[i].fillAmount = 1;
            fillFocusImages[i].color = fullFocusColor;
        }

        float fillAmount = focusFill - (int)focusFill;
        if(fillAmount > 0)
        {
            fillFocusImages[(int)focusFill].fillAmount = fillAmount;
            fillFocusImages[(int)focusFill].color = notFullFocusColor;
        }
    }
}
