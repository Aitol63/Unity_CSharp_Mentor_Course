#if MENTOR_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;

namespace MentorCourse.Movement
{
    /// <summary>
    /// Та же логика на пакете Input System. Файл компилируется, только если пакет установлен.
    /// В инспекторе назначь действия из ассета Input Actions.
    /// </summary>
    public class InputSystemReader : MonoBehaviour, IMoveInput
    {
        [SerializeField] private InputActionReference _move;
        [SerializeField] private InputActionReference _jump;
        [SerializeField] private InputActionReference _dash;

        private bool _jumpQueued;
        private bool _dashQueued;

        public Vector2 Move { get; private set; }
        public bool JumpHeld => _jump.action.IsPressed();

        private void OnEnable()
        {
            _move.action.Enable();
            _jump.action.Enable();
            _dash.action.Enable();
            _jump.action.performed += OnJump;
            _dash.action.performed += OnDash;
        }

        private void OnDisable()
        {
            _jump.action.performed -= OnJump;
            _dash.action.performed -= OnDash;
            _move.action.Disable();
            _jump.action.Disable();
            _dash.action.Disable();
        }

        private void Update() => Move = _move.action.ReadValue<Vector2>();

        private void OnJump(InputAction.CallbackContext _) => _jumpQueued = true;
        private void OnDash(InputAction.CallbackContext _) => _dashQueued = true;

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
#endif
