using DG.Tweening;
using Game.Core.Constants;
using Game.Managers;
using Game.UI;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class GiantHuman : DestructibleBase
    {
        [SerializeField] private float runSpeed;
        [SerializeField] private float runDistance;
        [SerializeField] private Animator animator;
        [SerializeField] private HealthIndicatorUI healthIndicator;
        [SerializeField] private Transform stickman;

        private bool isHit = false;
        private bool isRunning = false;
        private float distanceTraveled = 0f;

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
            float moveDelta = runSpeed * Time.deltaTime;
            transform.position += Vector3.forward * moveDelta;
            distanceTraveled += moveDelta;

            if (distanceTraveled >= runDistance)
            {
                StopRunning();
            }
        }

        private void StartRunning()
        {
            isRunning = true;

            Sequence seq = DOTween.Sequence();

            seq.Append(stickman.DORotate(Vector3.zero, 0.2f));
            seq.OnComplete(() => isRunning = true);

            animator.SetTrigger(AnimationHash.Run);
        }

        private void StopRunning()
        {
            isRunning = false;

            Sequence seq = DOTween.Sequence();

            seq.Append(stickman.DORotate(Vector3.up * 180f, 0.2f));

            animator.SetTrigger(AnimationHash.Idle);
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
