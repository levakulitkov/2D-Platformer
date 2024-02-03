using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBarSlider : HealthView
{
    [SerializeField] private Slider _smoothHealthSlider;
    [SerializeField] private float _decreaseHealthDuration = 0.7f;

    private void Start()
    {
        _smoothHealthSlider.value = 1;
    }

    public override void HealthChangeHandler(float value)
    {
        StartCoroutine(DecreaseHealthSmoothly(value));
    }

    private IEnumerator DecreaseHealthSmoothly(float value)
    {
        var elapsedTime = 0f;
        var startValue = _smoothHealthSlider.value;
        var targetValue = value / Health.MaxValue;

        while (elapsedTime < _decreaseHealthDuration)
        {
            elapsedTime += Time.deltaTime;
            var normalizedTime = elapsedTime / _decreaseHealthDuration;

            _smoothHealthSlider.value = Mathf.Lerp(startValue, targetValue, normalizedTime);

            yield return null;
        }
    }
}