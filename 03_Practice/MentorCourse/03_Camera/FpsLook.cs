using UnityEngine;

namespace MentorCourse.CameraRig
{
    /// <summary>
    /// Вид от первого лица. Pitch и yaw хранятся в полях: читать eulerAngles ненадёжно.
    /// Дельту мыши не умножаем на deltaTime: она уже зависит от длительности кадра.
    /// </summary>
    public class FpsLook : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private float _sensitivity = 2f;
        [SerializeField] private float _minPitch = -85f;
        [SerializeField] private float _maxPitch = 85f;

        private float _pitch;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            float yaw = Input.GetAxis("Mouse X") * _sensitivity;
            float pitchDelta = Input.GetAxis("Mouse Y") * _sensitivity;

            _body.Rotate(Vector3.up, yaw);
            _pitch = Mathf.Clamp(_pitch - pitchDelta, _minPitch, _maxPitch);
            transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }
    }
}
