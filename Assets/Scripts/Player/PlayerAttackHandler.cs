using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAttackHandler : MonoBehaviour
{
    private static readonly string[] WhatAreDamageableLayers = new string[]{"Damageable"};
    
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _cooldown = 0.2f;
    [SerializeField] private float _rayDistance = 1f;

    private SpriteRenderer _renderer;
    private bool _isAttackState;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Attack(bool isPlayerSpriteFlipped)
    {
        if (_isAttackState)
            return;

        StartCoroutine(SetAttackState());

        Vector2 forwardDirection = PlayerAnimatorData.DefaultSpriteDirectionIsRight ^ isPlayerSpriteFlipped 
            ? Vector2.right 
            : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, forwardDirection, _rayDistance, 
            LayerMask.GetMask(WhatAreDamageableLayers));

        if (hit)
        {
            var targetHealth = hit.collider.gameObject.GetComponent<Health>();
            targetHealth.TakeDamage(_damage);
        }
    }

    private IEnumerator SetAttackState()
    {
        _isAttackState = true;
        
        yield return new WaitForSeconds(_cooldown);
        
        _isAttackState = false;
    }
}
