using TMPro;
using UnityEngine;

public class HealthText : HealthView
{
    private const char HealthTextSeparator = '/';

    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _healthText.text = $"{Health.Value}{HealthTextSeparator}{Health.MaxValue}";
    }

    public override void HealthChangeHandler(float value)
    {
        _healthText.text = $"{value}{HealthTextSeparator}{Health.MaxValue}";
    }
}