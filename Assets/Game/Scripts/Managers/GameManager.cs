using UnityEngine;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour, IProvidable
    {
        public bool IsGameActive
        {
            get;
            private set;
        }

        private void Awake()
        {
            IsGameActive = false;
            ServiceProvider.Register(this);
        }

        public void StartGame()
        {
            IsGameActive = true;
            EventBase.NotifyListeners(EventType.OnGameStart);
        }

        public void EndGame(bool status)
        {
            IsGameActive = false;
            EventBase.NotifyListeners(EventType.OnGameOver, status);
        }
    }
}
