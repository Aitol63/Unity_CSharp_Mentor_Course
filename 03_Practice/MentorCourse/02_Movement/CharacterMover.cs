using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>
    /// CharacterController с приёмами отзывчивости: coyote time, jump buffer,
    /// усиленная гравитация при падении и переменная высота прыжка.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _jumpHeight = 1.4f;
        [SerializeField] private float _gravity = -25f;
        [SerializeField] private float _fallMultiplier = 1.8f;
        [SerializeField] private float _coyoteTime = 0.1f;
        [SerializeField] private float _jumpBuffer = 0.1f;

        private CharacterController _controller;
        private IMoveInput _input;
        private float _verticalVelocity;
        private float _coyoteTimer;
        private float _bufferTimer;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<IMoveInput>();
        }

        private void Update()
        {
            UpdateTimers();
            TryJump();
            ApplyGravity();

            Vector3 move = transform.right * _input.Move.x + transform.forward * _input.Move.y;
            Vector3 velocity = move * _speed + Vector3.up * _verticalVelocity;
            _controller.Move(velocity * Time.deltaTime);
        }

        private void UpdateTimers()
        {
            _coyoteTimer = _controller.isGrounded ? _coyoteTime : _coyoteTimer - Time.deltaTime;
            _bufferTimer = _input.ConsumeJump() ? _jumpBuffer : _bufferTimer - Time.deltaTime;
        }

        private void TryJump()
        {
            if (_coyoteTimer <= 0f || _bufferTimer <= 0f) return;

            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _coyoteTimer = 0f;
            _bufferTimer = 0f;
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                // Небольшая прижимающая скорость: иначе isGrounded будет мигать.
                _verticalVelocity = -2f;
                return;
            }

            bool falling = _verticalVelocity < 0f;
            bool jumpReleased = _verticalVelocity > 0f && !_input.JumpHeld;
            float multiplier = falling || jumpReleased ? _fallMultiplier : 1f;

            _verticalVelocity += _gravity * multiplier * Time.deltaTime;
        }
    }
}
