using System;
using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.Spawn
{
    /// <summary>Волна как данные: баланс настраивается без кода.</summary>
    [CreateAssetMenu(menuName = "Mentor Course/Wave")]
    public class WaveConfig : ScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public Health Prefab;
            [Min(1)] public int Count;
        }

        [field: SerializeField] public Entry[] Entries { get; private set; }
        [field: SerializeField, Min(0.05f)] public float SpawnInterval { get; private set; } = 0.5f;
        [field: SerializeField, Min(0f)] public float DelayAfterWave { get; private set; } = 3f;
    }
}
