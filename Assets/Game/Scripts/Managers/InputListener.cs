using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Managers
{
    [DefaultExecutionOrder(-1)]
    public class InputListener : MonoBehaviour, IProvidable, IPointerDownHandler, IPointerUpHandler
    {
        // Change this according to the desired precision.
        [SerializeField] private float maxDistance = 100f;

        public Vector2 Input { get; private set; }

        [HideInInspector] public PointerEventData eventData;
        
        private Vector2 startPos;
        private Vector2 delta;
    
        private void Awake()
        {
            ServiceProvider.Register(this);
        }
    
        private void Update()
        {
            if (eventData != null)
            {
                delta = eventData.position - startPos;
                delta.x = Mathf.Clamp(delta.x, -maxDistance, maxDistance);
                delta.y = Mathf.Clamp(delta.y, -maxDistance, maxDistance);
                Input = delta / maxDistance;
                startPos = eventData.position;
            }
        }
    
        public void OnPointerDown(PointerEventData _eventData)
        {
            eventData = _eventData;
            startPos = eventData.position;
        }
    
        public void OnPointerUp(PointerEventData _eventData)
        {
            eventData = null;
            delta = Vector2.zero;
            startPos = Vector2.zero;
            Input = Vector2.zero;
        }
    }
}