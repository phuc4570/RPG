using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterConfig<TData, TEnum> : SingletonScriptableObject<CharacterConfig<TData, TEnum>> where TData : CharacterConfigData<TEnum> where TEnum : Enum
{
    [TableList]
    [SerializeField] private List<TData> data = new List<TData>();

    public IReadOnlyList<TData> Data => data;

    private Dictionary<TEnum, TData> dict = new Dictionary<TEnum, TData>();
    private bool isInitialized = false;

    private void Init()
    {
        for(int i = 0; i < data.Count; i++)
        {
            if (!dict.ContainsKey(data[i].Type))
            {
                dict.Add(data[i].Type, data[i]);
            }
        }
    }

    public TData GetData(TEnum type)
    {
        if (!isInitialized) Init();
        if(dict.TryGetValue(type, out TData result))
        {
            return result;
        }
        return null;
    }
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
