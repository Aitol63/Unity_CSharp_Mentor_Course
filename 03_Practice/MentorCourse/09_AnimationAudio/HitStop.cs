using System.Collections;
using UnityEngine;

namespace MentorCourse.AnimationAudio
{
    /// <summary>Короткая заморозка времени при сильном ударе.</summary>
    public class HitStop : MonoBehaviour
    {
        private Coroutine _routine;

        public void Play(float duration = 0.06f)
        {
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(Freeze(duration));
        }

        private IEnumerator Freeze(float duration)
        {
            Time.timeScale = 0f;
            // Обычный WaitForSeconds при timeScale = 0 никогда не закончится.
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1f;
        }

        private void OnDisable() => Time.timeScale = 1f;
    }
}
