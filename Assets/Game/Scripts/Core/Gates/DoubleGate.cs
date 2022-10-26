using UnityEngine;

namespace Game.Core
{
    public class DoubleGate : MonoBehaviour
    {
        [SerializeField] private Gate gate_L;
        [SerializeField] private Gate gate_R;

        private void Awake()
        {
            // When one of the gates is used, disable the other.
            gate_L.onGateIsUsed.AddListener(() => gate_R.col.enabled = false);
            gate_R.onGateIsUsed.AddListener(() => gate_L.col.enabled = false);
        }
    }
}
