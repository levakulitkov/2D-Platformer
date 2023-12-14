using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public void MoveTo(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(_speed * Time.deltaTime * direction.x, 0, 0);
    }
}
