public interface ITickable
{
    public abstract void CustomUpdate(float deltaTime);
    public abstract void CustomFixedUpdate(float fixedDeltaTime);
}
