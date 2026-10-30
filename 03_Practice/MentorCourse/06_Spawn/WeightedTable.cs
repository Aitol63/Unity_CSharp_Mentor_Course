using System;
using System.Collections.Generic;

namespace MentorCourse.Spawn
{
    /// <summary>Случайный выбор с весами: лут, типы врагов, события.</summary>
    public sealed class WeightedTable<T>
    {
        private readonly List<(T Item, float Weight)> _entries = new();
        private float _total;

        public void Add(T item, float weight)
        {
            if (weight <= 0f) throw new ArgumentOutOfRangeException(nameof(weight));
            _entries.Add((item, weight));
            _total += weight;
        }

        /// <param name="random01">Случайное число от 0 до 1. Передаётся снаружи, чтобы код был тестируемым.</param>
        public T Pick(float random01)
        {
            if (_entries.Count == 0) throw new InvalidOperationException("Таблица пуста");

            float point = random01 * _total;

            foreach ((T item, float weight) in _entries)
            {
                if (point < weight) return item;
                point -= weight;
            }

            return _entries[_entries.Count - 1].Item;
        }
    }
}
