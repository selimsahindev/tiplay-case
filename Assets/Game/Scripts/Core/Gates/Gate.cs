using UnityEngine;
using Game.Managers;
using UnityEngine.Events;
using TMPro;

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

        private PoolLibrary poolLib;
        public Collider col { get; private set; }

        [HideInInspector] public UnityEvent onGateIsUsed = new UnityEvent();

        private void Awake()
        {
            poolLib = ServiceProvider.GetManager<PoolLibrary>();
            col = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            RigManager rigManager = other.GetComponent<RigManager>();

            if (rigManager != null)
            {
                if (value > 0)
                {
                    for (int i = 0; i < value; i++)
                    {
                        Debug.Log("added");
                        Fellow fellow = poolLib.GetFellowPool.Pop();
                        fellow.gameObject.SetActive(true);
                        fellow.transform.position = rigManager.transform.position;
                        rigManager.AddNewFellow(fellow);
                    }
                }
                else if (value < 0)
                {
                    rigManager.RemoveFellows(Mathf.Abs(value));
                }

                surfaceMeshRenderer.material = greyMaterial;
                col.enabled = false;
                onGateIsUsed.Invoke();
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
