using UnityEngine;

public class PlayerAnimationsSetter : AnimationsSetter
{
    [SerializeField] private Color _VampyrismStateColor;

    private Color _defaultColor;
    private bool isVampyrismAuraActivated;

    private void Start()
    {
        _defaultColor = Renderer.color;
    }

    public void Set(float inputX, bool isGrounded, bool isJumped, bool isAttacked, 
        bool isVampyrismAuraActive)
    {
        Animator.SetBool(PlayerAnimatorData.Params.IsRun, inputX != 0);
        SetFlipX(inputX, PlayerAnimatorData.DefaultSpriteDirectionIsRight);
        
        Animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
        
        if (isJumped)
            Animator.SetTrigger(PlayerAnimatorData.Params.Jumped);
        
        if (isAttacked)
            Animator.SetTrigger(PlayerAnimatorData.Params.Attacked);

        if (!isVampyrismAuraActivated && isVampyrismAuraActive)
        {
            Renderer.color = _VampyrismStateColor;
            isVampyrismAuraActivated = true;
        }
        else if (isVampyrismAuraActivated && !isVampyrismAuraActive)
        {
            Renderer.color = _defaultColor;
            isVampyrismAuraActivated = false;
        }
    }
}
