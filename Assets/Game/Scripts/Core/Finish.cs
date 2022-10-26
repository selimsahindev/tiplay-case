using System.Collections;
using System.Collections.Generic;
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
                col.enabled = false;

                ParticleSystem confetti = Instantiate(confettiPrefab, transform.position.Modify(y: 2f), Quaternion.identity, transform);
                confetti.Play();
            }
        }
    }
}
