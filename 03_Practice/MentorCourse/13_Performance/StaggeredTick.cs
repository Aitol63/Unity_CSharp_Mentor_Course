using UnityEngine;

namespace MentorCourse.Performance
{
    /// <summary>Распределяет работу по кадрам: за кадр обрабатывается только порция объектов.</summary>
    public class StaggeredTick : MonoBehaviour
    {
        [SerializeField] private Transform[] _objects;
        [SerializeField] private Transform _viewer;
        [SerializeField] private float _activeRadius = 30f;
        [SerializeField] private int _perFrame = 20;

        private int _cursor;

        private void Update()
        {
            if (_objects.Length == 0) return;

            float sqrRadius = _activeRadius * _activeRadius;
            int count = Mathf.Min(_perFrame, _objects.Length);

            for (int i = 0; i < count; i++)
            {
                Transform item = _objects[_cursor];
                _cursor = (_cursor + 1) % _objects.Length;

                bool near = (item.position - _viewer.position).sqrMagnitude <= sqrRadius;
                if (item.gameObject.activeSelf != near) item.gameObject.SetActive(near);
            }
        }
    }
}
