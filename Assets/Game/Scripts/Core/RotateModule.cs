using UnityEngine;

namespace Game.Core
{
    public class RotateModule : MonoBehaviour
    {
        [SerializeField] private Vector3 axis;
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(axis, speed * Time.deltaTime, Space.Self);
        }
    }
}
