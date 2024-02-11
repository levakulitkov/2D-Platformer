using UnityEngine;

public class VampyrismAura : MonoBehaviour
{
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private float _damage = 1f;

    public float Cooldown => _cooldown;
    public float Damage => _damage;
}
