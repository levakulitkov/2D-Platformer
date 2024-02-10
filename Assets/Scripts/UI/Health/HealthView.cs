using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class HealthView : MonoBehaviour
{
    protected Health Health;

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        Health.Changed += HealthChangeHandler;
    }

    private void OnDisable()
    {
        Health.Changed -= HealthChangeHandler;
    }

    public abstract void HealthChangeHandler(float value);
}
