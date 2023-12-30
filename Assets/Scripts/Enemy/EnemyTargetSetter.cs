using UnityEngine;

public class EnemyTargetSetter : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _forwardDetectingDistance = 3;
    [SerializeField] private float _backwardDetectingDistance = 1;
    
    private Transform[] _waypoints;
    private int _currentWaypointIndex;
    
    private void Awake()
    {
        _waypoints = new Transform[_path.childCount];

        for (var i = 0; i < _path.childCount; i++)
            _waypoints[i] = _path.GetChild(i);
    }

    public Transform GetTargetPoint()
    {
        Transform target = _waypoints[_currentWaypointIndex];
            
        if (transform.position == target.position)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            target = _waypoints[_currentWaypointIndex];
        }

        return target;
    }
    
    public bool TryDetectPlayer(bool isEnemySpriteFlipped, out Transform target)
    {
        Vector2 forwardDirection = EnemyAnimatorData.DefaultSpriteDirectionIsRight ^ isEnemySpriteFlipped 
            ? Vector2.right 
            : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forwardDirection, _forwardDetectingDistance,
            LayerMask.GetMask("Player"));
        
        if (!hit)
            hit = Physics2D.Raycast(transform.position, -forwardDirection, _backwardDetectingDistance, 
                               LayerMask.GetMask("Player"));
        
        target = hit ? hit.collider.transform : null;
        return hit;
    }
}
