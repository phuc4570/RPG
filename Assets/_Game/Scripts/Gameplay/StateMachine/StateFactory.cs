using System;
using System.Collections.Generic;

public abstract class StateFactory
{
    protected abstract void RegisterStates();
    public abstract IState GetState<TStateEnum>(TStateEnum stateType) where TStateEnum : Enum;
}

public abstract class StateFactory<TEntity, TEnum>: StateFactory where TEnum: Enum where TEntity: Entity
{
    protected TEntity Entity { get; set; }
    protected readonly Dictionary<int, IState> stateCache;

    protected bool IsStagesRegistered { get; set; }

    protected StateFactory(TEntity entity)
    {
        Entity = entity;
        stateCache = new Dictionary<int, IState>();
        if (!IsStagesRegistered) RegisterStates();
    }

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

    protected IState GetState(int stateType)
    {
        if (stateCache.TryGetValue(stateType, out var state))
        {
            return state;
        }
        return default;
    }
}
