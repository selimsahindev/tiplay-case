using Game.Managers;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class Barrel : DestructibleBase
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layerMask;

        private ParticleLibrary particleLibrary;

        protected override void Awake()
        {
            base.Awake();

            particleLibrary = ServiceProvider.GetManager<ParticleLibrary>();
        }

        public override void BreakApart()
        {
            base.BreakApart();

            Explode();
        }

        private void Explode()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, layerMask);

            for (int i = 0; i < cols.Length; i++)
            {
                DestructibleBase destructible = cols[i].GetComponent<DestructibleBase>();

                if (destructible != null)
                {
                    Debug.Log(destructible.name);
                    destructible.BreakApart();
                }
            }

            PlayExplosionParticle();
        }

        private void PlayExplosionParticle()
        {
            ParticleSystem particle = particleLibrary.GetParticlePool(Enums.ParticleNames.Explosion).Pop();
            particle.gameObject.SetActive(true);
            particle.transform.position = transform.position.Modify(y: 1f);
            particle.Play();
            DelayHandler.WaitAndInvoke(() => {
                particleLibrary.GetParticlePool(Enums.ParticleNames.Explosion).Push(particle);
            }, particle.main.duration);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
