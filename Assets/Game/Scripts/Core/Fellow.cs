using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class Fellow : MonoBehaviour
    {
        public Animator Animator
        {
            get;
            private set;
        }

        public readonly UnityEvent<Collider> onTriggerEnter = new UnityEvent<Collider>();

        private void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter.Invoke(other);
        }
    }
}
