using System.Collections;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 0.2f;
    [SerializeField] private float _damage = 10;

    private Health _target;

    public bool PlayerInAttackRange { get; private set; }
    public bool IsAttackState { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerInAttackRange = true;
            _target = player.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            PlayerInAttackRange = false;
            _target = null;
        }
    }

    public void Attack()
    {
        if (!PlayerInAttackRange || IsAttackState)
            return;
        
        StartCoroutine(SetAttackState());
        
        _target.TakeDamage(_damage);
    }

    private IEnumerator SetAttackState()
    {
        IsAttackState = true;
        
        yield return new WaitForSeconds(_attackCooldown);
        
        IsAttackState = false;
    }
}
