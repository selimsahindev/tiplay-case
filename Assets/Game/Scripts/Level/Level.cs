using Game.Core.Events;
using UnityEngine;
using EventType = Game.Core.Enums.EventType;

namespace Game.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject startCamera;

        private void HandleOnGameStart()
        {
            startCamera.SetActive(false);
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.OnGameStart, HandleOnGameStart);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.OnGameStart, HandleOnGameStart);
        }
    }
}
