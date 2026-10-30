using System;
using System.Collections;
using MentorCourse.Combat;
using UnityEngine;

namespace MentorCourse.Spawn
{
    /// <summary>Волны на корутинах. Следующая волна начинается, когда враги текущей погибли.</summary>
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private WaveConfig[] _waves;
        [SerializeField] private Transform[] _spawnPoints;

        private int _alive;

        public event Action<int> WaveStarted;
        public event Action AllWavesCompleted;

        private IEnumerator Start()
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                WaveStarted?.Invoke(i + 1);
                yield return RunWave(_waves[i]);
                yield return new WaitUntil(() => _alive == 0);
                yield return new WaitForSeconds(_waves[i].DelayAfterWave);
            }

            AllWavesCompleted?.Invoke();
        }

        private IEnumerator RunWave(WaveConfig wave)
        {
            var delay = new WaitForSeconds(wave.SpawnInterval);

            foreach (WaveConfig.Entry entry in wave.Entries)
            {
                for (int i = 0; i < entry.Count; i++)
                {
                    SpawnEnemy(entry.Prefab);
                    yield return delay;
                }
            }
        }

        private void SpawnEnemy(Health prefab)
        {
            Transform point = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
            Health enemy = Instantiate(prefab, point.position, point.rotation);

            _alive++;
            enemy.Died += () => _alive--;
        }
    }
}
