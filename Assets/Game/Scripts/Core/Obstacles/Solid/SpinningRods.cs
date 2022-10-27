using UnityEngine;
using Game.Managers;
using DG.Tweening;

namespace Game.Core.Obstacles
{
    public class SpinningRods : ObstacleBase
    {
        [SerializeField] private Trigger trigger;
        private Collider[] cols;

        protected override void Awake()
        {
            base.Awake();

            cols = trigger.GetComponents<Collider>();
            
            trigger.onTriggerEnter.AddListener(OnTriggerEnter);
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponentInParent<RigManager>();

            if (rigManager != null)
            {
                // This one is the main collider.
                col.enabled = false;

                // These are colliders on the spinning part.
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i].enabled = false;
                }

                rigManager.RemoveFellows(damage);
            }
        }
    }
}
