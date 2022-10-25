using UnityEngine;
using Game.Core.Events;
using Game.Singletons;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : SingletonMonoBehaviour<GameManager>, IProvidable
    {
        public bool IsGameActive
        {
            get;
            private set;
        }

        private void Awake()
        {
            SetupInstance(false);
            IsGameActive = false;
            ServiceProvider.Register(this);
        }

        public void StartGame()
        {
            IsGameActive = true;
            EventBase.NotifyListeners(EventType.GameStarted);
        }

        public void EndGame(bool status)
        {
            IsGameActive = false;
            EventBase.NotifyListeners(EventType.GameOver, status);
        }
    }
}
