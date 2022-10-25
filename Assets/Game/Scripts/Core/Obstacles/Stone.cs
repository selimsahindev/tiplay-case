using UnityEngine;
using Game.UI;
using Game.Managers;
using DG.Tweening;

namespace Game.Core.Obstacles
{
    public class Stone : Destructible
    {
        [SerializeField] private HealthIndicatorUI healthIndicator;
        [SerializeField] private Transform[] itemsOnTop;

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

        protected override void BreakApart()
        {
            if (itemsOnTop != null)
            {
                ThrowTheItemsDown();
            }

            base.BreakApart();
        }

        private void ThrowTheItemsDown()
        {
            foreach (Transform item in itemsOnTop)
            {
                item.SetParent(null);

                Vector3 jumpPos = transform.position.Modify(y: 0f) + Vector3.forward * 4f;

                item.DOJump(jumpPos, 0.8f, 1, 0.25f).SetEase(Ease.InOutSine);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponent<RigManager>();

            if (rigManager != null)
            {
                rigManager.RemoveFellows(damage);
                Disappear();
            }
        }

        private void OnValidate()
        {
            healthIndicator?.SetHealth(health);
        }
    }
}
