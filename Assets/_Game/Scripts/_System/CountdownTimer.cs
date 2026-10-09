using Unity.VisualScripting;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public int RemainingTime { get; private set; }

    private long theLastTimeUnix;

    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {
        GameEvent.OnOneSecondPassed -= OnOneSecondPassed;
    }

    protected virtual void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            long currentTimeUnix = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (theLastTimeUnix > 0 && RemainingTime > 0)
            {
                int elapsedSeconds = (int)(currentTimeUnix - theLastTimeUnix);
                RemainingTime = Mathf.Max(0, RemainingTime - elapsedSeconds);
                if (RemainingTime <= 0)
                {
                    OnCountdownFinished();
                }
            }
        }
        else
        {
            theLastTimeUnix = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    public virtual void StartCountdown(int duration)
    {
        RemainingTime = duration;
        GameEvent.OnOneSecondPassed += OnOneSecondPassed;
    }

    public virtual void StopCountdown()
    {
        RemainingTime = 0;
        GameEvent.OnOneSecondPassed -= OnOneSecondPassed;
    }

    protected virtual void OnOneSecondPassed()
    {
        if (RemainingTime > 0)
        {
            RemainingTime--;
            if (RemainingTime <= 0)
            {
                OnCountdownFinished();
            }
        }
    }

    protected virtual void OnCountdownFinished()
    {
        GameEvent.OnOneSecondPassed -= OnOneSecondPassed;
    }
}