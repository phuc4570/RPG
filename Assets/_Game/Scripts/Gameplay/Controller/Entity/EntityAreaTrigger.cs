using UnityEngine;

public abstract class EntityAreaTrigger<T> : MonoBehaviour where T : Entity
{
    protected T Entity { get; private set; }
    public void Init(T entity)
    {
        Entity = entity;
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
