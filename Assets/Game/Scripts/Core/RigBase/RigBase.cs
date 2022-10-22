using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.RigBase
{
    [System.Serializable]
    public class FellowPlacementData
    {
        [HideInInspector] public bool isUsed = false;
        public Transform position;
        public string poseName;
    }

    public class RigBase : MonoBehaviour
    {
        public List<FellowPlacementData> fellowPlaces = new List<FellowPlacementData>();
        public List<Fellow> fellows = new List<Fellow>();

        private FellowPlacementData GetEmptyPlacementData()
        {
            foreach (FellowPlacementData place in fellowPlaces)
            {
                if (!place.isUsed)
                {
                    return place;
                }
            }

            return null;
        }

        public virtual void AddFellowToRig(Fellow fellow)
        {
            fellows.Add(fellow);
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

                fellows[i].Animator.SetTrigger(fellowPlaces[i].poseName);
            }
        }
    }
}