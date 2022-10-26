using DG.Tweening;
using UnityEngine;

namespace Game.Core.RigBase
{
    public class PistolRig : RigBase
    {
        [SerializeField] private Transform slidePartsParent;

        public override void PlayShootingAnimation()
        {
            slidePartsParent.DOKill();
            slidePartsParent.DOLocalMoveZ(-0.35f, 0.08f).OnComplete(() => {
                slidePartsParent.DOLocalMoveZ(0f, 0.2f).SetEase(Ease.OutQuart);
            });
        }

        public override void AddFellowToRig(Fellow fellow)
        {
            base.AddFellowToRig(fellow);
            
            // For the pistol rig only, we will rearrange after the second fellow.
            if (fellows.Count > 1)
            {
                ShootHandler.isActive = true;
                Rearrange();
            }
        }
    }
}
