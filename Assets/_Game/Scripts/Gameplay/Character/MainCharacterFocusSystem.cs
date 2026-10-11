using UnityEngine;

public class MainCharacterFocusSystem : MonoBehaviour
{
    private const int maxFocusCount = 3;
    private int focusCount = 0;

    public void IncreaseFocus()
    {
        focusCount++;
        if (focusCount > maxFocusCount)
        {
            focusCount = maxFocusCount;
        }
        UpdateFocusState();
    }

    public bool ConsumeFocus()
    {
        if (focusCount > 0)
        {
            focusCount--;
            UpdateFocusState();
            return true;
        }
        return false;
    }

    private void UpdateFocusState()
    {

    }
}
