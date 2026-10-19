using UnityEngine;

public class MainCharacterFocusSystem
{
    private const float maxFocusFill = 3f;
    private float focusFill = 0f;

    public void IncreaseFocusFill(float amount)
    {
        if (focusFill < maxFocusFill)
        {
            focusFill = Mathf.Clamp(focusFill + amount, 0, maxFocusFill);
            UpdateFocusFill();
        }
    }
    public bool ConsumeFocus()
    {
        if (focusFill >= 1)
        {
            focusFill = Mathf.Clamp(focusFill - 1, 0, maxFocusFill);
            UpdateFocusFill();
            return true;
        }
        return false;
    }

    private void UpdateFocusFill()
    {
        GameEvent.RaiseVisualFocusFillAnim(focusFill);
    }
}