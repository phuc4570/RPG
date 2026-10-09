using System;
using System.Collections.Generic;

public abstract class StateFactory
{
    public abstract IState GetState<TStateEnum>(TStateEnum stateType) where TStateEnum : Enum;
    protected abstract IState GetState(int stateType);
}

public abstract class StateFactory<TEntity, TEnum>: StateFactory where TEnum: Enum where TEntity: Entity
{
    protected readonly TEntity entity;
    protected readonly Dictionary<int, IState> stateCache;

    protected bool IsStagesRegistered { get; set; }

    protected StateFactory(TEntity entity)
    {
        this.entity = entity;
        stateCache = new Dictionary<int, IState>();
    }

    protected abstract void RegisterStates();

    protected void RegisterState(TEnum stateType, IState state)
    {
        int key = Convert.ToInt32(stateType);
        stateCache[key] = state;
    }

    public override IState GetState<TStateEnum>(TStateEnum stateType)
    {
        if (!IsStagesRegistered) RegisterStates();
        return GetState(Convert.ToInt32(stateType));
    }

    protected override IState GetState(int stateType)
    {
        if (stateCache.TryGetValue(stateType, out var state))
        {
            return state;
        }
        return default;
    }
}
