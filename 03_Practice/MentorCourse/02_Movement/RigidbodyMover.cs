using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>
    /// Физический персонаж: ускорение, торможение, прыжок, движение относительно камеры.
    /// Настрой Rigidbody: Freeze Rotation X/Z, Interpolate.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _acceleration = 40f;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private Transform _cameraTransform;

        [Header("Ground check")]
        [SerializeField] private Vector3 _groundOffset = new(0f, 0.1f, 0f);
        [SerializeField] private float _groundRadius = 0.3f;
        [SerializeField] private LayerMask _groundMask = ~0;

        private Rigidbody _rb;
        private IMoveInput _input;

        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<IMoveInput>();
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics.CheckSphere(
                transform.position + _groundOffset, _groundRadius, _groundMask,
                QueryTriggerInteraction.Ignore);

            Vector3 target = GetMoveDirection() * _speed;
            Vector3 velocity = _rb.linearVelocity;

            // Горизонталь ведём к цели, вертикаль оставляем гравитации.
            Vector3 horizontal = Vector3.MoveTowards(
                new Vector3(velocity.x, 0f, velocity.z), target, _acceleration * Time.fixedDeltaTime);

            _rb.linearVelocity = new Vector3(horizontal.x, velocity.y, horizontal.z);

            if (_input.ConsumeJump() && IsGrounded) Jump();
        }

        private Vector3 GetMoveDirection()
        {
            Vector2 move = _input.Move;
            if (_cameraTransform == null) return new Vector3(move.x, 0f, move.y);

            Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;
            return forward * move.y + right * move.x;
        }

        private void Jump()
        {
            // v = √(2gh): дизайнер задаёт высоту в метрах, а не абстрактную силу.
            float velocity = Mathf.Sqrt(2f * -Physics.gravity.y * _jumpHeight);
            Vector3 current = _rb.linearVelocity;
            _rb.linearVelocity = new Vector3(current.x, velocity, current.z);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + _groundOffset, _groundRadius);
        }
    }
}
