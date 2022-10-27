using UnityEngine;
using Game.Managers;
using UnityEngine.Events;
using TMPro;
using MoreMountains.NiceVibrations;

namespace Game.Core
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private int value;
        [Space]
        [SerializeField] private MeshRenderer surfaceMeshRenderer;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Material blueMaterial;
        [SerializeField] private Material redMaterial;
        [SerializeField] private Material greyMaterial;

        public Collider col { get; private set; }

        [HideInInspector] public UnityEvent onGateIsUsed = new UnityEvent();

        private void Awake()
        {
            col = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponentInParent<RigManager>();

            if (rigManager != null)
            {
                if (value > 0)
                {
                    rigManager.InitializeFellows(value);
                }
                else if (value < 0)
                {
                    rigManager.RemoveFellows(Mathf.Abs(value));
                }

                surfaceMeshRenderer.material = greyMaterial;
                col.enabled = false;
                onGateIsUsed.Invoke();

                MMVibrationManager.Haptic(HapticTypes.LightImpact);
            }
        }

        private void OnValidate()
        {
            if (surfaceMeshRenderer != null)
            {
                surfaceMeshRenderer.material = value < 0 ? redMaterial : blueMaterial;
            }

            if (valueText != null)
            {
                valueText.text = (value < 0 ? "-" : "+") + Mathf.Abs(value).ToString();
            }
        }
    }
}
