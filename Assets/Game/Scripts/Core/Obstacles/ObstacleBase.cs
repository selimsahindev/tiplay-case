using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class ObstacleBase : MonoBehaviour
    {
        [SerializeField] protected int damage = 3;

        protected Collider col;

        protected virtual void Awake()
        {
            col = GetComponent<Collider>();
        }
    }
}
