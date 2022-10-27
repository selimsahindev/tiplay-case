using Game.Core.Constants;
using Game.Core.Events;
using Game.Managers;
using UnityEngine;
using EventType = Game.Core.Enums.EventType;

namespace Game.UI
{
    public class StickmanUpgradeButton : UpgradeButtonBase
    {
        private void Start()
        {
            // Check if the upgrade is affordable once at the beginning.
            SetLockStatus(!IsUpgradeAffordable(DataManager.Instance.StickmanUpgrade, out _));
            UpdateTexts();
        }

        protected override void Upgrade()
        {
            UpgradeStickman();
            UpdateTexts();
        }

        private void UpgradeStickman()
        {
            int upgradeLevel = DataManager.Instance.StickmanUpgrade;

            if (IsUpgradeAffordable(upgradeLevel, out int cost))
            {
                DataManager.Instance.SetStickmanUpgrade(upgradeLevel + 1);
                LevelManager.Instance.RemoveMoney(cost);
                EventBase.NotifyListeners(EventType.StickmanUpgraded);
            }
        }

        private void UpdateTexts()
        {
            SetLevelText(DataManager.Instance.StickmanUpgrade.ToString());
            SetPriceText(Mathf.RoundToInt(GameSettings.GetUpgradeCost(DataManager.Instance.StickmanUpgrade)).ToString());
        }

        protected override void OnMoneyUpdated()
        {
            // Check if next upgrade is still affordable and update the lock status.
            SetLockStatus(!IsUpgradeAffordable(DataManager.Instance.StickmanUpgrade, out _));
            UpdateTexts();
        }
    }
}
