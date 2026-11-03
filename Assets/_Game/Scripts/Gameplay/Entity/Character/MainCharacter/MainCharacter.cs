using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : Character
{
    #region References
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private MainCharacterShadow mainCharacterShadow;
    #endregion

    #region Public Properties
    public MainCharacterInputReader MainCharacterInputReader { get; private set; }
    public MainCharacterMovementController MainCharacterMovementController => MovementController as MainCharacterMovementController;
    public MainCharacterCombatController MainCharacterCombatController => CombatController as MainCharacterCombatController;
    public MainCharacterManaSystem ManaSystem { get; private set; }
    public MainCharacterStaminaSystem StaminaSystem { get; private set; }
    public MainCharacterFocusSystem FocusSystem { get; private set; }
    public MainCharacterHealingPotion HealingPotion { get; private set; }
    #endregion

    public override void EnableEntity()
    {
        base.EnableEntity();
        MainCharacterInputReader.EnableInput(true);
        FocusSystem.ResetFocusFill();
        ManaSystem.AddValue(ManaSystem.MaxValue);
        StaminaSystem.AddValue(StaminaSystem.MaxValue);
        HealingPotion.AddValue(HealingPotion.MaxValue);
    }

    protected override void SetUp()
    {
        SetUpInputReader();
        base.SetUp();
    }

    private void SetUpInputReader()
    {
        MainCharacterInputReader = new MainCharacterInputReader();
    }

    protected override void SetUpStateFactory()
    {
        StateFactory = new MainCharacterStateFactory(this);
    }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new MainCharacterHealthSystem(this);
        ManaSystem = new MainCharacterManaSystem(this);
        StaminaSystem = new MainCharacterStaminaSystem(this);
        FocusSystem = new MainCharacterFocusSystem();
        HealingPotion = new MainCharacterHealingPotion(this);
    }

    protected override void SetUpMovementController()
    {
        MovementController = new MainCharacterMovementController(this);
    }

    protected override void SetUpCombatController()
    {
        CombatController = new MainCharacterCombatController(this);
    }

    protected override void Init()
    {
        InitInputReader();
        base.Init();
    }

    private void InitInputReader()
    {
        MainCharacterInputReader.Init(inputActionAsset);
    }

    protected override void InitMovementController()
    {
        base.InitMovementController();
        MovementController.InitBaseMovementSpeed(5f);
    }

    protected override void InitCombatController()
    {
        base.InitCombatController();
        MainCharacterCombatController.InitDodgeForce(15f);
        MainCharacterCombatController.InitDodgeStaminaCost(10);
        MainCharacterCombatController.InitBlockDamageReduction(0.1f);
        MainCharacterCombatController.InitBlockStaminaCost(5);
        MainCharacterCombatController.InitMainCharacterShadow(mainCharacterShadow);
        CharacterCombatController.InitBaseAttackDamage(10);
        CharacterCombatController.InitBaseAttackSpeed(1);
        CharacterCombatController.InitAttackForce(2);
    }

    protected override void InitProperty()
    {
        base.InitProperty();
        Direction = Vector2.right;
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(120);
        ManaSystem.Init(60);
        StaminaSystem.Init(100);
        HealingPotion.Init(4, 30);
    }

    protected override void BeforeTakeDamage(DamageData damageData)
    {
        base.BeforeTakeDamage(damageData);
        if (MainCharacterCombatController.IsBlocking)
        {
            if (MainCharacterCombatController.IsPerfectBlock)
            {
                TimeManager.Ins.DoHitstop(0.12f);
                MainCharacterCameraController.Ins.ShakeParry();

                damageData.SetDamage(0);
                if (damageData.Source is Enemy)
                {
                    var enemy = (damageData.Source as Enemy);
                    enemy.StatusEffectManager.AddStatusEffect(StatusEffectType.Stunned, enemy.EnemyCombatController.ParryStunDuration);
                }
                if (damageData.Source is Boss)
                {
                    var boss = (damageData.Source as Boss);
                    boss.PoiseSystem.AddValue(-boss.BossCombatController.PoiseParryLost);
                }
            }
            else
            {
                damageData.SetDamage(Mathf.Clamp(Mathf.RoundToInt(damageData.Damage * MainCharacterCombatController.BlockDamageReduction), 1, int.MaxValue));
                StaminaSystem.AddValue(-MainCharacterCombatController.BlockStaminaCost);
            }
        }
    }

    protected override void AfterTakeDamage()
    {
        base.AfterTakeDamage();
        if (!IsAlive)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Dead));
            return;
        }
        if (MainCharacterCombatController.IsPerfectBlock)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Block));
            return;
        }
        StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Hurt));
    }

    protected override void PlayVFXOnDamage(DamageData damageData)
    {
        if (MainCharacterCombatController.IsBlocking)
        {
            if (MainCharacterCombatController.IsPerfectBlock)
            {
                VFXController.PlayVFX(VFXTypeEnum.Parry, Vector3.one);
                SoundManager.Ins.PlaySound(SoundTypeEnum.Parry);
            }
            else
            {
                VFXController.PlayVFX(VFXTypeEnum.Block, Vector3.one);
                SoundManager.Ins.PlaySound(SoundTypeEnum.Block);
            }
        }
        else
        {
            float scaleX = -1f;
            if (damageData != null && damageData.Source != null)
            {
                if (damageData.Source.transform.position.x < transform.position.x)
                {
                    scaleX = 1f;
                }
            }

            Vector3 localScale = new Vector3(scaleX, 1f, 1f);

            switch (damageData.AttackType)
            {
                case AttackTypeEnum.LightAttack:
                    VFXController.PlayVFX(VFXTypeEnum.LightAttack, localScale);
                    break;
                case AttackTypeEnum.HeavyAttack:
                    VFXController.PlayVFX(VFXTypeEnum.HeavyAttack, localScale);
                    break;
            }
            SoundManager.Ins.PlaySound(SoundTypeEnum.Punch);
        }
    }

    public override void PreDisableEntity()
    {
        base.PreDisableEntity();
        MainCharacterInputReader.EnableInput(false);
    }

    public override void DisableEntity()
    {
        base.DisableEntity();
        GameManager.Ins.OnLose();
    }

    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);
        StaminaSystem.CustomUpdate(deltaTime);
        ManaSystem.CustomUpdate(deltaTime);
    }

    public void OnPerfectDodge()
    {
        TimeManager.Ins.DoSlowMotion(0.2f, 1.0f);
        StaminaSystem.AddMaxValue(MainCharacterCombatController.DodgeStaminaCost);
        FocusSystem.IncreaseFocusFill(1f);
    }
}
