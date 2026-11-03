using System.Collections;
using UnityEngine;

public class CoroutineManager : SingletonObject<CoroutineManager>
{
    public void BeginCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void EndCoroutine(IEnumerator routine)
    {
        StopCoroutine(routine);
    }
}
