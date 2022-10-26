using DG.Tweening;
using Game.Core.Constants;
using Game.Managers;
using Game.UI;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class GiantHuman : Destructible
    {
        [SerializeField] private float runSpeed;
        [SerializeField] private Animator animator;
        [SerializeField] private HealthIndicatorUI healthIndicator;

        private bool isHit = false;
        private bool isRunning = false;

        protected override void Awake()
        {
            base.Awake();

            healthIndicator.SetHealth(health);
        }

        private void Update()
        {
            if (isRunning)
            {
                HandleMovement();
            }
        }

        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
            healthIndicator.SetHealth(health);

            if (!isHit)
            {
                isHit = true;
                StartRunning();
            }
        }

        private void HandleMovement()
        {
            transform.position += Vector3.forward * runSpeed * Time.deltaTime;
        }

        private void StartRunning()
        {
            isRunning = true;

            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DORotate(Vector3.zero, 0.2f));
            seq.OnComplete(() => isRunning = true);

            animator.SetTrigger(AnimationHash.Run);
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponent<RigManager>();

            if (rigManager != null)
            {
                col.enabled = false;
                rigManager.RemoveFellows(damage);
            }
        }

        private void OnValidate()
        {
            healthIndicator?.SetHealth(health);
        }
    }
}
