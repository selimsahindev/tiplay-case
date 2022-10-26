using UnityEngine;
using Game.Managers;
using Game.Core.Events;
using Dreamteck.Splines;
using EventType = Game.Core.Enums.EventType;

namespace Game.Core
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float sideSpeed;
        [SerializeField] private float sideBounds;

        private bool isMoving = false;
        private InputListener inputListener;
        private SplineFollower follower;

        private void Awake()
        {
            follower = GetComponentInParent<SplineFollower>();
            follower.followSpeed = 0f;

            inputListener = ServiceProvider.GetManager<InputListener>();
        }

        private void Update()
        {
            if (isMoving)
            {
                MovementUpdate();
            }
        }

        private void MovementUpdate()
        {
            Vector3 pos = transform.localPosition;

            pos.x += inputListener.Input.x * sideSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -sideBounds, sideBounds);

            transform.localPosition = pos;
        }

        private void HandleGameStartedEvent()
        {
            isMoving = true;
            follower.followSpeed = forwardSpeed;
        }

        private void HandleGameOverEvent(bool status)
        {
            isMoving = false;
            follower.followSpeed = 0f;
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
