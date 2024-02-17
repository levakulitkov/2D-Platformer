using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event Action<float> Changed;
    public event Action Died;

    [SerializeField] private int _maxValue = 100;

    private float _value;

    public float Value
    {
        get
        {
            return _value;
        }
        private set
        {
            _value = Mathf.Clamp(value, 0, _maxValue);

            Changed?.Invoke(_value);

            if (_value == 0)
                Died?.Invoke();
        }
    }

    public int MaxValue => _maxValue;

    private void Awake()
    {
        Value = _maxValue;
    }

    public void TakeDamage(float value)
    {
        Value -= value;
    }

    public void Heal(float value)
    {
        Value += value;
    }
}
