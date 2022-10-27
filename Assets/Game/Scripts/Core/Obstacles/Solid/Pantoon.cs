using UnityEngine;
using Game.Managers;

namespace Game.Core.Obstacles
{
    public class Pantoon : ObstacleBase
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
