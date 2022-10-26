using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering.UI;

namespace Game.UI
{
    public class WeaponProgressUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI weaponNameText;
        [SerializeField] private RectTransform arrowRectTransform;
        [SerializeField] private RectTransform nodesParent;

        public List<RectTransform> progressionNodes = new List<RectTransform>();

        private int currentIndex = 0;

        private Vector2 elementSize;
        private Vector2 baseAnchoredPosition;

        private void Awake()
        {
            Prepare();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int index = (currentIndex + 1) % progressionNodes.Count;
                MoveArrow(index);
            }
        }

        private void Prepare()
        {
            var firstElement = progressionNodes[0];

            elementSize = firstElement.sizeDelta;

            float xDiff = arrowRectTransform.position.x - firstElement.position.x;
            
            nodesParent.position += new Vector3(xDiff - elementSize.x / 2, 0f, 0f);

            baseAnchoredPosition = nodesParent.anchoredPosition;
        }

        public void MoveArrow(int nodeIndex)
        {
            if (nodeIndex > progressionNodes.Count - 1)
            {
                return;
            }

            nodesParent.DOKill();
            nodesParent.DOAnchorPosX(baseAnchoredPosition.x - nodeIndex * elementSize.x, 0.15f).SetEase(Ease.InOutSine);

            currentIndex = nodeIndex;

            SetText(nodeIndex);
        }

        // This could be dynamic but due to time concerns I update the text here statically.
        private void SetText(int index)
        {
            if (index < 5)
            {
                weaponNameText.text = "PISTOL";
            }
            else if (index < 9)
            {
                weaponNameText.text = "SMG";
            }
            else
            {
                weaponNameText.text = "SHOTGUN";
            }
        }
    }
}
