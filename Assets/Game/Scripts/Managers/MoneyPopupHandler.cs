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

        public void ShowMoneyPopup(Vector3 origin, UnityAction onComplete = null)
        {
            float randomizePos = 4f;

            RectTransform money = moneyPopupPool.Pop();
            money.position = origin;

            Vector2 randomPos = money.anchoredPosition + new Vector2(Random.Range(-randomizePos, randomizePos), Random.Range(-randomizePos, randomizePos));

            Sequence seq = DOTween.Sequence();

            money.localScale = Vector3.one * 0.75f;
            seq.Append(money.DOAnchorPos(money.anchoredPosition + randomPos, 0.35f).SetEase(Ease.OutSine));
            seq.Append(money.DOScale(1f, 0.1f).SetEase(Ease.InOutSine));
            seq.Append(money.DOMove(moneyIndicator.position, 0.35f).SetEase(Ease.OutSine));
            seq.OnComplete(() => {
                moneyPopupPool.Push(money);
                onComplete?.Invoke();
            });
            seq.Play();

            money.gameObject.SetActive(true);
        }
    }
}
