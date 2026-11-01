using System.Collections;
using UnityEngine;

public class TimeManager : SingletonObject<TimeManager>
{
    private WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    private float originalFixedDeltaTime;

    void Start()
    {
        StartCoroutine(Timer());
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return waitForOneSecond;
            GameEvent.RaiseOneSecondPassed();
        }
    }

    public void DoSlowMotion(float targetTimeScale, float duration)
    {
        StartCoroutine(SlowMotionRoutine(targetTimeScale, duration));
    }

    private IEnumerator SlowMotionRoutine(float targetScale, float duration)
    {
        Time.timeScale = targetScale;
        Time.fixedDeltaTime = originalFixedDeltaTime * targetScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }

    public void DoHitstop(float duration)
    {
        StartCoroutine(HitstopRoutine(duration));
    }

    private IEnumerator HitstopRoutine(float duration)
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
    }
}
