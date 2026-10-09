using UnityEngine;

//Class abstract để tạo các Singleton
public abstract class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
{
    private static T ins;
    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                ins = FindFirstObjectByType<T>();
            }
            return ins;
        }
    }

    [SerializeField] protected bool destroyOnLoad;

    protected virtual void Awake()
    {
        if (ins != null && ins != this)
        {
            enabled = false;
            Destroy(gameObject);
            return;
        }

        ins = (T)this;

        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (ins == (T)this)
        {
            ins = null;
        }
    }
}
