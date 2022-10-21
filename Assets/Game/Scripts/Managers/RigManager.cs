using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Core.Events;
using Game.Core.Constants;
using Game.Core.RigBase;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    public class RigManager : MonoBehaviour
    {
        [SerializeField] private Fellow firstFellow;
        [SerializeField] private PistolRig pistolRig;

        private Stack<Fellow> collected = new Stack<Fellow>();

        private void Start()
        {
            firstFellow.onTriggerEnter.AddListener(OnTriggerEnter);
        }

        public void AddNewFellow(Fellow newFellow)
        {
            collected.Push(newFellow);
        }

        public void RemoveFellow()
        {
            if (collected.Count == 0)
            {
                return;
            }

            collected.Pop();
        }
        
        private void HandleOnGameStart()
        {
            // Set the first character
            firstFellow.Animator.SetTrigger(AnimationHash.Run);
        }

        private void OnTriggerEnter(Collider other)
        {
            Fellow fellow = other.GetComponent<Fellow>();

            if (fellow is not null)
            {
                AddNewFellow(fellow);
            }
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
