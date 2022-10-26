using Game.Core.Events;
using Game.Core.Obstacles;
using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Core.RigBase
{
    public class ShootHandler : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private float timeBetweenShots;
        [Space, SerializeField] private Transform tipOfTheGun;
        [SerializeField] private LayerMask layerMask;

        [HideInInspector] public bool isActive = false;

        private bool isLoaded = true;
        private RigBase rig;
        private Transform target;
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = ServiceProvider.GetManager<GameManager>();
            rig = GetComponent<RigBase>();
        }

        private void Update()
        {
            if (isActive && gameManager.IsGameActive)
            {
                RaycastUpdate();
                //HandleRotation();
            }
        }

        private int GetPower()
        {
            // TODO: Calculate this dynamically later.
            return Mathf.Clamp(rig.fellows.Count, 1, 3);
        }

        private void Fire(DestructibleBase destructable)
        {
            destructable.GetDamage(GetPower());
            rig.PlayShootingAnimation();

            // Notify listeners (Camera will shake upon this.)
            EventBase.NotifyListeners(Enums.EventType.ShotsFired);
        }

        private void RaycastUpdate()
        {
            Vector3 rayOrigin = transform.position.Modify(y: tipOfTheGun.position.y);
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.forward * range, Color.green);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, range, layerMask))
            {
                DestructibleBase destructable = hit.collider.GetComponent<DestructibleBase>();

                if (destructable != null)
                {
                    target = destructable.transform;

                    if (isLoaded)
                    {
                        Fire(destructable);
                        StartCoroutine(Cooldown());
                    }
                }
                else
                {
                    target = null;
                }
            }
        }

        private void HandleRotation()
        {
            if (target == null)
            {
                transform.eulerAngles = Vector3.zero;
                return;
            }

            Vector3 lookPos = target.position.Modify(y: transform.position.y);
            float distance = Vector3.Distance(transform.position, lookPos);

            if (distance > 2f)
            {
                transform.LookAt(lookPos);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
        }

        private IEnumerator Cooldown()
        {
            isLoaded = false;
            yield return new WaitForSeconds(timeBetweenShots);
            isLoaded = true;
        }
    }
}
