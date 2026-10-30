using MentorCourse.Basics;
using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>
    /// Удар по области. PerformHit вызывается событием анимации либо напрямую.
    /// Цель получает урон один раз, даже если у неё несколько коллайдеров.
    /// </summary>
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] private Transform _hitPoint;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private int _damage = 20;
        [SerializeField] private float _cooldownTime = 0.8f;
        [SerializeField] private LayerMask _mask;

        private readonly Collider[] _hits = new Collider[16];
        private readonly System.Collections.Generic.HashSet<IDamageable> _damaged = new();
        private Cooldown _cooldown;

        public bool IsReady => _cooldown.IsReady;

        public bool TryStart()
        {
            if (!_cooldown.IsReady) return false;
            _cooldown.Start(_cooldownTime);
            return true;
        }

        public void PerformHit()
        {
            _damaged.Clear();
            int count = Physics.OverlapSphereNonAlloc(_hitPoint.position, _radius, _hits, _mask);

            for (int i = 0; i < count; i++)
            {
                IDamageable target = _hits[i].GetComponentInParent<IDamageable>();
                if (target == null || !_damaged.Add(target)) continue;

                Vector3 direction = (_hits[i].transform.position - transform.position).normalized;
                target.TakeDamage(new DamageInfo(_damage, _hitPoint.position, direction, gameObject));
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_hitPoint == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_hitPoint.position, _radius);
        }
    }
}
