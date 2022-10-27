using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using MoreMountains.NiceVibrations;
using DG.Tweening;
using Game.Managers;
using Game.Core.Constants;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UpgradeButtonBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI levelText;
        [SerializeField] protected TextMeshProUGUI priceText;
        [SerializeField] private Image grayOverlay;

        protected bool isLocked = false;

        private Vector3 initialScale;
        private CanvasGroup canvasGroup;
        private Tween fadeTween;
        private Tween scaleTween;

        protected static UnityEvent freeUpgradeCompletedEvent = new UnityEvent();

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            initialScale = transform.localScale;

            GetComponent<Button>().onClick.AddListener(OnCLick);
        }

        protected bool IsUpgradeAffordable(int upgradeLevel, out int cost)
        {
            cost = GameSettings.GetUpgradeCost(upgradeLevel);
            return DataManager.Instance.Money >= cost;
        }

        protected void SetLevelText(string text)
        {
            levelText.text = text;
        }

        protected void SetPriceText(string text)
        {
            priceText.text = text;
        }

        protected void SetLockStatus(bool isLocked)
        {
            // Keep lock information
            this.isLocked = isLocked;

            // Kill fade tween to be able to change canvasGroup alpha immediately.
            fadeTween.Kill();

            // Set alpha value
            canvasGroup.alpha = isLocked ? 0.75f : 1f;

            // Set gray overlay
            grayOverlay.enabled = isLocked;
        }

        private void ClickAnimation()
        {
            fadeTween.Kill();
            fadeTween = canvasGroup.DOFade(0.75f, 0.1f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() => {
                    canvasGroup.DOFade(1f, 0.1f).SetEase(Ease.OutQuart);
                });

            scaleTween.Kill();
            scaleTween = transform.DOScale(initialScale * 0.95f, 0.1f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() => {
                    transform.DOScale(initialScale, 0.1f).SetEase(Ease.OutQuart);
                });
        }

        public virtual void OnCLick()
        {
            // Check if next upgrade is still affordable and update the lock status.
            SetLockStatus(!IsUpgradeAffordable(DataManager.Instance.StickmanUpgrade, out _));

            if (!isLocked)
            {
                ClickAnimation();
            }

            Upgrade();

            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        #region Virtual Methods

        protected virtual void OnMoneyUpdated()
        {
            return;
        }

        protected virtual void Upgrade()
        {
            return;
        }

        #endregion

        private void OnEnable()
        {
            EventBase.StartListening(EventType.MoneyAdded, OnMoneyUpdated);
            EventBase.StartListening(EventType.MoneyDecreased, OnMoneyUpdated);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.MoneyAdded, OnMoneyUpdated);
            EventBase.StopListening(EventType.MoneyDecreased, OnMoneyUpdated);
        }
    }

}
