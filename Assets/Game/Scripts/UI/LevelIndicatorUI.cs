using Game.Managers;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class LevelIndicatorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;

        private void Start()
        {
            levelText.text = $"LEVEL {LevelManager.Instance.ActiveLevelIndex}";
        }
    }
}
