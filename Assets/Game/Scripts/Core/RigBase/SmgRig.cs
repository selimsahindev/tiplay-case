using DG.Tweening;
using UnityEngine;

namespace Game.Core.RigBase
{
    public class SmgRig : RigBase
    {
        [SerializeField] private Transform slidePartsParent;

        public override void PlayShootingAnimation()
        {
            slidePartsParent.DOKill();
            slidePartsParent.DOLocalMoveZ(-0.5f, 0.1f).OnComplete(() => {
                slidePartsParent.DOLocalMoveZ(0f, 0.25f).SetEase(Ease.OutQuart);
            });
        }

        public override void AddFellowToRig(Fellow fellow)
        {
            base.AddFellowToRig(fellow);
            Rearrange();

            if (fellows.Count > 0)
            {
                ShootHandler.isActive = true;
            }
        }
    }
}
