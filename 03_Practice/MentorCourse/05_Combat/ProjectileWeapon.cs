using MentorCourse.Basics;
using UnityEngine;
using UnityEngine.Pool;

namespace MentorCourse.Combat
{
    /// <summary>Оружие со снарядами из стандартного ObjectPool.</summary>
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _speed = 40f;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _fireInterval = 0.15f;

        private ObjectPool<Projectile> _pool;
        private Cooldown _cooldown;

        private void Awake()
        {
            _pool = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: item => item.gameObject.SetActive(true),
                actionOnRelease: item => item.gameObject.SetActive(false),
                actionOnDestroy: item => Destroy(item.gameObject),
                defaultCapacity: 32);
        }

        public bool TryFire()
        {
            if (!_cooldown.IsReady) return false;
            _cooldown.Start(_fireInterval);

            Projectile projectile = _pool.Get();
            projectile.transform.position = _muzzle.position;
            projectile.Launch(_muzzle.forward, _speed, _damage, gameObject, _pool.Release);
            return true;
        }
    }
}
