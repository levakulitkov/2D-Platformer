public class PlayerAnimationsSetter : AnimationsSetter
{
    public void Set(float inputX, bool isGrounded, bool isJumped, bool isAttacked)
    {
        Animator.SetBool(PlayerAnimatorData.Params.IsRun, inputX != 0);
        SetFlipX(inputX, PlayerAnimatorData.DefaultSpriteDirectionIsRight);
        
        Animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
        
        if (isJumped)
            Animator.SetTrigger(PlayerAnimatorData.Params.Jumped);
        
        if (isAttacked)
            Animator.SetTrigger(PlayerAnimatorData.Params.Attacked);
    }
}
