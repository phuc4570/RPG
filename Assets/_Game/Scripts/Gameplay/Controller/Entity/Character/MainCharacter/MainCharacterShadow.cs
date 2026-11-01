using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainCharacterShadow : MonoBehaviour, ITickable, IDamageable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D hitbox;

    private static MainCharacter mainCharacter;
    private const float timeWindow = 0.1f;
    private const float shadowShowDuration = 0.75f;

    private bool isInitialized = false;
    private bool isPerfectDodge;
    private bool isShadowActive;
    private float timer = 0f;

    private void OnEnable()
    {
        StateManager.Ins?.Register(this);
    }

    private void OnDisable()
    {
        isInitialized = false;
        StateManager.Ins?.Unregister(this);
    }

    public void Init(MainCharacter mainCharacter)
    {
        if(MainCharacterShadow.mainCharacter == null)
        {
            MainCharacterShadow.mainCharacter = mainCharacter;
        }
        transform.position = mainCharacter.transform.position;
        spriteRenderer.transform.localScale = mainCharacter.transform.localScale;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        hitbox.enabled = true;
        isPerfectDodge = true;
        isShadowActive = false;
        timer = 0f;
        isInitialized = true;
    }

    public void CustomUpdate(float deltaTime)
    {
        if (isInitialized)
        {
            timer += deltaTime;
            if (isShadowActive)
            {
                if (timer >= shadowShowDuration * 0.8f)
                {
                    float alpha = Mathf.Lerp(0.3f, 0f, (timer - shadowShowDuration * 0.8f) / (shadowShowDuration * 0.2f));
                    spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
                }
                if (timer >= shadowShowDuration)
                {
                    PoolSystem.Ins.ReturnObject(this);
                }
            }
            else
            {
                if (timer >= timeWindow)
                {
                    isPerfectDodge = false;
                    PoolSystem.Ins.ReturnObject(this);
                }
            }
        }
    }

    public void CustomFixedUpdate(float fixedDeltaTime)
    {

    }

    public void TakeDamage(DamageData damageData)
    {
        if (isPerfectDodge)
        {
            timer = 0f;
            isShadowActive = true;
            hitbox.enabled = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            mainCharacter.OnPerfectDodge();
        }
    }
}
