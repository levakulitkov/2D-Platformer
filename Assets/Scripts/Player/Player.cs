using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _lowerLimitPositionY = -15;
    
    private int _money;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Update()
    {
        if (transform.position.y < _lowerLimitPositionY)
            Die();
    }

    public void TakeCoin(Coin coin)
    {
        _money += coin.Amount;
        Debug.Log(_money);
    }
    
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}