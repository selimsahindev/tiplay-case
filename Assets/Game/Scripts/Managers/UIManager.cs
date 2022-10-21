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

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        private void Start()
        {
            mainPanel.SetActiveImmediately(true);
        }

        private void HandleOnGameStart()
        {
            mainPanel.SetActiveSmooth(false);
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
