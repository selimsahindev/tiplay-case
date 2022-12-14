using UnityEngine;
using Game.Managers;

namespace Game.Core.Obstacles
{
    public class Fence : ObstacleBase
    {
        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponentInParent<RigManager>();

            if (rigManager != null)
            {
                col.enabled = false;
                rigManager.RemoveFellows(damage);
            }
        }
    }
}
