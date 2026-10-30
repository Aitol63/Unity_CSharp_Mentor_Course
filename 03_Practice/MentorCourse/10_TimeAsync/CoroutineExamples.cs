using System.Collections;
using UnityEngine;

namespace MentorCourse.TimeAsync
{
    /// <summary>Основные формы ожидания в корутинах.</summary>
    public class CoroutineExamples : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _blinks = 3;

        // Кэш: один объект ожидания вместо нового на каждый шаг.
        private readonly WaitForSeconds _blinkDelay = new(0.1f);
        private Coroutine _blink;

        public void Blink()
        {
            if (_blink != null) StopCoroutine(_blink);
            _blink = StartCoroutine(BlinkRoutine());
        }

        private IEnumerator BlinkRoutine()
        {
            for (int i = 0; i < _blinks; i++)
            {
                _renderer.enabled = false;
                yield return _blinkDelay;
                _renderer.enabled = true;
                yield return _blinkDelay;
            }

            _blink = null;
        }

        public IEnumerator MoveOverTime(Vector3 target, float duration)
        {
            Vector3 start = transform.position;

            for (float time = 0f; time < duration; time += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(start, target, time / duration);
                yield return null;
            }

            transform.position = target;
        }

        // Выключение объекта остановило бы мигание на скрытом рендерере.
        private void OnDisable()
        {
            if (_renderer != null) _renderer.enabled = true;
        }
    }
}
