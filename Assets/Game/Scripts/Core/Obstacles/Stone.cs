using UnityEngine;
using Game.UI;
using Game.Managers;
using DG.Tweening;

namespace Game.Core.Obstacles
{
    public class Stone : Destructible
    {
        [SerializeField] private HealthIndicatorUI healthIndicator;
        [SerializeField] private Fellow theFellowOnTop;

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
            if (theFellowOnTop != null)
            {
                ThrowTheFellowDown();
            }

            base.BreakApart();
        }

        private void ThrowTheFellowDown()
        {
            Debug.Log("fellow is threw");
            theFellowOnTop.transform.SetParent(null);

            Vector3 jumpPos = transform.position.Modify(y: 0f) + Vector3.forward * 3f;

            theFellowOnTop.transform.DOJump(jumpPos, 1f, 1, 0.15f).SetEase(Ease.InOutSine);
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
