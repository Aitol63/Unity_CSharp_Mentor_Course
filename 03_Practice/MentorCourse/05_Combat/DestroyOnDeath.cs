using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>Отдельная маленькая реакция на смерть. Для пула пиши такой же компонент с Release.</summary>
    [RequireComponent(typeof(Health))]
    public class DestroyOnDeath : MonoBehaviour
    {
        [SerializeField] private float _delay;

        private Health _health;

        private void Awake() => _health = GetComponent<Health>();
        private void OnEnable() => _health.Died += OnDied;
        private void OnDisable() => _health.Died -= OnDied;

        private void OnDied() => Destroy(gameObject, _delay);
    }
}
