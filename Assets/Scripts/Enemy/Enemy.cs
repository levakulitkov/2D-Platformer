using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), 
    typeof(EnemyTargetSetter), typeof(EnemyAnimationsSetter))]
[RequireComponent(typeof(EnemyAttackTrigger), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _aggressiveStateDuration = 2;

    private EnemyMover _mover;
    private EnemyTargetSetter _targetSetter;
    private EnemyAnimationsSetter _animator;
    private EnemyAttackTrigger _attackTrigger;
    private Health _health;
    private bool _isAggressiveState;
    
    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _targetSetter = GetComponent<EnemyTargetSetter>();
        _animator = GetComponent<EnemyAnimationsSetter>();
        _attackTrigger = GetComponent<EnemyAttackTrigger>();
        _health = GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Update()
    {
        bool isPlayer = _targetSetter.TryDetectPlayer(_animator.IsFlipped, out Transform target);
        if (isPlayer && !_isAggressiveState)
            StartCoroutine(SetAggressiveState());
        else if (!_isAggressiveState)
            target = _targetSetter.GetTargetPoint();

        Vector2 offset = default;
        var isAttacked = false;
        if (isPlayer && _attackTrigger.PlayerInAttackRange && !_attackTrigger.IsAttackState)
        {
            _attackTrigger.Attack();
            isAttacked = true;
        }
        else if (!isPlayer || !_attackTrigger.PlayerInAttackRange)
        {
            Vector3 previousPosition = transform.position;
            
            _mover.MoveTo(target, _isAggressiveState);
            
            offset = transform.position - previousPosition;
        }
        
        _animator.Set(offset, isAttacked);
    }

    private IEnumerator SetAggressiveState()
    {
        _isAggressiveState = true;
        
        yield return new WaitForSeconds(_aggressiveStateDuration);
        
        _isAggressiveState = false;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}