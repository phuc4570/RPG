using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager
{
    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    public void AddStatusEffect(StatusEffectType type, float duration, float value = 0f)
    {
        if (type == StatusEffectType.None) return;
        var existingEffect = activeStatusEffects.Find(e => e.Type == type);
        if (existingEffect != null)
        {
            if(existingEffect.Duration < duration)
            {
                existingEffect.Duration = duration;
            }
        }
        else
        {
            activeStatusEffects.Add(new StatusEffect(type, duration, value));
        }
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < activeStatusEffects.Count; i++)
        {
            var effect = activeStatusEffects[i];
            effect.Duration -= deltaTime;
            if (effect.Duration <= 0)
            {
                activeStatusEffects.RemoveAt(i);
            }
        }
    }

    public bool HasStatusEffect(StatusEffectType type)
    {
        for(int i = 0; i < activeStatusEffects.Count; i++)
        {
            if (activeStatusEffects[i].Type == type)
            {
                return true;
            }
        }
        return false;
    }

    public float GetStatusEffectValue(StatusEffectType type)
    {
        for(int i = 0; i < activeStatusEffects.Count; i++)
        {
            if (activeStatusEffects[i].Type == type)
            {
                return activeStatusEffects[i].Value;
            }
        }
        return 0f;
    }

    public void RemoveStatusEffect(StatusEffectType type)
    {
        activeStatusEffects.RemoveAll(e => e.Type == type);
    }

    public void ClearStatusEffects()
    {
        activeStatusEffects.Clear();
    }
}
