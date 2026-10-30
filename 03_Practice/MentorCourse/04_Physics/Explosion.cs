using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.PhysicsPractice
{
    /// <summary>Урон и отброс по радиусу без аллокаций: буфер создаётся один раз.</summary>
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _maxDamage = 80;
        [SerializeField] private float _force = 800f;
        [SerializeField] private LayerMask _mask = ~0;
        [SerializeField] private LayerMask _obstacleMask;

        private readonly Collider[] _hits = new Collider[64];

        [ContextMenu("Explode")]
        public void Explode()
        {
            Vector3 center = transform.position;
            int count = Physics.OverlapSphereNonAlloc(center, _radius, _hits, _mask);

            for (int i = 0; i < count; i++)
            {
                Collider hit = _hits[i];
                Vector3 point = hit.ClosestPoint(center);

                if (Physics.Linecast(center, point, _obstacleMask)) continue;

                if (hit.attachedRigidbody != null)
                    hit.attachedRigidbody.AddExplosionForce(_force, center, _radius);

                if (hit.TryGetComponent(out IDamageable target))
                {
                    float falloff = 1f - Mathf.Clamp01(Vector3.Distance(center, point) / _radius);
                    int damage = Mathf.CeilToInt(_maxDamage * falloff);
                    target.TakeDamage(new DamageInfo(damage, point, (point - center).normalized, gameObject));
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
