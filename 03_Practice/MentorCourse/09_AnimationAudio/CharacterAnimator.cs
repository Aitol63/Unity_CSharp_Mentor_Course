using UnityEngine;

namespace MentorCourse.AnimationAudio
{
    /// <summary>
    /// Мост между логикой и Animator. Хэши быстрее строк и защищают от опечаток.
    /// В Animator создай параметры Speed (Float), Grounded (Bool), Attack (Trigger).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int GroundedHash = Animator.StringToHash("Grounded");
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        [SerializeField] private float _damping = 0.1f;

        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void SetSpeed(float speed01) =>
            _animator.SetFloat(SpeedHash, speed01, _damping, Time.deltaTime);

        public void SetGrounded(bool grounded) => _animator.SetBool(GroundedHash, grounded);

        public void PlayAttack() => _animator.SetTrigger(AttackHash);
    }
}
