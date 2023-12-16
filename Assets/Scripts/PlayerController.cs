using UnityEngine;

[RequireComponent(typeof(PlayerAnimationsSetter), typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const float RayDistance = 0.5f;
    private static readonly string[] WhatAreGroundLayers = new string[] { "Ground" };
    
    private PlayerAnimationsSetter _animator;
    private PlayerMover _mover;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimationsSetter>();
        _mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw(Horizontal);
        _mover.MoveHorizontally(inputX);

        bool isGrounded = CheckForGround();
        var isJumped = false;
        
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _mover.Jump();
                isJumped = true;
            }
        }
        
        _animator.Set(inputX, isGrounded, isJumped);
    }
    
    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RayDistance,
            LayerMask.GetMask(WhatAreGroundLayers));

        return hit.collider != null;
    }
}
