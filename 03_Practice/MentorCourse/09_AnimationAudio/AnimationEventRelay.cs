using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.AnimationAudio
{
    /// <summary>
    /// Лежит рядом с Animator и принимает Animation Events.
    /// Событие в клипе вызывает метод по имени, поэтому имя нельзя менять без правки клипа.
    /// </summary>
    public class AnimationEventRelay : MonoBehaviour
    {
        [SerializeField] private MeleeAttack _melee;
        [SerializeField] private AudioSource _footsteps;

        public void AnimEvent_Hit() => _melee.PerformHit();
        public void AnimEvent_Footstep() => _footsteps.Play();
    }
}
