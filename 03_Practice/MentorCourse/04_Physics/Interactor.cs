using System;
using UnityEngine;

namespace MentorCourse.PhysicsPractice
{
    /// <summary>Рейкаст из камеры ищет объект для взаимодействия и сообщает подсказку UI.</summary>
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _range = 3f;
        [SerializeField] private LayerMask _mask = ~0;
        [SerializeField] private KeyCode _key = KeyCode.E;

        private IInteractable _current;

        public event Action<string> PromptChanged;

        private void Update()
        {
            IInteractable found = FindTarget();

            if (!ReferenceEquals(found, _current))
            {
                _current = found;
                PromptChanged?.Invoke(found?.Prompt);
            }

            if (_current != null && Input.GetKeyDown(_key))
                _current.Interact(gameObject);
        }

        private IInteractable FindTarget()
        {
            bool hasHit = Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hit, _range,
                                          _mask, QueryTriggerInteraction.Collide);

            return hasHit && hit.collider.TryGetComponent(out IInteractable target) ? target : null;
        }
    }
}
