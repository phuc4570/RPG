using System;

[Serializable]
public class StatusEffect
{
    public StatusEffectType Type { get; private set; }
    public float Duration { get; set; }
    public float Value { get; private set; }

    public StatusEffect(StatusEffectType type, float duration, float value = 0f)
    {
        Type = type;
        Duration = duration;
        Value = value;
    }
}
