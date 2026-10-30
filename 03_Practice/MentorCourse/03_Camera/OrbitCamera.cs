using UnityEngine;

namespace MentorCourse.CameraRig
{
    /// <summary>Камера от третьего лица с защитой от прохождения сквозь стены.</summary>
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _pivotOffset = new(0f, 1.6f, 0f);
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _sensitivity = 3f;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private LayerMask _obstacleMask = ~0;

        private float _yaw;
        private float _pitch = 15f;

        private void LateUpdate()
        {
            if (_target == null) return;

            _yaw += Input.GetAxis("Mouse X") * _sensitivity;
            _pitch = Mathf.Clamp(_pitch - Input.GetAxis("Mouse Y") * _sensitivity, -30f, 70f);

            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            Vector3 pivot = _target.position + _pivotOffset;
            Vector3 back = rotation * Vector3.back;
            float distance = _distance;

            if (Physics.SphereCast(pivot, _collisionRadius, back, out RaycastHit hit, _distance,
                                   _obstacleMask, QueryTriggerInteraction.Ignore))
            {
                distance = hit.distance;
            }

            transform.SetPositionAndRotation(pivot + back * distance, rotation);
        }
    }
}
