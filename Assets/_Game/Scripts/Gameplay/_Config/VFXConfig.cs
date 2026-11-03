using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VFXConfig : SingletonScriptableObject<VFXConfig>
{
    [TableList]
    [SerializeField] private List<VFXConfigData> data = new List<VFXConfigData>();

    private Dictionary<VFXTypeEnum, VFXConfigData> dict = new Dictionary<VFXTypeEnum, VFXConfigData>();

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

    public VFXConfigData GetData(VFXTypeEnum type)
    {
        if (!isInitialized) Init();
        if (dict.TryGetValue(type, out VFXConfigData result))
        {
            return result;
        }
        return null;
    }
}

[Serializable]
public class VFXConfigData
{
    [SerializeField] private VFXTypeEnum type;
    [SerializeField] private ParticleSystem prefab;
    public VFXTypeEnum Type => type;
    public ParticleSystem Prefab => prefab;
}
