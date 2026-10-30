using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace MentorCourse.Performance
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }

    /// <summary>
    /// Один Update вместо сотен. Снятие с регистрации безопасно даже во время обхода.
    /// ProfilerMarker показывает эту работу отдельной строкой в Profiler.
    /// </summary>
    public class TickManager : MonoBehaviour
    {
        private static readonly ProfilerMarker Marker = new("TickManager.Update");

        private readonly List<ITickable> _items = new(256);

        public void Register(ITickable item) => _items.Add(item);

        public void Unregister(ITickable item)
        {
            int index = _items.IndexOf(item);
            if (index >= 0) _items[index] = null;
        }

        private void Update()
        {
            using (Marker.Auto())
            {
                float deltaTime = Time.deltaTime;

                for (int i = _items.Count - 1; i >= 0; i--)
                {
                    ITickable item = _items[i];

                    if (item != null)
                    {
                        item.Tick(deltaTime);
                        continue;
                    }

                    // Быстрое удаление: последний элемент встаёт на место пустого.
                    _items[i] = _items[_items.Count - 1];
                    _items.RemoveAt(_items.Count - 1);
                }
            }
        }
    }
}
