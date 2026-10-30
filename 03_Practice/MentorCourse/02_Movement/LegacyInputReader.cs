using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>
    /// Читает старый Input. Нужна настройка Active Input Handling = Both.
    /// Нажатия запоминаются, поэтому FixedUpdate их не потеряет.
    /// </summary>
    public class LegacyInputReader : MonoBehaviour, IMoveInput
    {
        private bool _jumpQueued;
        private bool _dashQueued;

        public Vector2 Move { get; private set; }
        public bool JumpHeld { get; private set; }

        private void Update()
        {
            Move = Vector2.ClampMagnitude(
                new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);

            JumpHeld = Input.GetKey(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Space)) _jumpQueued = true;
            if (Input.GetKeyDown(KeyCode.LeftShift)) _dashQueued = true;
        }

        public bool ConsumeJump()
        {
            bool value = _jumpQueued;
            _jumpQueued = false;
            return value;
        }

        public bool ConsumeDash()
        {
            bool value = _dashQueued;
            _dashQueued = false;
            return value;
        }
    }
}
