namespace EntitySystem.MovementSystem
{
    public interface IEntityMovement
    {
        void HandleMovement(Entity entity);
        void Reset(Entity entity);
    }
}