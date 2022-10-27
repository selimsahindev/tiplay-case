using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Constants
{
    public static class GameSettings
    {
        public const int BaseEndMoney = 2;

        // Upgrade Settings
        public const int BaseUpgradeCost = 20;
        public const int AdditionalUpgradeCostPerLevel = 50;

        public static int GetUpgradeCost(int level)
        {
            // Multiply upgrade cost every 10 level
            int multiplierEvery2Level = 1 + Mathf.FloorToInt(level / 2);

            return BaseUpgradeCost + (level * multiplierEvery2Level * AdditionalUpgradeCostPerLevel);
        }
    }
}
