using MentorCourse.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MentorCourse.UI
{
    /// <summary>Вид только показывает значения и не содержит правил игры.</summary>
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Health _source;
        [SerializeField] private Image _fill;
        [SerializeField] private TMP_Text _label;

        private void OnEnable()
        {
            _source.Changed += Render;
            Render(_source.Current, _source.Max);
        }

        private void OnDisable() => _source.Changed -= Render;

        private void Render(int current, int max)
        {
            _fill.fillAmount = (float)current / max;
            if (_label != null) _label.SetText("{0}/{1}", (float)current, max);
        }
    }
}
