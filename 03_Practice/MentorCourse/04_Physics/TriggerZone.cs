using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MentorCourse.PhysicsPractice
{
    /// <summary>
    /// Зона, которая помнит, кто внутри. Уничтоженный объект не вызовет OnTriggerExit,
    /// поэтому мёртвые записи вычищаются в CountInside.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private string _requiredTag = "Player";
        [SerializeField] private UnityEvent _entered;
        [SerializeField] private UnityEvent _exited;

        private readonly HashSet<Collider> _inside = new();

        private void Reset() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_requiredTag)) return;
            if (_inside.Add(other)) _entered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (_inside.Remove(other)) _exited.Invoke();
        }

        public int CountInside()
        {
            _inside.RemoveWhere(item => item == null);
            return _inside.Count;
        }
    }
}
