using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterConfig<TData, TEnum> : SingletonScriptableObject<CharacterConfig<TData, TEnum>> where TData : CharacterConfigData<TEnum> where TEnum : Enum
{
    [TableList]
    [SerializeField] private List<CharacterConfigData<TEnum>> data = new List<CharacterConfigData<TEnum>>();

    public IReadOnlyList<CharacterConfigData<TEnum>> Data => data;
}

[Serializable]
public class CharacterConfigData<TEnum> where TEnum : Enum
{
    [SerializeField] private TEnum type;
    [SerializeField] private int health;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private int baseAttackDamage;

    public TEnum Type => type;
    public int Health => health;
    public float BaseMovementSpeed => baseMovementSpeed;
    public int BaseAttackDamage => baseAttackDamage;
}
