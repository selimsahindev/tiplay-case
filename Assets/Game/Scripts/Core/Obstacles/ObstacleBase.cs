using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class ObstacleBase : MonoBehaviour
    {
        protected Collider col;

        protected virtual void Awake()
        {
            col = GetComponent<Collider>();
        }
    }
}
