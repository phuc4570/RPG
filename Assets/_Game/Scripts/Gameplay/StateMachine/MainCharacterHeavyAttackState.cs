using UnityEngine;

public class MainCharacterHeavyAttackState : EntityState<MainCharacter>
{
    private const float stateDuration = 0.5f;
    private int chargeLevel = 0;
    private float timer;
    private bool isAttackPerformed = false;
    private bool isBlockPerformed = false;
    private bool isDodgePerformed = false;

    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterHeavyAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        entity.MainCharacterMovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.HeavyAttack, entity.MovementSpeed);

        timer = stateDuration;
        isAttackPerformed = false;
        isBlockPerformed = false;
        isDodgePerformed = false;

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed += HandleLightAttackPerformed;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (isDodgePerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
            }
            else if (isBlockPerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.BlockIdle));
            }
            else if (isAttackPerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack));
            }
            else
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
            }
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed -= HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed -= HandleLightAttackPerformed;
    }

    public void SetChargeLevel(int level)
    {
        chargeLevel = level;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        if (isBlocking)
        {
            isBlockPerformed = true;
        }
    }

    private void HandleDodgePerformed()
    {
        isDodgePerformed = true;
    }

    private void HandleLightAttackPerformed()
    {
        isAttackPerformed = true;
    }
}
