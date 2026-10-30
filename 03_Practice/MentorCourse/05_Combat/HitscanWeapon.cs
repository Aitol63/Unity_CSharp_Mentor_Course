using MentorCourse.Basics;
using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>Мгновенный выстрел рейкастом, как у автомата в шутере.</summary>
    public class HitscanWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _range = 100f;
        [SerializeField] private int _damage = 15;
        [SerializeField] private float _fireInterval = 0.1f;
        [SerializeField] private float _spreadDegrees = 1f;
        [SerializeField] private LayerMask _hitMask = ~0;

        private Cooldown _cooldown;

        public bool TryFire()
        {
            if (!_cooldown.IsReady) return false;
            _cooldown.Start(_fireInterval);

            Quaternion spread = Quaternion.Euler(
                Random.Range(-_spreadDegrees, _spreadDegrees),
                Random.Range(-_spreadDegrees, _spreadDegrees), 0f);
            Vector3 direction = _muzzle.rotation * spread * Vector3.forward;

            if (!Physics.Raycast(_muzzle.position, direction, out RaycastHit hit, _range, _hitMask,
                                 QueryTriggerInteraction.Ignore))
                return true;

            if (hit.collider.TryGetComponent(out IDamageable target))
                target.TakeDamage(new DamageInfo(_damage, hit.point, direction, gameObject));

            return true;
        }
    }
}
