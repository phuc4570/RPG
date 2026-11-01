using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager
{
    private static List<StatusEffectType> StatusEffectCache = new List<StatusEffectType>();

    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    public bool CanMove => !dictStatusEffects[StatusEffectType.Rooted] && !dictStatusEffects[StatusEffectType.Stunned] && !dictStatusEffects[StatusEffectType.Freezed];
    public bool CanBeInterupt => !(HasStatusEffect(StatusEffectType.Unstoppable) || HasStatusEffect(StatusEffectType.Stunned) || HasStatusEffect(StatusEffectType.Freezed) || HasStatusEffect(StatusEffectType.Hype));

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
    public bool BeforeAddStatusEffect(StatusEffectType type)
    {
        switch (type)
        {
            case StatusEffectType.Stunned:
            case StatusEffectType.Freezed:
            case StatusEffectType.Rooted:
                return !HasStatusEffect(StatusEffectType.Unstoppable);
            default:
                return true;
        }
    }

    public void AddStatusEffect(StatusEffectType type, float duration, float value = 0f)
    {
        if (type == StatusEffectType.None) return;
        if (!BeforeAddStatusEffect(type)) return;
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
            AfterAddStatusEffect(type);
        }
    }

    public void AfterAddStatusEffect(StatusEffectType type)
    {
        switch (type)
        {
            case StatusEffectType.Unstoppable:
                RemoveStatusEffect(StatusEffectType.Stunned);
                RemoveStatusEffect(StatusEffectType.Freezed);
                RemoveStatusEffect(StatusEffectType.Rooted);
                break;
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
                RemoveStatusEffect(activeStatusEffects[i].Type);
            }
        }
    }

    public bool HasStatusEffect(StatusEffectType type)
    {
        return dictStatusEffects[type];
    }

    public void RemoveStatusEffect(StatusEffectType type)
    {
        activeStatusEffects.RemoveAll(e => e.Type == type);
        HandleStatusEffect(type, false);
    }

    public void ClearStatusEffects()
    {
        activeStatusEffects.Clear();
        foreach (var statusEffect in StatusEffectCache)
        {
            HandleStatusEffect(statusEffect, false);
        }
    }

    private void HandleStatusEffect(StatusEffectType type, bool isAdded)
    {
        dictStatusEffects[type] = isAdded;
        entity.HandleStatusEffect(type, isAdded);
    }
}
