using UnityEngine;

public class MainCharacterManaSystem : BarStatSystem
{
    private const float manaGainInterval = 0.5f;
    private const int manaGain = 1;
    private const float manaNotUsingInterval = 0.5f;

    private float manaGainTimer = 0f;
    private float manaNotUsingTimer = 0f;
    private bool isManaNotUsing = false;

    public MainCharacterManaSystem(int maxValue, int value = -1) : base(maxValue, value)
    {

    }

    public override void CustomUpdate(float deltaTime)
    {
        if (isManaNotUsing)
        {
            if (PercentageValue < 1f)
            {
                manaGainTimer += deltaTime;
                if (manaGainTimer >= manaGainInterval)
                {
                    manaGainTimer -= manaGainInterval;
                    AddValue(manaGain);
                }
            }
        }
        else
        {
            manaNotUsingTimer += deltaTime;
            if (manaNotUsingTimer >= manaNotUsingInterval)
            {
                isManaNotUsing = true;
                manaGainTimer = 0f;
            }
        }
    }

    protected override void OnUpdateMaxValue()
    {

    }

    protected override void OnUpdateValue()
    {
        GameEvent.RaiseUpdateMainCharacterManaBar(PercentageValue);
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
            manaNotUsingTimer = 0f;
            isManaNotUsing = false;
        }
    }
}