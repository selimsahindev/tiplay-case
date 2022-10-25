using UnityEngine;
using DG.Tweening;
using Game.Core.Events;
using Game.Managers;
using EventType = Game.Core.Enums.EventType;

namespace Game.Core
{
    public class Dollar : MonoBehaviour
    {
        public int value = 20;
        public ParticleSystem moneySplashParticle;
        private Collider col;

        private void Awake()
        {
            col = GetComponent<Collider>();
        }

        private void Collect()
        {
            col.enabled = false;
            PlayMoneySplash();
            transform.DOScale(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));

            DataManager.Instance.SetMoney(DataManager.Instance.Money + value);
            EventBase.NotifyListeners(EventType.MoneyUpdated);
        }

        private void PlayMoneySplash()
        {
            ParticleSystem particle = Instantiate(moneySplashParticle, transform.position, Quaternion.identity, null);
            particle.transform.position += Vector3.up * 0.15f;
            particle.transform.localScale = Vector3.one * 0.25f;
            particle.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }
    }
}
