using UnityEngine;

namespace MentorCourse.Architecture
{
    /// <summary>Эффект как ассет: дизайнер собирает способности без программиста.</summary>
    public abstract class AbilityEffect : ScriptableObject
    {
        public abstract void Apply(GameObject caster, GameObject target);
    }
}
