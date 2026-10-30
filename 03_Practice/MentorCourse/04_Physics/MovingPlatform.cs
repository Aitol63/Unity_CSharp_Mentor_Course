using UnityEngine;

namespace MentorCourse.PhysicsPractice
{
    /// <summary>Кинематическая платформа: движение через MovePosition в FixedUpdate.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _waitTime = 0.5f;

        private Rigidbody _rb;
        private int _index;
        private float _resumeAt;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void FixedUpdate()
        {
            if (_points.Length == 0 || Time.time < _resumeAt) return;

            Vector3 target = _points[_index].position;
            _rb.MovePosition(Vector3.MoveTowards(_rb.position, target, _speed * Time.fixedDeltaTime));

            if ((_rb.position - target).sqrMagnitude > 0.0001f) return;

            _index = (_index + 1) % _points.Length;
            _resumeAt = Time.time + _waitTime;
        }
    }
}
