using UnityEngine;

public abstract class EntityAreaTrigger<T> : MonoBehaviour where T : Entity
{
    [SerializeField] private Collider2D area;
    protected T Entity { get; private set; }
    public Collider2D Area => area;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(area == null)
        {
            area = GetComponent<Collider2D>();
        }
    }
#endif

    public void Init(T entity)
    {
        Entity = entity;
        OnValidate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Entity != null)
        {
            TriggerEnter(collision);
        }
    }

    protected virtual void TriggerEnter(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Entity != null)
        {
            TriggerExit(collision);
        }
    }

    protected virtual void TriggerExit(Collider2D collision)
    {

    }
}
