using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VisualStatusConfig : SingletonScriptableObject<VisualStatusConfig>
{
    [TableList]
    [SerializeField] private List<VisualStatusConfigData> data = new List<VisualStatusConfigData>();

    private Dictionary<StatusEffectType, VisualStatusConfigData> dict = new Dictionary<StatusEffectType, VisualStatusConfigData>();

    private bool isInitialized = false;

    private void Init()
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (!dict.ContainsKey(data[i].Type))
            {
                dict.Add(data[i].Type, data[i]);
            }
        }
    }

    public VisualStatusConfigData GetData(StatusEffectType type)
    {
        if (!isInitialized) Init();
        if (dict.TryGetValue(type, out VisualStatusConfigData result))
        {
            return result;
        }
        return null;
    }
}

[Serializable]
public class VisualStatusConfigData
{
    [SerializeField] private StatusEffectType type;
    [SerializeField] private Sprite icon;
    public StatusEffectType Type => type;
    public Sprite Icon => icon;
}