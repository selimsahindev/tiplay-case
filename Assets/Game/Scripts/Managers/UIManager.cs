using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Panels;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class UIManager : MonoBehaviour, IProvidable
    {
        public MainPanel mainPanel;
        public GamePanel gamePanel;
        public CommonPanel commonPanel;

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        private void Start()
        {
            mainPanel.SetActiveImmediately(true);
            gamePanel.SetActiveImmediately(false);
            commonPanel.SetActiveImmediately(true);
        }

        private void HandleGameStartedEvent()
        {
            mainPanel.SetActiveSmooth(false);
            gamePanel.SetActiveSmooth(true);
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
