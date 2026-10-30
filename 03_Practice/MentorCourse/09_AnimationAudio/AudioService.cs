using UnityEngine;

namespace MentorCourse.AnimationAudio
{
    /// <summary>
    /// Пул источников для 3D-звуков. Pitch слегка меняется,
    /// чтобы повторяющийся выстрел не звучал механически.
    /// </summary>
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private int _voices = 16;
        [SerializeField] private float _pitchVariation = 0.08f;

        private AudioSource[] _sources;
        private int _next;

        private void Awake()
        {
            _sources = new AudioSource[_voices];

            for (int i = 0; i < _voices; i++)
            {
                var child = new GameObject($"Voice {i}");
                child.transform.SetParent(transform);

                AudioSource source = child.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.spatialBlend = 1f;
                _sources[i] = source;
            }
        }

        public void PlayAt(AudioClip clip, Vector3 position, float volume = 1f)
        {
            // Если голосов мало, самый старый звук обрывается.
            AudioSource source = _sources[_next];
            _next = (_next + 1) % _sources.Length;

            source.transform.position = position;
            source.clip = clip;
            source.volume = volume;
            source.pitch = 1f + Random.Range(-_pitchVariation, _pitchVariation);
            source.Play();
        }
    }
}
