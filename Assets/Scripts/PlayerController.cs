using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const float RayDistance = 0.5f;
    private static readonly string[] WhatAreGroundLayers = new string[] { "Ground" };
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerMover _mover;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw(Horizontal);
        SetFlipX(inputX);
        
        _animator.SetBool(AnimatorPlayer.Params.IsRun, inputX != 0);
        
        if (inputX != 0)
            _mover.MoveHorizontally(inputX);

        bool isGrounded = CheckForGround();
        _animator.SetBool(AnimatorPlayer.Params.IsGrounded, isGrounded);
        
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _mover.Jump();
                _animator.SetTrigger(AnimatorPlayer.Params.Jumped);
            }
        }
    }
    
    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RayDistance,
            LayerMask.GetMask(WhatAreGroundLayers));

        return hit.collider != null;
    }

    private void SetFlipX(float inputX)
    {
        if (inputX > 0 && _renderer.flipX)
            _renderer.flipX = false;
        else if (inputX < 0 && !_renderer.flipX)
            _renderer.flipX = true;
    }
}
