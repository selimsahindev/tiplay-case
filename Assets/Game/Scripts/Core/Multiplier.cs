using UnityEngine;
using TMPro;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;
using DG.Tweening;
using Unity.VisualScripting;

namespace Game.Core
{
    public class Multiplier : MonoBehaviour
    {
        [SerializeField, Min(2)] private int multiplier = 2;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private MeshRenderer meshRenderer;

        public static Multiplier lastAchievedMultiplier;
        public int Value => multiplier;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponent<Collider>().enabled = false;
                lastAchievedMultiplier = this;
            }
        }

        private void HandleGameOverEvent(bool status)
        {
            if (status && lastAchievedMultiplier == this)
            {
                Color color = meshRenderer.material.color;
                meshRenderer.material.DOColor(Color.white, 0.25f)
                    .SetEase(Ease.OutQuart)
                    .OnComplete(() => {
                        meshRenderer.material.DOColor(color, 2f).SetEase(Ease.OutSine).SetDelay(2f);
                    });
            }
        }

        private void OnValidate()
        {
            if (text != null)
            {
                text.text = "X" + multiplier;
            }
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.GameOver, HandleGameOverEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.GameOver, HandleGameOverEvent);
        }
    }
}
