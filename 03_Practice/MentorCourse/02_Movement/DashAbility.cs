using System.Collections;
using MentorCourse.Basics;
using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>Рывок: корутина задаёт длительность, Cooldown ограничивает частоту.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DashAbility : MonoBehaviour
    {
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _duration = 0.15f;
        [SerializeField] private float _cooldown = 1f;

        private Rigidbody _rb;
        private IMoveInput _input;
        private Cooldown _ready;

        public bool IsDashing { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<IMoveInput>();
        }

        private void Update()
        {
            if (!_input.ConsumeDash() || IsDashing || !_ready.IsReady) return;

            Vector3 direction = new Vector3(_input.Move.x, 0f, _input.Move.y);
            if (direction.sqrMagnitude < 0.01f) direction = transform.forward;

            _ready.Start(_cooldown);
            StartCoroutine(Dash(direction.normalized));
        }

        private IEnumerator Dash(Vector3 direction)
        {
            IsDashing = true;
            float speed = _distance / _duration;
            float endTime = Time.time + _duration;
            var wait = new WaitForFixedUpdate();

            while (Time.time < endTime)
            {
                _rb.linearVelocity = direction * speed;
                yield return wait;
            }

            _rb.linearVelocity = Vector3.zero;
            IsDashing = false;
        }

        // Корутина останавливается при выключении, а флаг сам не сбросится.
        private void OnDisable() => IsDashing = false;
    }
}
