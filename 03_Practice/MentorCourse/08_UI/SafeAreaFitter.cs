using UnityEngine;

namespace MentorCourse.UI
{
    /// <summary>Подгоняет панель под безопасную зону экрана с вырезом.</summary>
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        private RectTransform _rect;
        private Rect _applied;

        private void Awake() => _rect = GetComponent<RectTransform>();

        private void Update()
        {
            Rect safe = Screen.safeArea;
            if (safe == _applied || Screen.width == 0 || Screen.height == 0) return;

            _applied = safe;
            var screen = new Vector2(Screen.width, Screen.height);
            _rect.anchorMin = safe.min / screen;
            _rect.anchorMax = safe.max / screen;
        }
    }
}
