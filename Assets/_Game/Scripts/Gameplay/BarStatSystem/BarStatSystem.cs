using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class BarStatSystem
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
            OnUpdateMaxValue();
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
            if (this.value == 0) OnValueIsOver();
        }
    }

    public float PercentageValue => Value * 1.0f / MaxValue;

    public BarStatSystem()
    {
        maxValue = 1;
        value = 1;
    }

    public virtual void Init(int maxValue, int value = -1)
    {
        MaxValue = maxValue;
        Value = value != -1 ? value : MaxValue;
    }

    public virtual void UpdateMaxValue(int value)
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

    public virtual void AddMaxValue(int value)
    {
        UpdateMaxValue(MaxValue + value);
        OnAddMaxValue(value);
    }

    public virtual void UpdateValue(int value)
    {
        int clampValue = Mathf.Clamp(value, 0, MaxValue);
        Value = clampValue;
    }

    public virtual void AddValue(int value)
    {
        UpdateValue(Value + value);
        OnAddValue(value);
    }

    public virtual void Reset()
    {
        UpdateValue(MaxValue);
    }

    public virtual void CustomUpdate(float deltaTime)
    {

    }

    protected virtual void OnUpdateMaxValue()
    {

    }

    protected virtual void OnUpdateValue()
    {

    }

    protected virtual void OnValueIsOver()
    {

    }

    protected virtual void OnAddMaxValue(int value)
    {

    }

    protected virtual void OnAddValue(int value)
    {

    }
}

public class BarStatSystem<T> : BarStatSystem where T : Entity
{
    protected T Entity { get; }

    public BarStatSystem(T entity)
    {
        Entity = entity;
    }

    protected override void OnUpdateValue()
    {
        base.OnUpdateValue();
        Entity.OnHealthValueUpdated();
    }
}
