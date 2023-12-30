using UnityEngine;

public static class EnemyAnimatorData
{
    public const bool DefaultSpriteDirectionIsRight = false;

    public static class Params
    {
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
    }
}