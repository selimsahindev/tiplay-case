using UnityEngine;
using Game.Managers;
using TMPro;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;
using DG.Tweening;

namespace Game.UI
{
    public class MoneyIndicatorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            SetMoney(DataManager.Instance.Money);
        }

        public void SetMoney(int money)
        {
            string moneyString;

            if (money >= 1_000_000)
            {
                float fMoney = money / 1_000_000;
                moneyString = $"${Mathf.CeilToInt(fMoney)}m";
            }
            else if (money >= 1000)
            {
                moneyString = "$" + ((float)money / 1000).ToString("0.0") + "k";
            }
            else
            {
                moneyString = $"${money}";
            }

            text.text = moneyString;
        }

        private void HandleMoneyAddedEvent()
        {
            transform.DOKill(true);
            transform.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo);

            SetMoney(DataManager.Instance.Money);
        }

        private void HandleMoneyDecreasedEvent()
        {
            SetMoney(DataManager.Instance.Money);
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.MoneyAdded, HandleMoneyAddedEvent);
            EventBase.StartListening(EventType.MoneyDecreased, HandleMoneyDecreasedEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.MoneyAdded, HandleMoneyAddedEvent);
            EventBase.StopListening(EventType.MoneyDecreased, HandleMoneyDecreasedEvent);
        }
    }
}
