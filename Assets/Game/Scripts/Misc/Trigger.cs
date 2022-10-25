using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public readonly UnityEvent<Collider> onTriggerEnter = new UnityEvent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }
}
