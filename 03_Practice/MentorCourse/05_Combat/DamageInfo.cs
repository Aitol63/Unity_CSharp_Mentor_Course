using UnityEngine;

namespace MentorCourse.Combat
{
    /// <summary>Описание удара. Маленькая readonly struct: не создаёт мусора.</summary>
    public readonly struct DamageInfo
    {
        public readonly int Amount;
        public readonly Vector3 Point;
        public readonly Vector3 Direction;
        public readonly GameObject Source;

        public DamageInfo(int amount, Vector3 point, Vector3 direction, GameObject source)
        {
            Amount = amount;
            Point = point;
            Direction = direction;
            Source = source;
        }
    }
}
