using UnityEngine;
using UnityEngine.UI;

public class UIMainCharacterStatsInfo : MonoBehaviour
{
    [SerializeField] private Image healingPotionImageFill;
    [SerializeField] private RectTransform healthBarFillRect;
    [SerializeField] private RectTransform manaBarFillRect;
    [SerializeField] private RectTransform staminaBarFillRect;

    [SerializeField] private float originHealthBarFillRectWidth;
    [SerializeField] private float originManaBarFillRectWidth;
    [SerializeField] private float originStaminaBarFillRectWidth;

    private void OnEnable()
    {
        GameEvent.OnUpdateMainCharacterHealingPotion += UpdateHealingPotion;
        GameEvent.OnUpdateMainCharacterHealthBar += UpdateHealthBar;
        GameEvent.OnUpdateMainCharacterManaBar += UpdateManaBar;
        GameEvent.OnUpdateMainCharacterStaminaBar += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        GameEvent.OnUpdateMainCharacterHealingPotion -= UpdateHealingPotion;
        GameEvent.OnUpdateMainCharacterHealthBar -= UpdateHealthBar;
        GameEvent.OnUpdateMainCharacterManaBar -= UpdateManaBar;
        GameEvent.OnUpdateMainCharacterStaminaBar -= UpdateStaminaBar;
    }

    private void UpdateHealingPotion(float healingPotionPercentage)
    {
        healingPotionImageFill.fillAmount = 1 - healingPotionPercentage;
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        float newWidth = originHealthBarFillRectWidth * healthPercentage;
        healthBarFillRect.sizeDelta = new Vector2(newWidth, healthBarFillRect.sizeDelta.y);
    }

    private void UpdateManaBar(float manaPercentage)
    {
        float newWidth = originManaBarFillRectWidth * manaPercentage;
        manaBarFillRect.sizeDelta = new Vector2(newWidth, manaBarFillRect.sizeDelta.y);
    }

    private void UpdateStaminaBar(float staminaPercentage)
    {
        float newWidth = originStaminaBarFillRectWidth * staminaPercentage;
        staminaBarFillRect.sizeDelta = new Vector2(newWidth, staminaBarFillRect.sizeDelta.y);
    }
}
