using UnityEngine;

public class MainCharacterHealingPotion
{
    private int maxValue;
    public int MaxValue
    {
        get
        {
            return maxValue;
        }
        protected set
        {
            maxValue = value;
        }
    }

    private int value;
    public int Value
    {
        get
        {
            return value;
        }
        protected set
        {
            this.value = value;
            OnUpdateValue();
        }
    }

    private int healingValue;
    public int HealingValue
    {
        get
        {
            return healingValue;
        }
        protected set
        {
            healingValue = value;
        }
    }

    public float PercentageValue => Value * 1.0f / MaxValue;

    private MainCharacter mainCharacter;

    public MainCharacterHealingPotion(int maxValue, int healingValue, MainCharacter mainCharacter)
    {
        MaxValue = maxValue;
        Value = MaxValue;
        HealingValue = healingValue;
        this.mainCharacter = mainCharacter;
    }

    public void UpdateMaxValue(int value)
    {
        int currentValue = Value;
        if (MaxValue < value)
        {
            currentValue = Value + (value - MaxValue);
        }
        else if (MaxValue > value)
        {
            currentValue = Mathf.Min(Value, MaxValue);
        }
        MaxValue = value;
        UpdateValue(currentValue);
    }

    public void AddMaxValue(int value)
    {
        UpdateMaxValue(MaxValue + value);
    }

    public void UpdateValue(int value)
    {
        int clampValue = Mathf.Clamp(value, 0, MaxValue);
        Value = clampValue;
    }

    public void AddValue(int value)
    {
        UpdateValue(Value + value);
        OnAddValue(value);
    }

    public void UpdateHealingValue(int value)
    {
        int clampValue = Mathf.Clamp(value, 0, MaxValue);
        HealingValue = clampValue;
    }

    public void AddHealingValue(int value)
    {
        UpdateHealingValue(Value + value);
    }

    public void Reset()
    {
        UpdateValue(MaxValue);
    }

    private void OnUpdateValue()
    {
        GameEvent.RaiseUpdateMainCharacterHealingPotion(PercentageValue);
    }

    private void OnAddValue(int value)
    {
        if(value < 0)
        {
            mainCharacter.HealthSystem.AddValue(HealingValue);
        }
    }
}
