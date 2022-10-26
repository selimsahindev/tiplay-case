using TMPro;
using UnityEngine;

namespace Game.Core
{
    public class Multiplier : MonoBehaviour
    {
        [SerializeField, Min(2)] private int multiplier = 2;
        [SerializeField] private TextMeshProUGUI text;

        private void OnValidate()
        {
            if (text != null)
            {
                text.text = "X" + multiplier;
            }
        }
    }
}
