using UnityEngine;

public class MainCharacterStaminaSystem : BarStatSystem
{
    private const float staminaGainInterval = 0.1f;
    private const int staminaGain = 1;
    private const float staminaNotUsingInterval = 1f;

    private float staminaGainTimer = 0f;
    private float staminaNotUsingTimer = 0f;
    private bool isStaminaNotUsing = false;

    public MainCharacterStaminaSystem(int maxValue, int value = -1) : base(maxValue, value)
    {

    }

    public override void CustomUpdate(float deltaTime)
    {
        if (isStaminaNotUsing)
        {
            if (PercentageValue < 1f)
            {
                staminaGainTimer += deltaTime;
                if (staminaGainTimer >= staminaGainInterval)
                {
                    staminaGainTimer -= staminaGainInterval;
                    AddValue(staminaGain);
                }
            }
        }
        else
        {
            staminaNotUsingTimer += deltaTime;
            if(staminaNotUsingTimer >= staminaNotUsingInterval)
            {
                isStaminaNotUsing = true;
                staminaGainTimer = 0f;
            }
        }
    }

    protected override void OnUpdateMaxValue()
    {

    }

    protected override void OnUpdateValue()
    {
        GameEvent.RaiseUpdateMainCharacterStaminaBar(PercentageValue);
    }

    protected override void OnValueIsOver()
    {

    }

    protected override void OnAddMaxValue(int value)
    {
        
    }

    protected override void OnAddValue(int value)
    {
        if(value < 0)
        {
            staminaNotUsingTimer = 0f;
            isStaminaNotUsing = false;
        }
    }
}
