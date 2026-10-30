using MentorCourse.Basics;
using UnityEngine;

namespace MentorCourse.Architecture
{
    /// <summary>Состояние кулдауна живёт на персонаже, а не в ассете: ассет общий для всех.</summary>
    public class AbilityCaster : MonoBehaviour
    {
        [SerializeField] private Ability _ability;

        private Cooldown _cooldown;

        public bool TryCast(GameObject target)
        {
            if (!_cooldown.IsReady) return false;

            _cooldown.Start(_ability.Cooldown);
            _ability.Cast(gameObject, target);
            return true;
        }
    }
}
