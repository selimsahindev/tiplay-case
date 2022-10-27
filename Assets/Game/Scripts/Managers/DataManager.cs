using Game.Singletons;
using UnityEngine;

namespace Game.Managers
{
    public class DataManager : SingletonMonoBehaviour<DataManager>
    {
        private readonly string LevelData = "level_data";
        private readonly string MoneyData = "money_data";
        private readonly string VibrationData = "vibration_data";
        private readonly string StickmanUpgradeData = "stickman_upgrade_data";
        private readonly string IncomeUpgradeData = "income_upgrade_data";

        public int Level { get; private set; }
        public int Money { get; private set; }
        public bool Vibration { get; private set; }
        public int StickmanUpgrade { get; private set; }
        public int IncomeUpgrade { get; private set; }

        private void Awake()
        {
            if (!SetupInstance(false))
            {
                return;
            }

            GetDatas();
        }

        private void GetDatas()
        {
            Level = PlayerPrefs.GetInt(LevelData, 1);
            Money = PlayerPrefs.GetInt(MoneyData, 0);
            Vibration = PlayerPrefs.GetInt(VibrationData, 1) == 1;
            StickmanUpgrade = PlayerPrefs.GetInt(StickmanUpgradeData, 0);
            IncomeUpgrade = PlayerPrefs.GetInt(IncomeUpgradeData, 0);
        }

        #region Setters

        public void SetLevel(int _level)
        {
            Level = _level;
            PlayerPrefs.SetInt(LevelData, Level);
            PlayerPrefs.Save();
        }

        public void SetMoney(int _money)
        {
            Money = _money;
            PlayerPrefs.SetInt(MoneyData, _money);
            PlayerPrefs.Save();
        }

        public void SetVibration(bool _enabled)
        {
            Vibration = _enabled;
            PlayerPrefs.SetInt(VibrationData, _enabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetStickmanUpgrade(int _stickmanUpgrade)
        {
            StickmanUpgrade = _stickmanUpgrade;
            PlayerPrefs.SetInt(StickmanUpgradeData, _stickmanUpgrade);
            PlayerPrefs.Save();
        }

        public void SetIncomeUpgrade(int _incomeUpgrade)
        {
            IncomeUpgrade = _incomeUpgrade;
            PlayerPrefs.SetInt(IncomeUpgradeData, _incomeUpgrade);
            PlayerPrefs.Save();
        }

        #endregion
    }
}