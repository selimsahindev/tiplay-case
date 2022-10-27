using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Constants
{
    public static class GameSettings
    {
        public const int BaseEndMoney = 3;

        // Upgrade Settings
        public const int BaseUpgradeCost = 20;
        public const int AdditionalUpgradeCostPerLevel = 10;

        public static int GetUpgradeCost(int level)
        {
            // Multiply upgrade cost every 10 level
            int multiplierEvery10Level = 1 + Mathf.FloorToInt(level / 10);

            return BaseUpgradeCost + (level * multiplierEvery10Level * AdditionalUpgradeCostPerLevel);
        }
    }
}
