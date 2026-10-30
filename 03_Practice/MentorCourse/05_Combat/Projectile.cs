using System;
using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>
    /// Снаряд проверяет путь рейкастом: быстрый объект не пролетит сквозь стену.
    /// Возвращается в пул через колбэк и не знает, кто им владеет.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 3f;
        [SerializeField] private LayerMask _hitMask = ~0;

        private Action<Projectile> _release;
        private GameObject _owner;
        private Vector3 _direction;
        private float _speed;
        private int _damage;
        private float _dieAt;

        public void Launch(Vector3 direction, float speed, int damage, GameObject owner,
                           Action<Projectile> release)
        {
            _direction = direction.normalized;
            _speed = speed;
            _damage = damage;
            _owner = owner;
            _release = release;
            _dieAt = Time.time + _lifetime;
            transform.rotation = Quaternion.LookRotation(_direction);
        }

        private void Update()
        {
            if (Time.time >= _dieAt)
            {
                Finish();
                return;
            }

            float step = _speed * Time.deltaTime;

            if (Physics.Raycast(transform.position, _direction, out RaycastHit hit, step, _hitMask,
                                QueryTriggerInteraction.Ignore)
                && !hit.transform.IsChildOf(_owner.transform))
            {
                transform.position = hit.point;
                if (hit.collider.TryGetComponent(out IDamageable target))
                    target.TakeDamage(new DamageInfo(_damage, hit.point, _direction, _owner));

                Finish();
                return;
            }

            transform.position += _direction * step;
        }

        private void Finish() => _release?.Invoke(this);
    }
}
