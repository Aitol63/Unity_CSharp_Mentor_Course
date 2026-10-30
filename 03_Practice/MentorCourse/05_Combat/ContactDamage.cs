using System.Collections.Generic;
using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>Лава или шипы: повторяющийся урон, пока цель в триггере.</summary>
    [RequireComponent(typeof(Collider))]
    public class ContactDamage : MonoBehaviour
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _interval = 0.5f;

        private readonly Dictionary<IDamageable, float> _nextHit = new();

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable target)) return;

            _nextHit.TryGetValue(target, out float time);
            if (Time.time < time) return;

            _nextHit[target] = Time.time + _interval;
            Vector3 point = other.ClosestPoint(transform.position);
            target.TakeDamage(new DamageInfo(_damage, point, Vector3.up, gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable target)) _nextHit.Remove(target);
        }
    }
}
