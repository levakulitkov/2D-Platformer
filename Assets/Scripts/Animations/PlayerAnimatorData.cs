using UnityEngine;

public static class PlayerAnimatorData
{
    public const bool DefaultSpriteDirectionIsRight = true;

    public static class Params
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Jumped = Animator.StringToHash(nameof(Jumped));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}