using UnityEngine;

namespace MentorCourse.AI
{
    /// <summary>Сначала дешёвые проверки расстояния и угла, затем рейкаст.</summary>
    public class VisionSensor : MonoBehaviour
    {
        [SerializeField] private Transform _eye;
        [SerializeField] private float _distance = 12f;
        [SerializeField, Range(1f, 360f)] private float _angle = 110f;
        [SerializeField] private LayerMask _obstacleMask;

        public bool CanSee(Transform target)
        {
            Vector3 toTarget = target.position - _eye.position;

            if (toTarget.sqrMagnitude > _distance * _distance) return false;

            float minDot = Mathf.Cos(_angle * 0.5f * Mathf.Deg2Rad);
            if (Vector3.Dot(_eye.forward, toTarget.normalized) < minDot) return false;

            return !Physics.Linecast(_eye.position, target.position, _obstacleMask,
                                     QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            if (_eye == null) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_eye.position, _distance);

            Vector3 left = Quaternion.Euler(0f, -_angle * 0.5f, 0f) * _eye.forward;
            Vector3 right = Quaternion.Euler(0f, _angle * 0.5f, 0f) * _eye.forward;
            Gizmos.DrawRay(_eye.position, left * _distance);
            Gizmos.DrawRay(_eye.position, right * _distance);
        }
    }
}
