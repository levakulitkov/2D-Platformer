using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _lowerLimitPositionY;

    private int _money;
    
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