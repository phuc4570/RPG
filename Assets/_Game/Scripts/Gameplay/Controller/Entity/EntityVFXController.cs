using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVFXController
{
    private Transform standingPoint;
    private Transform bodyPoint;
    private Transform bodyRevertPoint;

    private Dictionary<VFXTypeEnum, List<ParticleSystem>> vfxInstances = new Dictionary<VFXTypeEnum, List<ParticleSystem>>();

    public virtual void Init(Transform standingPoint, Transform bodyPoint, Transform bodyRevertPoint)
    {
        this.standingPoint = standingPoint;
        this.bodyPoint = bodyPoint;
        this.bodyRevertPoint = bodyRevertPoint;
    }

    public virtual void PlayVFX(VFXTypeEnum type, Vector3 localScale)
    {
        var vfxData = VFXConfig.Ins.GetData(type);
        if (vfxData == null || vfxData.Prefab == null) return;
        var vfxInstance = PoolSystem.Ins.RequiredObject(vfxData.Prefab) as ParticleSystem;
        if (!vfxInstances.ContainsKey(type))
        {
            vfxInstances[type] = new List<ParticleSystem>();
        }
        vfxInstances[type].Add(vfxInstance);
        switch (type)
        {
            case VFXTypeEnum.Healing:
                vfxInstance.transform.position = standingPoint.position;
                break;
            case VFXTypeEnum.Block:
            case VFXTypeEnum.Parry:
                vfxInstance.transform.position = bodyPoint.position;
                break;
            case VFXTypeEnum.LightAttack:
            case VFXTypeEnum.HeavyAttack:
                vfxInstance.transform.position = bodyRevertPoint.position;
                break;
        }
        vfxInstance.transform.localScale = localScale;
        vfxInstance.Play();
        CoroutineManager.Ins.BeginCoroutine(WaitParticleRoutine(vfxInstance));
    }

    public virtual void StopVFX(VFXTypeEnum type)
    {
        if (vfxInstances.ContainsKey(type))
        {
            foreach (var vfxInstance in vfxInstances[type])
            {
                vfxInstance.Stop();
                PoolSystem.Ins.ReturnObject(vfxInstance);
            }
            vfxInstances.Remove(type);
        }
    }

    private IEnumerator WaitParticleRoutine(ParticleSystem ps)
    {
        yield return null;

        while (ps.isPlaying)
        {
            yield return null;
        }

        PoolSystem.Ins.ReturnObject(ps);
    }
}

public class EntityVFXController<T> : EntityVFXController where T : Entity
{
    public T Entity { get; }
    protected EntityVFXController(T entity)
    {
        Entity = entity;
    }
}