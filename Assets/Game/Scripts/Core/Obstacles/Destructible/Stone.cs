using UnityEngine;
using Game.UI;
using Game.Managers;
using DG.Tweening;

namespace Game.Core.Obstacles
{
    public class Stone : DestructibleBase
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

        public override void BreakApart()
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
                Vector3 jumpPos;

                item.SetParent(null);
                
                if (item.GetComponent<Dollar>() != null)
                {
                    jumpPos = item.localPosition.Modify(y: 0f) + Vector3.back * 1f;
                }
                else
                {
                    jumpPos = item.localPosition.Modify(y: 0f) + Vector3.forward * 4f;
                }

                item.DOLocalJump(jumpPos, 0.8f, 1, 0.25f).SetEase(Ease.InOutSine);
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
