using System;
using System.Collections.Generic;

namespace MentorCourse.Basics
{
    /// <summary>Generic-пул для обычных C#-объектов: списков, буферов, команд.</summary>
    public sealed class SimplePool<T> where T : class
    {
        private readonly Stack<T> _free = new();
        private readonly Func<T> _create;
        private readonly Action<T> _reset;

        public int FreeCount => _free.Count;

        public SimplePool(Func<T> create, Action<T> reset = null)
        {
            _create = create ?? throw new ArgumentNullException(nameof(create));
            _reset = reset;
        }

        public T Get() => _free.Count > 0 ? _free.Pop() : _create();

        public void Release(T item)
        {
            _reset?.Invoke(item);
            _free.Push(item);
        }
    }
}
