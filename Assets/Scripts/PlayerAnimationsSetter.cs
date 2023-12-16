using UnityEngine;

public class PlayerAnimationsSetter : AnimationsSetter
{
    public void Set(float inputX, bool isGrounded, bool isJumped)
    {
        Animator.SetBool(PlayerAnimatorData.Params.IsRun, inputX != 0);
        SetFlipX(inputX, true);
        
        Animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
        
        if (isJumped)
            Animator.SetTrigger(PlayerAnimatorData.Params.Jumped);
    }
}
