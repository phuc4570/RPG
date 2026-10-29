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

        for (int i = 0; i < tickables.Count; i++)
        {
            tickables[i].CustomUpdate(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;

        for (int i = 0; i < tickables.Count; i++)
        {
            tickables[i].CustomFixedUpdate(fixedDeltaTime);
        }
    }
}
