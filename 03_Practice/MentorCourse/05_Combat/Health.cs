using System;
using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>
    /// Здоровье ничего не знает про UI, звук и анимацию.
    /// Они подписываются на события и реагируют сами.
    /// </summary>
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _max = 100;
        [SerializeField] private float _invulnerabilityTime;

        private float _vulnerableAt;

        public int Max => _max;
        public int Current { get; private set; }
        public bool IsDead => Current <= 0;

        public event Action<int, int> Changed;
        public event Action<DamageInfo> Damaged;
        public event Action Died;

        private void Awake() => Current = _max;

        // Для объектов из пула: OnEnable вызывается при каждом выходе из пула.
        private void OnEnable() => Restore();

        public void TakeDamage(in DamageInfo info)
        {
            if (IsDead || Time.time < _vulnerableAt) return;

            _vulnerableAt = Time.time + _invulnerabilityTime;
            Current = Mathf.Max(0, Current - info.Amount);

            Damaged?.Invoke(info);
            Changed?.Invoke(Current, _max);
            if (IsDead) Died?.Invoke();
        }

        public void Heal(int amount)
        {
            if (IsDead || amount <= 0) return;

            Current = Mathf.Min(_max, Current + amount);
            Changed?.Invoke(Current, _max);
        }

        public void Restore()
        {
            Current = _max;
            _vulnerableAt = 0f;
            Changed?.Invoke(Current, _max);
        }
    }
}
