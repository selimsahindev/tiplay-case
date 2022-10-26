using DG.Tweening;
using UnityEngine;

namespace Game.Core.Obstacles
{
    public class DestructibleBase : ObstacleBase
    {
        public int health;

        public virtual void GetDamage(int damage)
        {
            health -= damage;
            
            if (health < 0)
            {
                health = 0;
                BreakApart();
            }

            transform.DOKill(true);
            transform.DOScale(transform.localScale * 1.12f, 0.08f).SetLoops(2, LoopType.Yoyo);
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }

        public virtual void BreakApart()
        {
            // Handle Particles Etc.
            Disappear();
        }
    }
}
