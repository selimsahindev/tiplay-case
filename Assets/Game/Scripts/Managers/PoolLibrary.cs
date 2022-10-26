using Game.Core;
using Game.Core.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PoolLibrary : MonoBehaviour, IProvidable
    {
        [SerializeField] private Fellow fellowPrefab;
        private ParticleSystem moneySplashParticle;
        private ParticleSystem explosionParticle;

        private Dictionary<ParticleNames, ObjectPool<ParticleSystem>> poolsDictionary
            = new Dictionary<ParticleNames, ObjectPool<ParticleSystem>>();

        private ObjectPool<Fellow> fellowPool;

        private void Awake()
        {
            ServiceProvider.Register(this);

            LoadResources();
            PopulatePools();
        }

        private void LoadResources()
        {
            moneySplashParticle = Resources.Load<ParticleSystem>("Particles/MoneySplashParticle");
            explosionParticle = Resources.Load<ParticleSystem>("Particles/ExplosionParticle");
        }

        private void PopulatePools()
        {
            Transform parent;
            
            parent = new GameObject("MoneySplashParticles").transform;
            parent.SetParent(this.transform);
            poolsDictionary.Add(ParticleNames.MoneySplash, new ObjectPool<ParticleSystem>(
                12,
                () => {
                    return Instantiate(moneySplashParticle, Vector3.zero, Quaternion.identity, parent);
                },
                item => {
                    item.transform.SetParent(parent);
                    item.gameObject.SetActive(false);
                }
            ));

            parent = new GameObject("ExplosionParticles").transform;
            parent.SetParent(this.transform);
            poolsDictionary.Add(ParticleNames.Explosion, new ObjectPool<ParticleSystem>(
                12,
                () => {
                    return Instantiate(explosionParticle, Vector3.zero, Quaternion.identity, parent);
                },
                item => {
                    item.transform.SetParent(parent);
                    item.gameObject.SetActive(false);
                }
            ));

            parent = new GameObject("Fellows").transform;
            parent.SetParent(this.transform);
            fellowPool = new ObjectPool<Fellow>(
                128,
                () => Instantiate(fellowPrefab, Vector3.zero, Quaternion.identity, parent),
                item => {
                    item.transform.SetParent(parent);
                    item.gameObject.SetActive(false);
                }
            );
        }

        public ObjectPool<ParticleSystem> GetParticlePool(ParticleNames particleName)
        {
            if (poolsDictionary.TryGetValue(particleName, out ObjectPool<ParticleSystem> result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public ObjectPool<Fellow> GetFellowPool
        {
            get { return fellowPool; }
        }
    }
}
