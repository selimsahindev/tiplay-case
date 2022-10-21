using UnityEngine;
using DG.Tweening;

namespace Game.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetActiveImmediately(bool isActive)
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                Debug.Log("Panel's canvas group reference was null.");
            }

            canvasGroup.DOKill();

            canvasGroup.alpha = isActive ? 1f : 0f;
            gameObject.SetActive(isActive);
        }

        public void SetActiveSmooth(bool isActive, float duration = 0.25f)
        {
            canvasGroup.DOKill();

            if (isActive)
            {
                gameObject.SetActive(true);
                canvasGroup.DOFade(1f, duration);
            }
            else
            {
                canvasGroup.DOFade(0f, duration).OnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}
