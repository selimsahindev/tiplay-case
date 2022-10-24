namespace Game.Core.RigBase
{
    public class PistolRig : RigBase
    {
        public override void AddFellowToRig(Fellow fellow)
        {
            base.AddFellowToRig(fellow);
            
            // For the pistol rig only, we will rearrange after the second fellow.
            if (fellows.Count > 1)
            {
                Rearrange();
            }
        }
    }
}
