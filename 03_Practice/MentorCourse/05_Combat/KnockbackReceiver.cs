using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>Реакция на урон через событие: Health про отброс не знает.</summary>
    [RequireComponent(typeof(Health), typeof(Rigidbody))]
    public class KnockbackReceiver : MonoBehaviour
    {
        [SerializeField] private float _force = 4f;

        private Health _health;
        private Rigidbody _rb;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable() => _health.Damaged += OnDamaged;
        private void OnDisable() => _health.Damaged -= OnDamaged;

        private void OnDamaged(DamageInfo info)
        {
            Vector3 push = Vector3.ProjectOnPlane(info.Direction, Vector3.up).normalized;
            _rb.AddForce(push * _force, ForceMode.VelocityChange);
        }
    }
}
