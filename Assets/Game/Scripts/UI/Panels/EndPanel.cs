using DG.Tweening;
using UnityEngine;

namespace Game.UI.Panels
{
    public class EndPanel : Panel
    {
        [SerializeField] private GameObject successScreen;
        [SerializeField] private GameObject failScreen;
        [SerializeField] private Transform glowImage;

        public void EnableSuccessScreen()
        {
            successScreen.SetActive(true);
            failScreen.SetActive(false);

            glowImage.DOLocalRotate(Vector3.forward * -360f, 4.2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }

        public void EnableFailScreen()
        {
            successScreen.SetActive(false);
            failScreen.SetActive(true);
        }
    }
}
