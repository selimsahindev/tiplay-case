using UnityEngine;
using Game.Core.Events;
using Game.Singletons;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : SingletonMonoBehaviour<GameManager>, IProvidable
    {
        public bool IsFinishReached { get; set; }

        public bool IsGameActive
        {
            get;
            private set;
        }

        public Camera mainCamera { get; private set; }

        private void Awake()
        {
            SetupInstance(false);
            Application.targetFrameRate = 60;
            ServiceProvider.Register(this);

            IsGameActive = false;
            IsFinishReached = false;

            mainCamera = Camera.main;

            DG.Tweening.DOTween.SetTweensCapacity(200, 100);
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
