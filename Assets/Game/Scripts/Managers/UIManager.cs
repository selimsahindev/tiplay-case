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
        public EndPanel endPanel;
        public CommonPanel commonPanel;

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        private void Start()
        {
            mainPanel.SetActiveImmediately(true);
            gamePanel.SetActiveImmediately(false);
            endPanel.SetActiveImmediately(false);
            commonPanel.SetActiveImmediately(true);
        }

        private void HandleGameStartedEvent()
        {
            mainPanel.SetActiveSmooth(false);
            gamePanel.SetActiveSmooth(true);
        }

        private void HandleGameOverEvent(bool status)
        {
            gamePanel.SetActiveSmooth(false);
            commonPanel.SetActiveSmooth(false);
            endPanel.SetActiveSmooth(true);

            if (status)
            {
                endPanel.EnableSuccessScreen();
            }
            else
            {
                endPanel.EnableFailScreen();
            }
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.GameStarted, HandleGameStartedEvent);
            EventBase.StartListening(EventType.GameOver, HandleGameOverEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.GameStarted, HandleGameStartedEvent);
            EventBase.StopListening(EventType.GameOver, HandleGameOverEvent);
        }
    }
}
