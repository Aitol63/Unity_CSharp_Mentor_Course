using System;
using UnityEngine;

namespace MentorCourse.AnimationAudio
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PooledParticle : MonoBehaviour
    {
        private ParticleSystem _particles;
        private Action<PooledParticle> _release;

        private void Awake() => _particles = GetComponent<ParticleSystem>();

        public void Play(Action<PooledParticle> release)
        {
            _release = release;
            _particles.Play();
        }

        private void OnParticleSystemStopped() => _release?.Invoke(this);
    }
}
