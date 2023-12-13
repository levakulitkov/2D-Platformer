using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rayDistance;
    [SerializeField] private string[] _whatAreGroundLayers;
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        bool isGrounded = CheckForGround();
        
        if (isGrounded)
            JumpOnKeyDown();
        
        _animator.SetBool(AnimatorPlayer.Params.IsGrounded, isGrounded);
    }

    private void Move()
    {
        float inputX = Input.GetAxisRaw(Horizontal);
        
        switch (inputX)
        {
            case 0:
                _animator.SetBool(AnimatorPlayer.Params.IsRun, false);
                return;
            case 1 when _renderer.flipX:
                _renderer.flipX = false;
                break;
            case -1 when !_renderer.flipX:
                _renderer.flipX = true;
                break;
        }

        transform.Translate(_speed * Time.deltaTime * inputX, 0, 0);
        
        _animator.SetBool(AnimatorPlayer.Params.IsRun, true);
    }

    private bool CheckForGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rigidBody2D.position, Vector2.down, _rayDistance, 
            LayerMask.GetMask(_whatAreGroundLayers));
        
        return hit.collider != null;
    }

    private void JumpOnKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _rigidBody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        
            _animator.SetTrigger(AnimatorPlayer.Params.Jumped);
        }
    }
}