using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Health))]
public class BloodParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> _events = new();
    private Health _health;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _health = GetComponent<Health>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out VampyrismAura vampyrismAura))
    //    {
    //        if (!_particleSystem.isPlaying)
    //        {
    //            var emission = _particleSystem.emission; 
    //            emission.rateOverTime = new MinMaxCurve(1 / vampyrismAura.Cooldown);
    //            _particleSystem.Play();
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out VampyrismAura vampyrismAura))
    //    {
    //        if (!_particleSystem.isStopped)
    //            _particleSystem.Stop();
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out VampyrismAura vampyrismAura))
        {
            int events = _particleSystem.GetCollisionEvents(other, _events);

            Debug.Log("hit:" + events);

            for (int i = 0; i < events; i++)
            {
                _health.TakeDamage(vampyrismAura.Damage);

                var playerHealth = vampyrismAura.GetComponent<Health>();
                playerHealth.Heal(vampyrismAura.Damage);
            }
        }
    }
}
