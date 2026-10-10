using UnityEngine;

public class MainCharacterLightAttackState : EntityState<MainCharacter>
{
    private const float comboResetTime = 0.5f;
    private const float stateDuration = 0.4f;

    private int comboIdx = 0;
    private float lastAttackTime;
    private float timer;
    private bool isAttackPerformed = false;
    private bool isBlockPerformed = false;
    private bool isDodgePerformed = false;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterLightAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }
    public override void Enter()
    {
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;

        if (lastAttackTime + comboResetTime < Time.time)
        {
            comboIdx = 0;
        }

        entity.MainCharacterMovementController.StopMovement();
        switch (comboIdx)
        {
            case 0:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack1, entity.MovementSpeed);
                break;
            case 1:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack2, entity.MovementSpeed);
                break;
            case 2:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack3, entity.MovementSpeed);
                break;
        }

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
            lastAttackTime = Time.time;
            comboIdx = (comboIdx + 1) % 3;

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
