using UnityEngine;

public class EnemyAnimationsSetter : AnimationsSetter
{
    public void Set(Vector2 offset)
    {
        Animator.SetBool(EnemyAnimatorData.Params.IsRun, offset.x != 0);
        SetFlipX(offset.x, false);
    }
}
