using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    
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
        if (inputX != 0)
            _mover.MoveHorizontally(inputX);

        SetFlipX(inputX);
        _animator.SetBool(AnimatorPlayer.Params.IsRun, inputX != 0);

        bool isGrounded = CheckForGround();
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _mover.Jump();
                _animator.SetTrigger(AnimatorPlayer.Params.Jumped);
            }
        }

        _animator.SetBool(AnimatorPlayer.Params.IsGrounded, isGrounded);
    }
    
    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f,
            LayerMask.GetMask("Ground"));

        return hit.collider != null;
    }

    private void SetFlipX(float inputX)
    {
        switch (inputX)
        {
            case > 0 when _renderer.flipX:
                _renderer.flipX = false;
                break;
            case < 0 when !_renderer.flipX:
                _renderer.flipX = true;
                break;
        }
    }
}