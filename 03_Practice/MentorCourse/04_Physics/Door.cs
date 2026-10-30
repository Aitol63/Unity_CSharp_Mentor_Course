using System.Collections;
using UnityEngine;

namespace MentorCourse.PhysicsPractice
{
    /// <summary>Дверь на корутине. Старая анимация останавливается, чтобы две копии не спорили.</summary>
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _openAngle = 90f;
        [SerializeField] private float _duration = 0.4f;

        private Coroutine _rotation;
        private bool _isOpen;

        public string Prompt => _isOpen ? "Закрыть" : "Открыть";

        public void Interact(GameObject actor)
        {
            _isOpen = !_isOpen;
            if (_rotation != null) StopCoroutine(_rotation);
            _rotation = StartCoroutine(Rotate(_isOpen ? _openAngle : 0f));
        }

        private IEnumerator Rotate(float angle)
        {
            Quaternion from = _pivot.localRotation;
            Quaternion to = Quaternion.Euler(0f, angle, 0f);

            for (float t = 0f; t < 1f; t += Time.deltaTime / _duration)
            {
                _pivot.localRotation = Quaternion.Slerp(from, to, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }

            _pivot.localRotation = to;
        }
    }
}
