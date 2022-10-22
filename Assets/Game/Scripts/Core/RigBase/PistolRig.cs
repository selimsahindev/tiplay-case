using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.RigBase
{
    public class PistolRig : RigBase
    {
        public override void AddFellowToRig(Fellow fellow)
        {
            base.AddFellowToRig(fellow);
            
            if (fellows.Count > 1)
            {
                Rearrange();
            }
        }
    }
}
