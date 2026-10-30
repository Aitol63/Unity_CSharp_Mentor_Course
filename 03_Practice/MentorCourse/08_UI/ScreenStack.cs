using System.Collections.Generic;
using UnityEngine;

namespace MentorCourse.UI
{
    /// <summary>Навигация по экранам: новый экран кладётся сверху, Pop возвращает к предыдущему.</summary>
    public class ScreenStack : MonoBehaviour
    {
        private readonly Stack<GameObject> _stack = new();

        public void Push(GameObject screen)
        {
            if (_stack.Count > 0) _stack.Peek().SetActive(false);
            screen.SetActive(true);
            _stack.Push(screen);
        }

        public void Pop()
        {
            if (_stack.Count == 0) return;

            _stack.Pop().SetActive(false);
            if (_stack.Count > 0) _stack.Peek().SetActive(true);
        }
    }
}
