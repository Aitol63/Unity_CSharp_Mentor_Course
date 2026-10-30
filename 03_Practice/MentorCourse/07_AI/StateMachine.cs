namespace MentorCourse.AI
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }

    public class StateMachine
    {
        public IState Current { get; private set; }

        public void Change(IState next)
        {
            if (ReferenceEquals(Current, next)) return;

            Current?.Exit();
            Current = next;
            Current?.Enter();
        }

        public void Tick() => Current?.Tick();
    }
}
