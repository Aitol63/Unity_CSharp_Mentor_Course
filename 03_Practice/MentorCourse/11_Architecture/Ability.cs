using UnityEngine;

namespace MentorCourse.Architecture
{
    [CreateAssetMenu(menuName = "Mentor Course/Abilities/Ability")]
    public class Ability : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, Min(0f)] public float Cooldown { get; private set; } = 1f;
        [SerializeField] private AbilityEffect[] _effects;

        public void Cast(GameObject caster, GameObject target)
        {
            foreach (AbilityEffect effect in _effects)
                effect.Apply(caster, target);
        }
    }
}
