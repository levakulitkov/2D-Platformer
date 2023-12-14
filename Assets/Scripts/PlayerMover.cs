using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void MoveHorizontally(float inputX)
    {
        transform.Translate(_speed * Time.deltaTime * inputX, 0, 0);
    }

    public void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
