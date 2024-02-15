using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampyrismAura : MonoBehaviour
{
    private readonly List<VampyrismAuraTarget> _potentialTargets = new();

    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 10f;

    private Health _vampireHealth;
    private bool _isActive;
    private bool _cooldownState;
    private VampyrismAuraTarget _closestTarget; 

    public bool IsActive => _isActive;

    private void Awake()
    {
        _vampireHealth = GetComponentInParent<Health>();
    }

    private void Update()
    {
        if (_isActive)
            UpdateClosestTarget();
    }

    private void UpdateClosestTarget()
    {
        Vector3 position = transform.position;

        float minDistance = _closestTarget != null 
            ? Vector3.Distance(_closestTarget.Transform.position, position)
            : float.MaxValue;

        VampyrismAuraTarget newClosestTarget = _potentialTargets.FirstOrDefault(potentialTarget => 
            Vector3.Distance(potentialTarget.Transform.position, position) < minDistance);

        if (newClosestTarget != null)
        {
            _closestTarget?.StopEffect?.Invoke();

            _closestTarget = newClosestTarget;

            _closestTarget.PlayEffect?.Invoke();
        }
    }

    public void AddTarget(VampyrismAuraTarget target)
    {
        _potentialTargets.Add(target);
    }

    public void RemoveTarget(VampyrismAuraTarget target)
    {
        _potentialTargets.Remove(target);

        if (_closestTarget != null && _closestTarget.Equals(target))
        {
            _closestTarget.StopEffect?.Invoke();
            _closestTarget = null;
        }
    }

    public void StealHealth(Health target)
    {
        if (_isActive)
        {
            target.TakeDamage(_damage);
            _vampireHealth.Heal(_damage);
        }
    }

    public void Activate()
    {
        if (!_isActive && !_cooldownState)
            StartCoroutine(SetActiveState());
    }

    private IEnumerator SetActiveState()
    {
        _isActive = true;
        float time = 0;

        while (time < _duration)
        {
            time += Time.deltaTime;
            yield return null;
        }       

        _isActive = false;

        _closestTarget?.StopEffect?.Invoke();

        StartCoroutine(SetCooldownState());
    }

    private IEnumerator SetCooldownState()
    {
        _cooldownState = true;
        float time = 0;

        while (time < _cooldown)
        {
            time += Time.deltaTime;
            yield return null;
        }

        _cooldownState = false;
    }
}
