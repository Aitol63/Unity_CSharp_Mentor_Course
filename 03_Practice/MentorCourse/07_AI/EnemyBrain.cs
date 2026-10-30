using MentorCourse.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace MentorCourse.AI
{
    /// <summary>
    /// Собирает состояния и хранит общие данные.
    /// Логика каждого состояния лежит в своём классе.
    /// Сцене нужен запечённый NavMesh (пакет AI Navigation, компонент NavMeshSurface).
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(VisionSensor), typeof(Health))]
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private MeleeAttack _melee;
        [SerializeField] private float _attackDistance = 2f;
        [SerializeField] private float _thinkInterval = 0.2f;

        private Health _health;
        private float _nextThink;

        public NavMeshAgent Agent { get; private set; }
        public VisionSensor Vision { get; private set; }
        public MeleeAttack Melee => _melee;
        public Transform Target => _target;
        public Transform[] PatrolPoints => _patrolPoints;
        public float AttackDistance => _attackDistance;
        public Vector3 LastKnownPosition { get; set; }
        public StateMachine Machine { get; } = new();

        public IState Patrol { get; private set; }
        public IState Chase { get; private set; }
        public IState Attack { get; private set; }
        public IState Search { get; private set; }

        public float DistanceToTarget => Vector3.Distance(transform.position, _target.position);
        public bool CanSeeTarget => _target != null && Vision.CanSee(_target);

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Vision = GetComponent<VisionSensor>();
            _health = GetComponent<Health>();

            Patrol = new PatrolState(this);
            Chase = new ChaseState(this);
            Attack = new AttackState(this);
            Search = new SearchState(this);
        }

        private void OnEnable()
        {
            _health.Died += OnDied;
            // Случайный сдвиг: враги из одной волны не думают в одном кадре.
            _nextThink = Time.time + Random.Range(0f, _thinkInterval);
            Machine.Change(Patrol);
        }

        private void OnDisable() => _health.Died -= OnDied;

        private void Update()
        {
            if (Time.time < _nextThink) return;

            _nextThink = Time.time + _thinkInterval;
            Machine.Tick();
        }

        private void OnDied()
        {
            Machine.Change(null);
            Agent.isStopped = true;
            enabled = false;
        }
    }
}
