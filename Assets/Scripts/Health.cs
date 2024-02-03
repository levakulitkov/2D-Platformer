using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent<float> Changed;
    public event Action Died;
    
    [SerializeField] private int _maxValue = 100;

    private float _currentValue;

    public int MaxValue => _maxValue;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        _currentValue = Mathf.Clamp(_currentValue - damage, 0, _maxValue);

        Changed?.Invoke(_currentValue);

        if (_currentValue == 0)
			Died?.Invoke();
    }

    public void Heal(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maxValue);

        Changed?.Invoke(_currentValue);
    }
}
