using UnityEngine;

namespace MentorCourse.CameraRig
{
    /// <summary>
    /// Тряска через "травму": сила растёт от ударов и плавно падает.
    /// Вешай на дочерний объект камеры, чтобы не спорить со скриптом следования.
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float _maxAngle = 4f;
        [SerializeField] private float _maxOffset = 0.3f;
        [SerializeField] private float _frequency = 25f;
        [SerializeField] private float _decay = 1.5f;

        private float _trauma;

        public void AddTrauma(float amount) => _trauma = Mathf.Clamp01(_trauma + amount);

        private void LateUpdate()
        {
            if (_trauma <= 0f) return;

            // Неотмасштабированное время: тряска работает и во время hit stop.
            float shake = _trauma * _trauma;
            float time = Time.unscaledTime * _frequency;

            transform.localPosition = new Vector3(Noise(time, 0f), Noise(time, 1f), 0f) * (_maxOffset * shake);
            transform.localRotation = Quaternion.Euler(0f, 0f, Noise(time, 2f) * _maxAngle * shake);

            _trauma = Mathf.Max(0f, _trauma - _decay * Time.unscaledDeltaTime);
            if (_trauma > 0f) return;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        private static float Noise(float x, float seed) => Mathf.PerlinNoise(x, seed) * 2f - 1f;
    }
}
