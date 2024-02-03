using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : HealthView
{
    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        _healthSlider.value = 1;
    }

    public override void HealthChangeHandler(float value)
    {
        var normalizedHealth = value / Health.MaxValue;
        _healthSlider.value = normalizedHealth;
    }
}