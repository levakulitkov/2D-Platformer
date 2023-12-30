using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _aggressiveStateMultiplier = 2;

    public void MoveTo(Transform target, bool isAggressiveState)
    {
        if (target is null)
            return;

        float speed = isAggressiveState ? _speed * _aggressiveStateMultiplier : _speed;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}