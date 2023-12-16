using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class AnimationsSetter : MonoBehaviour
{
    private SpriteRenderer _renderer;
    
    protected Animator Animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }
    
    protected void SetFlipX(float offset, bool defaultDirectionIsRight)
    {
        if (offset == 0)
            return;

        bool isFlipped = defaultDirectionIsRight && offset < 0 || !defaultDirectionIsRight && offset > 0;

        if (_renderer.flipX != isFlipped)
            _renderer.flipX = isFlipped;
    }
}
