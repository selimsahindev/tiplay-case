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
        [SerializeField] private RigBase rig;
        [SerializeField] private LayerMask layerMask;

        private bool isLoaded = true;
        private Transform target;
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = ServiceProvider.GetManager<GameManager>();
        }

        private void Update()
        {
            if (gameManager.IsGameActive)
            {
                RaycastUpdate();
                HandleRotation();
            }
        }

        private int GetPower()
        {
            return rig.fellows.Count;
        }

        private void Fire(Destructible destructable)
        {
            destructable.GetDamage(GetPower());
        }

        private void RaycastUpdate()
        {
            Vector3 rayOrigin = transform.position.Modify(y: tipOfTheGun.position.y);
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.forward * range, Color.green);

            if (Physics.Raycast(rayOrigin, Vector3.forward, out RaycastHit hit, range, layerMask))
            {
                Destructible destructable = hit.collider.GetComponent<Destructible>();

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
