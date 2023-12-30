using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;

    private float _currentValue;

    public event Action Died;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        _currentValue = Mathf.Clamp(_currentValue - damage, 0, _maxValue);
        
        Debug.Log($"У {gameObject.name} осталось {_currentValue}hp");
        
		if (_currentValue == 0)
			Died?.Invoke();
    }

    public void Heal(float value)
    {
        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maxValue);
        
        Debug.Log($"У {gameObject.name} теперь {_currentValue}hp");
    }
}
