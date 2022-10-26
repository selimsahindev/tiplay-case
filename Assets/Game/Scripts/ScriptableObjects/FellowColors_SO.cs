using UnityEngine;
using Game.Core.Enums;

namespace Game.Scriptables
{
    [CreateAssetMenu(fileName = "New Color Data", menuName = "Scriptables/Fellow Color Data")]
    public class FellowColors_SO : ScriptableObject
    {
        [Header("Colors")]
        public Color BlackColor;
        public Color BlueColor;
        public Color OrangeColor;
        public Color YellowColor;
        public Color RedColor;
        public Color GreyColor;

        public Color GetColor(FellowColorType colorType)
        {
            switch (colorType)
            {
                case FellowColorType.Blue: return BlueColor;
                case FellowColorType.Orange: return OrangeColor;
                case FellowColorType.Yellow: return YellowColor;
                case FellowColorType.Red: return RedColor;
                case FellowColorType.Grey: return GreyColor;
                default: return BlackColor;
            }
        }
    }
}
