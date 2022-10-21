using UnityEngine;

namespace Game.Core
{
    public class CustomCameraDolly : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;

        private void LateUpdate()
        {
            Vector3 pos = target.position;

            pos.x = transform.position.x;

            pos.x = Mathf.Lerp(transform.position.x, target.position.x, speed * Time.deltaTime);

            transform.position = pos;
        }
    }
}
