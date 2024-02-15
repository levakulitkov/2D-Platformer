using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerAnimationsSetter),
	typeof(PlayerAttackHandler))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const float RayDistance = 0.5f;
    private static readonly string[] WhatAreGroundLayers = new string[] { "Ground" };
    
    private PlayerMover _mover;
    private PlayerAnimationsSetter _animator;
    private PlayerAttackHandler _attackHandler;
    private VampyrismAura _vampyrismAura;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimationsSetter>();
        _attackHandler = GetComponent<PlayerAttackHandler>();
        _vampyrismAura = GetComponentInChildren<VampyrismAura>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw(Horizontal);
        _mover.MoveHorizontally(inputX);

        bool isGrounded = CheckForGround();
        var isJumped = false;
        var isAttacked = false;

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _mover.Jump();
                isJumped = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _attackHandler.Attack(_animator.IsFlipped);
                isAttacked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _vampyrismAura.Activate();
        }
        
        _animator.Set(inputX, isGrounded, isJumped, isAttacked, _vampyrismAura.IsActive);
    }
    
    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RayDistance,
            LayerMask.GetMask(WhatAreGroundLayers));

        return hit.collider != null;
    }
}
