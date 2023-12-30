using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _healthPointsCount = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            var health = player.GetComponent<Health>();
            health.Heal(_healthPointsCount);
            
            Destroy(gameObject);
        }
    }
}
