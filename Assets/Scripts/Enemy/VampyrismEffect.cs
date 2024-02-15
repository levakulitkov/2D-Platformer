using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(ParticleSystem))]
public class VampyrismEffect : MonoBehaviour
{
    private readonly List<ParticleCollisionEvent> _events = new();

    [SerializeField] private VampyrismAura _vampyrismAura;

    private ParticleSystem _effect;
    private Health _health;
    private VampyrismAuraTarget _potentialTargetOfVampyrismAura;

    private void Awake()
    {
        _effect = GetComponent<ParticleSystem>();
        _health = GetComponent<Health>();

        _potentialTargetOfVampyrismAura = new VampyrismAuraTarget(transform, _effect.Play, _effect.Stop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VampyrismAura aura) && aura.Equals(_vampyrismAura))
        {
            _vampyrismAura.AddTarget(_potentialTargetOfVampyrismAura);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VampyrismAura aura) && aura.Equals(_vampyrismAura))
        {
            _vampyrismAura.RemoveTarget(_potentialTargetOfVampyrismAura);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            int events = _effect.GetCollisionEvents(other, _events);

            for (int i = 0; i < events; i++)
                _vampyrismAura.StealHealth(_health);
        }
    }
}
