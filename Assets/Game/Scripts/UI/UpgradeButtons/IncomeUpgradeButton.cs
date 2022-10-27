using Game.Core.Constants;
using Game.Core.Events;
using Game.Managers;
using UnityEngine;
using EventType = Game.Core.Enums.EventType;

namespace Game.UI
{
    public class IncomeUpgradeButton : UpgradeButtonBase
    {
        private void Start()
        {
            // Check if the upgrade is affordable once at the beginning.
            SetLockStatus(!IsUpgradeAffordable(DataManager.Instance.IncomeUpgrade, out _));
            UpdateTexts();
        }

        protected override void Upgrade()
        {
            UpgradeIncome();
            UpdateTexts();
        }

        private void UpgradeIncome()
        {
            int upgradeLevel = DataManager.Instance.IncomeUpgrade;

            if (IsUpgradeAffordable(upgradeLevel, out int cost))
            {
                DataManager.Instance.SetIncomeUpgrade(upgradeLevel + 1);
                LevelManager.Instance.RemoveMoney(cost);
                EventBase.NotifyListeners(EventType.IncomeUpgraded);
            }
        }

        private void UpdateTexts()
        {
            SetLevelText(DataManager.Instance.IncomeUpgrade.ToString());
            SetPriceText(Mathf.RoundToInt(GameSettings.GetUpgradeCost(DataManager.Instance.IncomeUpgrade)).ToString());
        }

        protected override void OnMoneyUpdated()
        {
            // Check if next upgrade is still affordable and update the lock status.
            SetLockStatus(!IsUpgradeAffordable(DataManager.Instance.IncomeUpgrade, out _));
            UpdateTexts();
        }
    }
}
