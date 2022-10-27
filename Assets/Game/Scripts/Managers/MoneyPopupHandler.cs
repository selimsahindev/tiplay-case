using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class MoneyPopupHandler : MonoBehaviour, IProvidable
    {
        [SerializeField] private RectTransform moneyIndicator;
        private RectTransform moneyPopupPrefab;
        private ObjectPool<RectTransform> moneyPopupPool;

        private void Awake()
        {
            ServiceProvider.Register(this);

            moneyPopupPrefab = Resources.Load<RectTransform>("UI/MoneyPopup");

            moneyPopupPool = new ObjectPool<RectTransform>(
                24,
                () => Instantiate(moneyPopupPrefab, Vector3.zero, Quaternion.identity, transform),
                item => {
                    item.transform.SetParent(transform);
                    item.gameObject.SetActive(false);
                });
        }

        public void ShowMoneyPopup(int count, Vector3 origin, UnityAction onComplete = null)
        {
            float randomizePos = 35f;
            float duration = 0.3f;
            float delay = 0.1f;

            for (int i = 0; i < count; i++)
            {
                RectTransform money = moneyPopupPool.Pop();
                money.position = origin;

                Vector2 randomOffset = new Vector2(Random.Range(-randomizePos, randomizePos), Random.Range(-randomizePos, randomizePos));

                Sequence seq = DOTween.Sequence();

                money.localScale = Vector3.one * 0.75f;
                seq.Append(money.DOAnchorPos(money.anchoredPosition + randomOffset, duration).SetEase(Ease.OutSine));
                seq.Append(money.DOScale(1f, 0.1f).SetEase(Ease.InOutSine));
                seq.Append(money.DOMove(moneyIndicator.position, duration).SetEase(Ease.OutSine).SetDelay(delay));
                seq.OnComplete(() => {
                    moneyPopupPool.Push(money);
                });
                seq.Play();

                money.gameObject.SetActive(true);
                
                if (onComplete != null)
                {
                    DelayHandler.WaitAndInvoke(onComplete.Invoke, 2 * duration + 0.1f + delay);
                }
            }
        }
    }
}
