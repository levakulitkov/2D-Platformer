using UnityEngine;

public class EnemyAnimationsSetter : AnimationsSetter
{
    public void Set(Vector2 offset, bool isAttacked)
    {
        Animator.SetBool(EnemyAnimatorData.Params.IsRun, offset.x != 0);
        SetFlipX(offset.x, EnemyAnimatorData.DefaultSpriteDirectionIsRight);
        
        if (isAttacked)
            Animator.SetTrigger(EnemyAnimatorData.Params.Attacked);
    }
}
