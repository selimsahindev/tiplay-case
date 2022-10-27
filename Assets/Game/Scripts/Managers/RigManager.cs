using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Core.Events;
using Game.Core.Constants;
using Game.Core.RigBase;
using DG.Tweening;
using EventType = Game.Core.Enums.EventType;
using MoreMountains.NiceVibrations;

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
        private MoneyPopupHandler moneyPopupHandler;

        private void Awake()
        {
            uiManager = ServiceProvider.GetManager<UIManager>();
            moneyPopupHandler = ServiceProvider.GetManager<MoneyPopupHandler>();

            // Make the child rigs interactable.
            pistolRig.trigger.onTriggerEnter.AddListener(OnTriggerEnter);
            smgRig.trigger.onTriggerEnter.AddListener(OnTriggerEnter);
            shotgunRig.trigger.onTriggerEnter.AddListener(OnTriggerEnter);
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
                    Vector3 origin = GameManager.Instance.mainCamera.WorldToScreenPoint(transform.position);
                    moneyPopupHandler.ShowMoneyPopup(origin, () => LevelManager.Instance.AddMoney(1));
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

        public void EndMove()
        {
            Fellow last = collected.Pop();
            last.transform.SetParent(null);

            Sequence seq = DOTween.Sequence();
            float duration = 0.25f;
            seq.Append(last.transform.DOMoveY(0f, duration));
            seq.Join(last.transform.DORotate(Vector3.up * 180f, duration));

            RemoveFellows(FellowCount);
            GameManager.Instance.EndGame(true);
        }

        public void RemoveFellows(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                RemoveFellow();
            }

            Debug.Log("remaining: " + FellowCount);
        }

        public void RemoveFellow()
        {
            if (collected.Count == 0 && !GameManager.Instance.IsFinishReached)
            {
                GameManager.Instance.EndGame(false);
                return;
            }

            if (collected.TryPop(out Fellow fellow))
            {
                float duration = 0.35f;
                Sequence seq = DOTween.Sequence();
            
                fellow.transform.SetParent(null);
                Vector3 movePos = transform.position + Random.onUnitSphere * Random.Range(1f, 2.5f);
                seq.Append(fellow.transform.DOMove(movePos, duration));
                seq.Join(fellow.transform.DORotate(Random.onUnitSphere * Random.Range(30f, 270f), duration));
                seq.Join(fellow.transform.DOScale(0f, duration));
                seq.OnComplete(() => fellow.gameObject.SetActive(false));

                // Move progress indicator.
                uiManager.gamePanel.progressUI.MoveArrow(FellowCount);
            }
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

                MMVibrationManager.Haptic(HapticTypes.LightImpact);

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
