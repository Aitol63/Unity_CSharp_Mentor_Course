using UnityEngine;

namespace MentorCourse.Movement
{
    /// <summary>Движение без физики: для декораций, простых прототипов и объектов без столкновений.</summary>
    public class TransformMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _turnSpeed = 720f;

        private IMoveInput _input;

        private void Awake() => _input = GetComponent<IMoveInput>();

        private void Update()
        {
            Vector3 direction = new Vector3(_input.Move.x, 0f, _input.Move.y);
            if (direction.sqrMagnitude < 0.0001f) return;

            transform.position += direction * (_speed * Time.deltaTime);

            Quaternion look = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, look, _turnSpeed * Time.deltaTime);
        }
    }
}
