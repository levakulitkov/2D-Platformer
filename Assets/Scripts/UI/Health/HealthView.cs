using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class HealthView : MonoBehaviour
{
    protected Health Health;

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    public virtual void HealthChangeHandler(float value)
    {
    }
}
