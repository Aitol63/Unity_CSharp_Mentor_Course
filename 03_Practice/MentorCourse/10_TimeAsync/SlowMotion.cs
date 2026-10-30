using UnityEngine;

namespace MentorCourse.TimeAsync
{
    /// <summary>Замедление времени. Физику надо замедлять вместе с ним, иначе она будет дёргаться.</summary>
    public class SlowMotion : MonoBehaviour
    {
        private float _defaultFixedDelta;

        private void Awake() => _defaultFixedDelta = Time.fixedDeltaTime;

        public void SetScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = _defaultFixedDelta * Mathf.Max(scale, 0.01f);
        }

        private void OnDestroy() => SetScale(1f);
    }
}
