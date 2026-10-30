using UnityEngine;

namespace MentorCourse.UI
{
    /// <summary>Точка сборки: создаёт модель и связывает её с видом.</summary>
    public class CounterInstaller : MonoBehaviour
    {
        [SerializeField] private CounterView _view;
        [SerializeField] private int _max = 10;

        private CounterPresenter _presenter;

        private void Start() => _presenter = new CounterPresenter(new CounterModel(_max), _view);
        private void OnDestroy() => _presenter?.Dispose();
    }
}
