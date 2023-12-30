using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class AnimationsSetter : MonoBehaviour
{
    private SpriteRenderer _renderer;
    
    protected Animator Animator;

    public bool IsFlipped => _renderer.flipX;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }
    
    protected void SetFlipX(float offset, bool defaultSpriteDirectionIsRight)
    {
        if (offset == 0)
            return;

        bool newFlippedState = defaultSpriteDirectionIsRight ^ offset > 0;

        if (_renderer.flipX != newFlippedState)
            _renderer.flipX = newFlippedState;
    }
}
