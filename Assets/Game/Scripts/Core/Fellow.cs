using UnityEngine;
using UnityEngine.Events;

namespace Game.Core
{
    public class Fellow : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        public SkinnedMeshRenderer SkinnedMeshRenderer => skinnedMeshRenderer;

        public Animator Animator { get; private set; }
        public Collider Collider { get; private set; }

        private void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            Collider = GetComponent<Collider>();
        }
    }
}
