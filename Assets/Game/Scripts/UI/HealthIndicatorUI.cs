using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class HealthIndicatorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetHealth(int health)
        {
            if (health > 0)
            {
                text.text = health.ToString();
            }
        }
    }
}
