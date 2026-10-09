using System;
using UnityEngine;

//Script để dùng làm ScriptableObject cho config
[Serializable]
public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;
    public static T Ins
    {
        //Load data ScriptableObject từ folder Resources
        get
        {
            if(instance != null) return instance;
            instance = Resources.Load<T>($"ScriptableObject/{typeof(T).Name}");
            if (instance == null)
            {
                T[] assets = Resources.LoadAll<T>("ScriptableObject");
                if (assets != null && assets.Length > 0)
                {
                    instance = assets[0];
                }
            }
            return instance;
        }
    }
}
