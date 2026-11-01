using Unity.Cinemachine;
using UnityEngine;

public class MainCharacterCameraController : SingletonObject<MainCharacterCameraController>
{
    [SerializeField] private CinemachineImpulseSource lightAttackImpulse;
    [SerializeField] private CinemachineImpulseSource heavyAttackImpulse;
    [SerializeField] private CinemachineImpulseSource parryImpulse;

    public void ShakeLightAttack()
    {
        lightAttackImpulse?.GenerateImpulse();
    }

    public void ShakeHeavyAttack()
    {
        heavyAttackImpulse?.GenerateImpulse();
    }

    public void ShakeParry()
    {
        parryImpulse?.GenerateImpulse();
    }
}
