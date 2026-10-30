using UnityEngine;

namespace MentorCourse.UI
{
    /// <summary>Держит UI-элемент над объектом в мире и скрывает его за камерой.</summary>
    public class WorldBar : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new(0f, 2f, 0f);
        [SerializeField] private RectTransform _bar;

        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void LateUpdate()
        {
            Vector3 screen = _camera.WorldToScreenPoint(_target.position + _offset);
            bool visible = screen.z > 0f;

            if (_bar.gameObject.activeSelf != visible) _bar.gameObject.SetActive(visible);
            if (visible) _bar.position = screen;
        }
    }
}
