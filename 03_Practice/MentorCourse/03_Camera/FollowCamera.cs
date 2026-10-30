using UnityEngine;

namespace MentorCourse.CameraRig
{
    /// <summary>Камера следует в LateUpdate, когда цель уже подвинулась.</summary>
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new(0f, 10f, -8f);
        [SerializeField] private float _smoothTime = 0.15f;

        private Vector3 _velocity;

        private void LateUpdate()
        {
            if (_target == null) return;

            transform.position = Vector3.SmoothDamp(
                transform.position, _target.position + _offset, ref _velocity, _smoothTime);
        }
    }
}
