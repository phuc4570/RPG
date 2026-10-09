using System.Collections;
using UnityEngine;

public class TimerManager : SingletonObject<TimerManager>
{
    private WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    void Start()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return waitForOneSecond;
            GameEvent.RaiseOneSecondPassed();
        }
    }
}
