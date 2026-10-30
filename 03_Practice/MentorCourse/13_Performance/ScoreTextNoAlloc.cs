using TMPro;
using UnityEngine;

namespace MentorCourse.Performance
{
    /// <summary>Текст обновляется только при изменении, а SetText не создаёт новую строку.</summary>
    public class ScoreTextNoAlloc : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private int _shown = int.MinValue;

        public void Show(int score)
        {
            if (score == _shown) return;

            _shown = score;
            _label.SetText("Score: {0}", score);
        }
    }
}
