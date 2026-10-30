using UnityEngine;

namespace MentorCourse.AI
{
    public sealed class PatrolState : IState
    {
        private readonly EnemyBrain _brain;
        private int _index;

        public PatrolState(EnemyBrain brain) => _brain = brain;

        public void Enter()
        {
            _brain.Agent.isStopped = false;
            MoveToCurrentPoint();
        }

        public void Tick()
        {
            if (_brain.CanSeeTarget)
            {
                _brain.Machine.Change(_brain.Chase);
                return;
            }

            if (_brain.Agent.pathPending || _brain.Agent.remainingDistance > _brain.Agent.stoppingDistance)
                return;

            if (_brain.PatrolPoints.Length == 0) return;

            _index = (_index + 1) % _brain.PatrolPoints.Length;
            MoveToCurrentPoint();
        }

        public void Exit() { }

        private void MoveToCurrentPoint()
        {
            if (_brain.PatrolPoints.Length == 0) return;
            _brain.Agent.SetDestination(_brain.PatrolPoints[_index].position);
        }
    }

    public sealed class ChaseState : IState
    {
        private readonly EnemyBrain _brain;

        public ChaseState(EnemyBrain brain) => _brain = brain;

        public void Enter() => _brain.Agent.isStopped = false;

        public void Tick()
        {
            if (!_brain.CanSeeTarget)
            {
                _brain.Machine.Change(_brain.Search);
                return;
            }

            _brain.LastKnownPosition = _brain.Target.position;
            _brain.Agent.SetDestination(_brain.Target.position);

            if (_brain.DistanceToTarget <= _brain.AttackDistance)
                _brain.Machine.Change(_brain.Attack);
        }

        public void Exit() { }
    }

    public sealed class AttackState : IState
    {
        private readonly EnemyBrain _brain;

        public AttackState(EnemyBrain brain) => _brain = brain;

        public void Enter() => _brain.Agent.isStopped = true;

        public void Tick()
        {
            if (_brain.DistanceToTarget > _brain.AttackDistance)
            {
                _brain.Machine.Change(_brain.Chase);
                return;
            }

            Vector3 look = Vector3.ProjectOnPlane(
                _brain.Target.position - _brain.transform.position, Vector3.up);
            if (look.sqrMagnitude > 0.001f)
                _brain.transform.rotation = Quaternion.LookRotation(look);

            // Удар без анимации. С Animator вызывай PerformHit из Animation Event.
            if (_brain.Melee != null && _brain.Melee.TryStart())
                _brain.Melee.PerformHit();
        }

        public void Exit() => _brain.Agent.isStopped = false;
    }

    /// <summary>Память: враг идёт к последней известной позиции и какое-то время ищет.</summary>
    public sealed class SearchState : IState
    {
        private const float SearchTime = 4f;

        private readonly EnemyBrain _brain;
        private float _giveUpAt;

        public SearchState(EnemyBrain brain) => _brain = brain;

        public void Enter()
        {
            _giveUpAt = Time.time + SearchTime;
            _brain.Agent.isStopped = false;
            _brain.Agent.SetDestination(_brain.LastKnownPosition);
        }

        public void Tick()
        {
            if (_brain.CanSeeTarget) _brain.Machine.Change(_brain.Chase);
            else if (Time.time >= _giveUpAt) _brain.Machine.Change(_brain.Patrol);
        }

        public void Exit() { }
    }
}
