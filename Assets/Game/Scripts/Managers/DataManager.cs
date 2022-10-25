using Game.Singletons;
using UnityEngine;

namespace Game.Managers
{
    public class DataManager : SingletonMonoBehaviour<DataManager>
    {
        private readonly string LevelData = "level_data";
        private readonly string MoneyData = "money_data";
        private readonly string VibrationData = "vibration_data";

        public int Level { get; private set; }
        public int Money { get; private set; }
        public bool Vibration { get; private set; }

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
        }

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
    }
}