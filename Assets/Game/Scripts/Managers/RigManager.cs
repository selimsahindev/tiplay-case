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
        [SerializeField] private SmgRig smgRig;

        private Stack<Fellow> collected = new Stack<Fellow>();
        private UIManager uiManager;

        public int FellowCount => collected.Count;

        private void Awake()
        {
            uiManager = ServiceProvider.GetManager<UIManager>();   
            AddNewFellow(firstFellow);
        }

        public void AddNewFellow(Fellow newFellow)
        {
            newFellow.Collider.enabled = false;

            collected.Push(newFellow);

            if (pistolRig.gameObject.activeSelf)
            {
                if (pistolRig.IsFull())
                {
                    pistolRig.Transfer(smgRig);
                }
                else
                {
                    pistolRig.AddFellowToRig(newFellow);
                }
            }

            if (smgRig.gameObject.activeSelf)
            {
                smgRig.AddFellowToRig(newFellow);
            }

            // Move progress indicator.
            uiManager.gamePanel.progressUI.MoveArrow(FellowCount);
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

            if (fellow != null)
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
