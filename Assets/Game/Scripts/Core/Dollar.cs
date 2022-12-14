using UnityEngine;
using DG.Tweening;
using Game.Core.Events;
using Game.Core.Enums;
using Game.Managers;
using EventType = Game.Core.Enums.EventType;

namespace Game.Core
{
    public class Dollar : MonoBehaviour
    {
        public int value = 20;
        private Collider col;

        private MoneyPopupHandler moneyPopupHandler;

        private void Awake()
        {
            col = GetComponent<Collider>();
            moneyPopupHandler = ServiceProvider.GetManager<MoneyPopupHandler>();
        }

        private void Collect()
        {
            col.enabled = false;
            PlayMoneySplash();
            transform.DOScale(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));

            Vector3 moneyPos = GameManager.Instance.mainCamera.WorldToScreenPoint(transform.position);
            moneyPopupHandler.ShowMoneyPopup(1, moneyPos, () => LevelManager.Instance.AddMoney(value));
        }

        private void PlayMoneySplash()
        {
            //ParticleSystem particle = Instantiate(moneySplashParticle, transform.position, Quaternion.identity, null);
            PoolLibrary particleLib = ServiceProvider.GetManager<PoolLibrary>();
            ParticleSystem particle = particleLib.GetParticlePool(ParticleNames.MoneySplash).Pop();
            particle.gameObject.SetActive(true);
            particle.transform.position = transform.position + Vector3.up * 0.15f;
            particle.transform.localScale = Vector3.one * 0.25f;
            particle.Play();
            DelayHandler.WaitAndInvoke(() => {
                particleLib.GetParticlePool(ParticleNames.MoneySplash).Push(particle);
            }, particle.main.duration);
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
