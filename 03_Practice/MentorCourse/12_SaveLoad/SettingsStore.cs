using UnityEngine;

namespace MentorCourse.SaveLoad
{
    /// <summary>PlayerPrefs подходит для настроек, но не для прогресса игрока.</summary>
    public static class SettingsStore
    {
        private const string MusicKey = "settings.music";
        private const string SfxKey = "settings.sfx";

        public static float Music
        {
            get => PlayerPrefs.GetFloat(MusicKey, 0.8f);
            set => PlayerPrefs.SetFloat(MusicKey, Mathf.Clamp01(value));
        }

        public static float Sfx
        {
            get => PlayerPrefs.GetFloat(SfxKey, 1f);
            set => PlayerPrefs.SetFloat(SfxKey, Mathf.Clamp01(value));
        }

        public static void Flush() => PlayerPrefs.Save();
    }
}
