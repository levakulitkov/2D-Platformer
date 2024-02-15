using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class AnimationsSetter : MonoBehaviour
{
    protected SpriteRenderer Renderer;    
    protected Animator Animator;

    public bool IsFlipped => Renderer.flipX;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }
    
    protected void SetFlipX(float offset, bool defaultSpriteDirectionIsRight)
    {
        if (offset == 0)
            return;

        bool newFlippedState = defaultSpriteDirectionIsRight ^ offset > 0;

        if (Renderer.flipX != newFlippedState)
            Renderer.flipX = newFlippedState;
    }
}
