using UnityEngine;

namespace MentorCourse.Basics
{
    /// <summary>
    /// Кулдаун без Update и корутин: просто помним момент готовности.
    /// Это struct: поле не делай readonly, иначе Start изменит копию.
    /// </summary>
    public struct Cooldown
    {
        private float _readyAt;

        public bool IsReady => Time.time >= _readyAt;
        public float Remaining => Mathf.Max(0f, _readyAt - Time.time);

        public void Start(float seconds) => _readyAt = Time.time + seconds;
    }
}
