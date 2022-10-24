namespace Game.Core.RigBase
{
    public class SmgRig : RigBase
    {
        public override void AddFellowToRig(Fellow fellow)
        {
            base.AddFellowToRig(fellow);
            Rearrange();
        }
    }
}
