using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(VampyrismAuraTargetFinder))]
public class VampyrismAura : MonoBehaviour
{
    private readonly List<ParticleCollisionEvent> _events = new();

    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 10f;

    private Health _vampireHealth;
    private VampyrismAuraTargetFinder _targetFinder;
    private bool _cooldownState;

    public bool IsActive { get; set; }

    private void Awake()
    {
        _vampireHealth = GetComponent<Health>();
        _targetFinder = GetComponent<VampyrismAuraTargetFinder>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out ParticleSystem effect)) 
        {
            Health targetHealth = other.GetComponentInParent<Health>(false);

            if (targetHealth != null)
            {
                int events = effect.GetCollisionEvents(gameObject, _events);

                for (int i = 0; i < events; i++)
                    StealHealth(targetHealth);
            }
        }        
    }

    public void StealHealth(Health target)
    {
        if (IsActive)
        {
            target.TakeDamage(_damage);
            _vampireHealth.Heal(_damage);
        }
    }

    public void TryActivate()
    {
        if (!IsActive && !_cooldownState)
        {
            StartCoroutine(SetActiveState());
        }
    }

    private IEnumerator SetActiveState()
    {
        IsActive = true;

        _targetFinder.StartFinding();

        yield return new WaitForSeconds(_duration);

        _targetFinder.Stop();

        IsActive = false;

        StartCoroutine(SetCooldownState());
    }

    private IEnumerator SetCooldownState()
    {
        _cooldownState = true;

        yield return new WaitForSeconds(_cooldown);

        _cooldownState = false;
    }
}