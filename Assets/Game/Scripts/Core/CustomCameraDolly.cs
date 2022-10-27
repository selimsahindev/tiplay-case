using DG.Tweening;
using Game.Core.Events;
using UnityEngine;
using EventType = Game.Core.Enums.EventType;

namespace Game.Core
{
    public class CustomCameraDolly : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private Transform vcamTransform;

        private void LateUpdate()
        {
            Vector3 pos = target.position;

            pos.x = transform.position.x;
            pos.x = Mathf.Lerp(transform.position.x, target.position.x, speed * Time.deltaTime);

            transform.position = pos;
        }

        private void HandleShotsFiredEvent()
        {
            vcamTransform.DOKill();
            vcamTransform.DOShakePosition(0.25f, 0.08f, 14).SetEase(Ease.OutSine);
        }

        private void HandleBarrelExploadedEvent()
        {
            vcamTransform.DOKill();
            vcamTransform.DOShakePosition(0.3f, 0.17f, 5).SetEase(Ease.OutSine);
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.ShotsFired, HandleShotsFiredEvent);
            EventBase.StartListening(EventType.BarrelExploaded, HandleBarrelExploadedEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.ShotsFired, HandleShotsFiredEvent);
            EventBase.StopListening(EventType.BarrelExploaded, HandleBarrelExploadedEvent);
        }
    }
}
