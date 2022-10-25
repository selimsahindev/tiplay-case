using UnityEngine;
using Game.UI;
using Game.Managers;

namespace Game.Core.Obstacles
{
    public class StoneWithFellow : Destructible
    {
        [SerializeField] private HealthIndicatorUI healthIndicator;
        [SerializeField] private Fellow fellow;

        protected override void Awake()
        {
            base.Awake();

            healthIndicator.SetHealth(health);
        }

        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
            healthIndicator.SetHealth(health);
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponent<RigManager>();

            if (rigManager != null)
            {
                Disappear();
            }
        }
    }
}
