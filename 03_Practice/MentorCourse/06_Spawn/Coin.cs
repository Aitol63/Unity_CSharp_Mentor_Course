using System;
using UnityEngine;

namespace MentorCourse.Spawn
{
    /// <summary>Монета сообщает о сборе, а что с ней делать дальше, решает спавнер.</summary>
    [RequireComponent(typeof(Collider))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _value = 1;

        public int Value => _value;

        public event Action<Coin, GameObject> Collected;

        private void Reset() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) Collected?.Invoke(this, other.gameObject);
        }
    }
}
