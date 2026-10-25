using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer healthBarFill;
    [SerializeField] private float originHealthBarFillWidth;

    public void UpdateHealthBar(float healthPercentage)
    {
        float newWidth = originHealthBarFillWidth * healthPercentage;
        healthBarFill.size = new Vector2(newWidth, healthBarFill.size.y);
    }
}
