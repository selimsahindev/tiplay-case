using Game.Core.Events;
using UnityEngine;
using EventType = Game.Core.Enums.EventType;

namespace Game.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject startCamera;

        private void HandleGameStartedEvent()
        {
            startCamera.SetActive(false);
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.GameStarted, HandleGameStartedEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.GameStarted, HandleGameStartedEvent);
        }
    }
}
