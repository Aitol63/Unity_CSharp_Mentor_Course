using UnityEngine;
using UnityEngine.Pool;

namespace MentorCourse.AnimationAudio
{
    /// <summary>
    /// Пул частиц. На префабе поставь Stop Action = Callback,
    /// тогда система сама вызовет OnParticleSystemStopped и вернётся в пул.
    /// </summary>
    public class VfxPlayer : MonoBehaviour
    {
        [SerializeField] private PooledParticle _prefab;

        private ObjectPool<PooledParticle> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<PooledParticle>(
                createFunc: () => Instantiate(_prefab, transform),
                actionOnGet: item => item.gameObject.SetActive(true),
                actionOnRelease: item => item.gameObject.SetActive(false));
        }

        public void Play(Vector3 position, Vector3 normal)
        {
            PooledParticle item = _pool.Get();
            item.transform.SetPositionAndRotation(position, Quaternion.LookRotation(normal));
            item.Play(_pool.Release);
        }
    }
}
