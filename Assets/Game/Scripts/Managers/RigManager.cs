using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Core.Events;
using Game.Core.Constants;
using Game.Core.RigBase;
using DG.Tweening;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    public class RigManager : MonoBehaviour
    {
        [SerializeField] private Fellow firstFellow;
        [SerializeField] private PistolRig pistolRig;
        [SerializeField] private SmgRig smgRig;
        [SerializeField] private ShotgunRig shotgunRig;

        public int FellowCount => collected.Count;

        private Stack<Fellow> collected = new Stack<Fellow>();

        private UIManager uiManager;

        private void Awake()
        {
            uiManager = ServiceProvider.GetManager<UIManager>();
        }

        private void Start()
        {
            AddNewFellow(firstFellow);
        }

        public void AddNewFellow(Fellow newFellow)
        {
            bool isExtra = false;

            newFellow.Collider.enabled = false;

            if (pistolRig.gameObject.activeSelf)
            {
                if (pistolRig.IsFull())
                {
                    pistolRig.TransferTo(smgRig);
                }
                else
                {
                    pistolRig.AddFellowToRig(newFellow);
                }
            }

            if (smgRig.gameObject.activeSelf)
            {
                if (smgRig.IsFull())
                {
                    smgRig.TransferTo(shotgunRig);
                }
                else
                {
                    smgRig.AddFellowToRig(newFellow);
                }
            }

            if (shotgunRig.gameObject.activeSelf)
            {
                if (shotgunRig.IsFull())
                {
                    // ADD +1 MONEY FOR EXTRA FELLOW
                    newFellow.gameObject.SetActive(false);
                    isExtra = true;
                }
                else
                {
                    shotgunRig.AddFellowToRig(newFellow);
                }
            }

            if (!isExtra)
            {
                collected.Push(newFellow);
            }
        }

        public void RemoveFellows(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                RemoveFellow();
            }
        }

        public void RemoveFellow()
        {
            if (collected.Count == 0)
            {
                GameManager.Instance.EndGame(false);
                return;
            }

            Sequence seq = DOTween.Sequence();
            Fellow fellow = collected.Pop();
            float duration = 0.35f;
            
            fellow.transform.SetParent(null);
            Vector3 movePos = transform.position + Random.onUnitSphere * Random.Range(1f, 2.5f);
            seq.Append(fellow.transform.DOMove(movePos, duration));
            seq.Join(fellow.transform.DORotate(Random.onUnitSphere * Random.Range(30f, 270f), duration));
            seq.Join(fellow.transform.DOScale(0f, duration));
            seq.OnComplete(() => fellow.gameObject.SetActive(false));

            // Move progress indicator.
            uiManager.gamePanel.progressUI.MoveArrow(FellowCount);
        }

        private void HandleGameStartedEvent()
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

                // Move progress indicator.
                uiManager.gamePanel.progressUI.MoveArrow(FellowCount);
            }
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
