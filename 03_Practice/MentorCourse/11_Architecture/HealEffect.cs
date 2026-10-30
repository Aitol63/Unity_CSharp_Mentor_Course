using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.Architecture
{
    [CreateAssetMenu(menuName = "Mentor Course/Abilities/Heal Effect")]
    public class HealEffect : AbilityEffect
    {
        [SerializeField] private int _amount = 25;

        public override void Apply(GameObject caster, GameObject target)
        {
            if (target.TryGetComponent(out Health health)) health.Heal(_amount);
        }
    }
}
