using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    private EnemyMover _mover;
    private Transform[] _waypoints;
    private int _currentWaypointIndex;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _mover = GetComponent<EnemyMover>();
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _waypoints[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _waypoints[_currentWaypointIndex];
        
        SetFlipX(target.position.x);
        
        _animator.SetBool(AnimatorEnemy.Params.IsRun, transform.position != target.position);
        
        _mover.MoveTo(target);
        
        if (transform.position == target.position)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
    }
    
    private void SetFlipX(float targetPositionX)
    {
        if (targetPositionX > transform.position.x && !_renderer.flipX)
            _renderer.flipX = true;
        else if (targetPositionX < transform.position.x && _renderer.flipX)
            _renderer.flipX = false;
    } 
}