using UnityEngine;
using Game.Managers;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;
using Dreamteck.Splines;

namespace Game.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float sideSpeed;
        [SerializeField] private float sideBounds;

        private bool isMoving = false;
        private InputManager inputManager;
        private SplineFollower follower;

        private void Awake()
        {
            follower = GetComponentInParent<SplineFollower>();
            follower.followSpeed = 0f;

            inputManager = ServiceProvider.GetManager<InputManager>();
        }

        private void Start()
        {
            EventBase.NotifyListeners(EventType.OnGameStart);   
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

            pos.x += inputManager.Input.x * sideSpeed * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, -sideBounds, sideBounds);

            transform.localPosition = pos;
        }

        private void HandleOnGameStart()
        {
            isMoving = true;
            follower.followSpeed = forwardSpeed;
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
