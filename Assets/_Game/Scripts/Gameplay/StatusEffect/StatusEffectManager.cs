using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager
{
    private static List<StatusEffectType> StatusEffectCache = new List<StatusEffectType>();

    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    public bool CanMove => !dictStatusEffects[StatusEffectType.Rooted] && !dictStatusEffects[StatusEffectType.Stunned] && !dictStatusEffects[StatusEffectType.Freezed];
    public bool CanBeInterupt => !dictStatusEffects[StatusEffectType.Unstoppable];

    private Dictionary<StatusEffectType, bool> dictStatusEffects = new Dictionary<StatusEffectType, bool>();

    private Entity entity;

    public void Init(Entity entity)
    {
        this.entity = entity;

        if(StatusEffectCache.Count == 0)
        {
            var stringEnums = Enum.GetNames(typeof(StatusEffectType));
            foreach(var stringEnum in stringEnums)
            {
                if (Enum.TryParse<StatusEffectType>(stringEnum, out var result))
                {
                    StatusEffectCache.Add(result);
                }
            }
        }

        foreach (var statusEffect in StatusEffectCache)
        {
            dictStatusEffects[statusEffect] = false;
        }
    }

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
            HandleStatusEffect(type, true);
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
                StatusEffectType type = activeStatusEffects[i].Type;
                activeStatusEffects.RemoveAt(i);
                HandleStatusEffect(type, false);
            }
        }
    }

    public bool HasStatusEffect(StatusEffectType type)
    {
        return dictStatusEffects[type];
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
        foreach (var statusEffect in StatusEffectCache)
        {
            dictStatusEffects[statusEffect] = false;
        }
    }

    private void HandleStatusEffect(StatusEffectType type, bool isAdded)
    {
        dictStatusEffects[type] = isAdded;
        entity.HandleStatusEffect(type, isAdded);
    }
}
