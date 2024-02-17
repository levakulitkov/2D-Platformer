using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampyrismAuraTargetFinder : MonoBehaviour
{
    [SerializeField] private float _intervalInSeconds = 0.2f;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private ParticleSystem _vampyrismEffectTemplate;

    private readonly Dictionary<int, VampyrismAuraTarget> _potentialTargets = new();

    private int _closestTargetId;
    private bool _started;

    public void StartFinding()
    {
        _started = true;

        StartCoroutine(FindingRoutine());
    }

    public void Stop()
    {
        _started = false;

        if (_potentialTargets.TryGetValue(_closestTargetId, out VampyrismAuraTarget closestTarget))
        {
            closestTarget.StopEffect?.Invoke();

            _closestTargetId = 0;

            _potentialTargets.Clear();
        }
    }

    private IEnumerator FindingRoutine()
    {
        var wait = new WaitForSeconds(_intervalInSeconds);
        List<Collider2D> results = new();

        while (_started)
        {
            Physics2D.OverlapCircle(transform.position, _radius, _filter, results);

            List<Enemy> enemies = new();
            foreach (Collider2D collider in results)
                if (!collider.TryGetComponent(out Enemy enemy))
                    enemies.Add(enemy);

            UpdatePotentialTargets(enemies.ToArray());
            UpdateClosestTarget();

            yield return wait;
        }
    }

    private void UpdatePotentialTargets(Enemy[] enemies)
    {
        List<int> inRangeTargetsIds = new();

        foreach (Enemy enemy in enemies)
        {
            int id = enemy.GetInstanceID();

            if (!_potentialTargets.ContainsKey(id))
            {
                ParticleSystem particleSystem = Instantiate(_vampyrismEffectTemplate, enemy.transform, false);
                _potentialTargets.Add(id, new VampyrismAuraTarget(particleSystem.transform, particleSystem.Play, particleSystem.Stop));
            }

            inRangeTargetsIds.Add(id);
        }

        int[] outOfRangeTargetsIds = (inRangeTargetsIds.Count > 0 
            ? _potentialTargets.Keys.Except(inRangeTargetsIds) 
            : _potentialTargets.Keys)
            .ToArray();

        foreach (int id in outOfRangeTargetsIds)
        {
            if (_closestTargetId == id)
            {
                _potentialTargets[id].StopEffect?.Invoke();
                _closestTargetId = 0;
            }

            _potentialTargets.Remove(id);
        }
    }

    private void UpdateClosestTarget()
    {
        Vector3 position = transform.position;
        _potentialTargets.TryGetValue(_closestTargetId, out VampyrismAuraTarget closestTarget);

        int newClosestTargetId = -1;
        float minDistance = closestTarget != null
            ? Vector3.Distance(closestTarget.Transform.position, position)
            : float.MaxValue;

        foreach ((int id, VampyrismAuraTarget potentialTarget) in _potentialTargets)
        {
            float distance = Vector3.Distance(potentialTarget.Transform.position, position);

            if (distance < minDistance)
            {
                newClosestTargetId = id;
                minDistance = distance;
            }
        }

        if (newClosestTargetId != -1)
        {
            closestTarget?.StopEffect?.Invoke();

            _closestTargetId = newClosestTargetId;

            _potentialTargets[newClosestTargetId].PlayEffect?.Invoke();
        }
    }
}