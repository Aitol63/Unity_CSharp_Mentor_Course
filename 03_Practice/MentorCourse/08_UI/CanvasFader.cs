using System.Collections;
using UnityEngine;

namespace MentorCourse.UI
{
    /// <summary>Плавное появление панели. Использует unscaled-время, поэтому работает на паузе.</summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasFader : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.2f;

        private CanvasGroup _group;
        private Coroutine _fade;

        private void Awake() => _group = GetComponent<CanvasGroup>();

        public void Show() => FadeTo(1f);
        public void Hide() => FadeTo(0f);

        private void FadeTo(float alpha)
        {
            if (_fade != null) StopCoroutine(_fade);
            _fade = StartCoroutine(Fade(alpha));
        }

        private IEnumerator Fade(float target)
        {
            _group.blocksRaycasts = target > 0f;
            _group.interactable = target > 0f;

            while (!Mathf.Approximately(_group.alpha, target))
            {
                _group.alpha = Mathf.MoveTowards(_group.alpha, target, Time.unscaledDeltaTime / _duration);
                yield return null;
            }
        }
    }
}
