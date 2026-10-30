using UnityEngine;
using UnityEngine.Pool;

namespace MentorCourse.Spawn
{
    /// <summary>Обёртка над ObjectPool для префабов с компонентом T.</summary>
    public class ComponentPool<T> where T : Component
    {
        private readonly ObjectPool<T> _pool;

        public int ActiveCount => _pool.CountActive;

        public ComponentPool(T prefab, Transform parent, int capacity = 32)
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Object.Instantiate(prefab, parent),
                actionOnGet: item => item.gameObject.SetActive(true),
                actionOnRelease: item => item.gameObject.SetActive(false),
                actionOnDestroy: item => Object.Destroy(item.gameObject),
                defaultCapacity: capacity);
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            T item = _pool.Get();
            item.transform.SetPositionAndRotation(position, rotation);
            return item;
        }

        public void Release(T item) => _pool.Release(item);
    }
}
