using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>Классический 2D-платформер на Rigidbody2D.</summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Platformer2DMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 8f;
        [SerializeField] private float _acceleration = 70f;
        [SerializeField] private float _jumpVelocity = 13f;
        [SerializeField] private float _coyoteTime = 0.1f;
        [SerializeField] private float _jumpBuffer = 0.1f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundRadius = 0.15f;
        [SerializeField] private LayerMask _groundMask;

        private Rigidbody2D _rb;
        private IMoveInput _input;
        private float _coyoteTimer;
        private float _bufferTimer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<IMoveInput>();
        }

        private void FixedUpdate()
        {
            bool grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundMask);

            _coyoteTimer = grounded ? _coyoteTime : _coyoteTimer - Time.fixedDeltaTime;
            _bufferTimer = _input.ConsumeJump() ? _jumpBuffer : _bufferTimer - Time.fixedDeltaTime;

            Vector2 velocity = _rb.linearVelocity;
            velocity.x = Mathf.MoveTowards(
                velocity.x, _input.Move.x * _speed, _acceleration * Time.fixedDeltaTime);

            if (_coyoteTimer > 0f && _bufferTimer > 0f)
            {
                velocity.y = _jumpVelocity;
                _coyoteTimer = 0f;
                _bufferTimer = 0f;
            }

            _rb.linearVelocity = velocity;
        }
    }
}
