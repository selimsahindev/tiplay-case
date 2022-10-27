using Game.Managers;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Game.Core
{
    public class Finish : MonoBehaviour
    {
        private ParticleSystem confettiPrefab;

        private Collider col;

        private void Awake()
        {
            col = GetComponent<Collider>();
            confettiPrefab = Resources.Load<ParticleSystem>("Particles/ConfettiBlastRainbow");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.IsFinishReached = true;

                col.enabled = false;

                ParticleSystem confetti = Instantiate(confettiPrefab, transform.position.Modify(y: 2f), Quaternion.identity, transform);
                confetti.transform.position += Vector3.forward * 0.5f;
                confetti.transform.localScale = Vector3.one * 0.75f;
                confetti.Play();

                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
        }
    }
}
