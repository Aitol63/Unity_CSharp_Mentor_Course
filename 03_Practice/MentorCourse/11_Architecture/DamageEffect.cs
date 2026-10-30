using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.Architecture
{
    // Unity требует, чтобы класс ScriptableObject лежал в файле с тем же именем.
    [CreateAssetMenu(menuName = "Mentor Course/Abilities/Damage Effect")]
    public class DamageEffect : AbilityEffect
    {
        [SerializeField] private int _amount = 25;

        public override void Apply(GameObject caster, GameObject target)
        {
            if (!target.TryGetComponent(out IDamageable damageable)) return;

            Vector3 direction = (target.transform.position - caster.transform.position).normalized;
            damageable.TakeDamage(new DamageInfo(_amount, target.transform.position, direction, caster));
        }
    }
}
