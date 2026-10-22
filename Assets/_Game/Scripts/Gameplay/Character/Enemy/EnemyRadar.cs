using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    [SerializeField] Enemy enemy;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
    }
#endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy.MovementController.Target = collision.GetComponentInParent<MainCharacter>().gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.MovementController.Target = null;
    }
}
