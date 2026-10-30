using System;
using UnityEngine;

namespace MentorCourse.Spawn
{
    /// <summary>Спавн в прямоугольнике по таймеру с пулом и ограничением количества.</summary>
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _prefab;
        [SerializeField] private float _interval = 1f;
        [SerializeField] private Vector2 _area = new(16f, 16f);
        [SerializeField] private int _maxActive = 20;

        private ComponentPool<Coin> _pool;
        private float _nextSpawn;

        public event Action<int> Scored;

        private void Awake() => _pool = new ComponentPool<Coin>(_prefab, transform);

        private void Update()
        {
            if (Time.time < _nextSpawn || _pool.ActiveCount >= _maxActive) return;

            _nextSpawn = Time.time + _interval;
            Vector3 offset = new Vector3(
                UnityEngine.Random.Range(-_area.x, _area.x) * 0.5f, 0.5f,
                UnityEngine.Random.Range(-_area.y, _area.y) * 0.5f);

            Coin coin = _pool.Get(transform.position + offset, Quaternion.identity);
            coin.Collected += OnCollected;
        }

        private void OnCollected(Coin coin, GameObject collector)
        {
            // Без отписки объект из пула накопил бы подписки и считал очки несколько раз.
            coin.Collected -= OnCollected;
            _pool.Release(coin);
            Scored?.Invoke(coin.Value);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, new Vector3(_area.x, 0.1f, _area.y));
        }
    }
}
