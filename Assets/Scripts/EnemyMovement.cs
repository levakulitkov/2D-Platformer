using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Transform[] _waypoints;
    private int _currentWaypointIndex;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _waypoints[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform target = _waypoints[_currentWaypointIndex];
        SetFlipX(target.position.x);
        
        _animator.SetBool(AnimatorPlayer.Params.IsRun, true);
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        
        if (transform.position == target.position)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Length)
                _currentWaypointIndex = 0;
        }
    }
    
    private void SetFlipX(float targetX)
    {
        if (targetX > transform.position.x && !_renderer.flipX)
            _renderer.flipX = true;
        else if (targetX < transform.position.x && _renderer.flipX)
            _renderer.flipX = false;
    } 
}