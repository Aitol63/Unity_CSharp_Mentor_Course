using UnityEngine;

namespace MentorCourse.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private KeyCode _key = KeyCode.Escape;

        public bool IsPaused { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(_key)) SetPaused(!IsPaused);
        }

        public void SetPaused(bool paused)
        {
            IsPaused = paused;
            _panel.SetActive(paused);
            Time.timeScale = paused ? 0f : 1f;
        }

        // Статика переживает смену сцены: без этого следующая сцена запустится на паузе.
        private void OnDestroy() => Time.timeScale = 1f;
    }
}
