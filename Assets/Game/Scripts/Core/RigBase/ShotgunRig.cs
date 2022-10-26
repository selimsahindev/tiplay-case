namespace Game.Core.RigBase
{
    public class ShotgunRig : RigBase
    {
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
