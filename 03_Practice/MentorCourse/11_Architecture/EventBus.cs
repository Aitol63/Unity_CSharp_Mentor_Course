using System;

namespace MentorCourse.Architecture
{
    /// <summary>
    /// Шина для редких глобальных событий. Внутри фичи лучше обычные события:
    /// связи через шину труднее отследить.
    /// </summary>
    public static class EventBus<T>
    {
        public static event Action<T> Raised;

        public static void Raise(T message) => Raised?.Invoke(message);

        public static void Clear() => Raised = null;
    }

    public readonly struct EnemyKilled
    {
        public readonly int Reward;

        public EnemyKilled(int reward) => Reward = reward;
    }
}
