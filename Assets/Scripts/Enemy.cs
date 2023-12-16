using UnityEngine;

[RequireComponent(typeof(EnemyAnimationsSetter), typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    
    private EnemyAnimationsSetter _animator;
    private EnemyMover _mover;
    private Transform[] _waypoints;
    private int _currentWaypointIndex;
    
    private void Awake()
    {
        _animator = GetComponent<EnemyAnimationsSetter>();
        _mover = GetComponent<EnemyMover>();
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _waypoints[i] = _path.GetChild(i);
    }

    private void Update()
    {
        Transform target = _waypoints[_currentWaypointIndex];
        Vector3 previousPosition = transform.position;
        
        _mover.MoveTo(target);
        
        _animator.Set(transform.position - previousPosition);

        if (transform.position == target.position)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
    }
}