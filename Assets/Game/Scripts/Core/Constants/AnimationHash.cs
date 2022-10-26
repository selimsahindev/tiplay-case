using Game.Core.Enums;
using UnityEngine;

namespace Game.Core.Constants
{
    public static class AnimationHash
    {
        public static readonly int Run = Animator.StringToHash("Run");
        public static readonly int Pose1 = Animator.StringToHash("Pose1");
        public static readonly int Pose2 = Animator.StringToHash("Pose2");
        public static readonly int Pose3 = Animator.StringToHash("Pose3");
        public static readonly int Pose4 = Animator.StringToHash("Pose4");
        public static readonly int Pose5 = Animator.StringToHash("Pose5");
        public static readonly int Pose6 = Animator.StringToHash("Pose6");

        public static int TryGetPoseHash(PoseType pose)
        {
            switch (pose)
            {
                case PoseType.Pose1: return Pose1;
                case PoseType.Pose2: return Pose2;
                case PoseType.Pose3: return Pose3;
                case PoseType.Pose4: return Pose4;
                case PoseType.Pose5: return Pose5;
                case PoseType.Pose6: return Pose6;
                default:
                    throw new System.Exception("Invalid PoseType argument.");
            }
        }
    }
}
