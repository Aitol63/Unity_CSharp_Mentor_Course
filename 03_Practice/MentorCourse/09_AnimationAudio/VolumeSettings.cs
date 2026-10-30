using UnityEngine;
using UnityEngine.Audio;

namespace MentorCourse.AnimationAudio
{
    /// <summary>
    /// Слайдер линейный, а миксер работает в децибелах. Параметр громкости
    /// нужно открыть в миксере через Expose.
    /// </summary>
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _parameter = "SfxVolume";

        public void SetVolume(float value01)
        {
            float decibels = Mathf.Log10(Mathf.Max(value01, 0.0001f)) * 20f;
            _mixer.SetFloat(_parameter, decibels);
        }
    }
}
