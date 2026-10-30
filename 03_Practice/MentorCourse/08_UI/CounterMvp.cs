using System;

namespace MentorCourse.UI
{
    // Полный пример Model-View-Presenter. Модель легко проверить тестами без сцены.

    public sealed class CounterModel
    {
        public int Value { get; private set; }
        public int Max { get; }

        public event Action<int> Changed;

        public CounterModel(int max) => Max = max;

        public void Increment()
        {
            if (Value >= Max) return;
            Value++;
            Changed?.Invoke(Value);
        }
    }

    public interface ICounterView
    {
        event Action Clicked;
        void Render(int value, bool canClick);
    }

    public sealed class CounterPresenter : IDisposable
    {
        private readonly CounterModel _model;
        private readonly ICounterView _view;

        public CounterPresenter(CounterModel model, ICounterView view)
        {
            _model = model;
            _view = view;
            _model.Changed += OnChanged;
            _view.Clicked += _model.Increment;
            OnChanged(_model.Value);
        }

        public void Dispose()
        {
            _model.Changed -= OnChanged;
            _view.Clicked -= _model.Increment;
        }

        private void OnChanged(int value) => _view.Render(value, value < _model.Max);
    }
}
