using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MentorCourse.UI
{
    public class CounterView : MonoBehaviour, ICounterView
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        public void Render(int value, bool canClick)
        {
            _label.SetText("{0}", value);
            _button.interactable = canClick;
        }

        private void OnClick() => Clicked?.Invoke();
    }
}
