using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.Scriptables;
using Game.Core.Enums;
using Game.Managers;

namespace Game.Core.RigBase
{
    [System.Serializable]
    public class FellowPlacementData
    {
        [HideInInspector] public bool isUsed = false;
        public Transform position;
        public string poseName;
        public FellowColorType color;
    }

    public class RigBase : MonoBehaviour
    {
        public List<FellowPlacementData> fellowPlaces = new List<FellowPlacementData>();
        public List<Fellow> fellows = new List<Fellow>();

        protected FellowColors_SO colors;
        
        public ShootHandler ShootHandler
        {
            get;
            private set;
        }

        private void Awake()
        {
            colors = Resources.Load<FellowColors_SO>("FellowColors");
            ShootHandler = GetComponent<ShootHandler>();
        }

        public bool IsFull()
        {
            return fellows.Count >= fellowPlaces.Count;
        }

        public virtual void AddFellowToRig(Fellow fellow)
        {
            fellows.Add(fellow);
        }

        public void AddFellowsToRig(List<Fellow> fellows)
        {
            this.fellows.AddRange(fellows);
        }

        public void Transfer(RigBase newRig)
        {
            newRig.gameObject.SetActive(true);
            newRig.AddFellowsToRig(fellows);
            newRig.Rearrange();
            fellows.Clear();
            gameObject.SetActive(false);
        }

        protected void Rearrange()
        {
            foreach (FellowPlacementData place in fellowPlaces)
            {
                place.isUsed = false;
            }

            for (int i = 0; i < fellows.Count; i++)
            {
                if (i >= fellowPlaces.Count)
                {
                    return;
                }

                fellowPlaces[i].isUsed = true;
                fellows[i].transform.SetParent(fellowPlaces[i].position);

                fellows[i].transform.DOKill();

                Sequence seq = DOTween.Sequence();
                seq.Append(fellows[i].transform.DOLocalMove(Vector3.zero, 0.25f));
                seq.Join(fellows[i].transform.DOLocalRotate(Vector3.zero, 0.25f));
                seq.Play();

                fellows[i].SkinnedMeshRenderer.material.color = colors.GetColor(fellowPlaces[i].color);
                fellows[i].Animator.SetTrigger(fellowPlaces[i].poseName);
            }
        }
    }
}
