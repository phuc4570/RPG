using System.Collections.Generic;
using UnityEngine;

public class StateManager : SingletonObject<StateManager>
{
    private readonly List<ITickable> tickables = new List<ITickable>();

    public void Register(ITickable tickable)
    {
        if (!tickables.Contains(tickable)) tickables.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        tickables.Remove(tickable);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        int count = tickables.Count;

        for (int i = 0; i < count; i++)
        {
            tickables[i].CustomUpdate(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        int count = tickables.Count;

        for (int i = 0; i < count; i++)
        {
            tickables[i].CustomFixedUpdate(fixedDeltaTime);
        }
    }
}
