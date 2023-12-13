using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount;
    
    private Action _startNewCoinCreating;
    
    public int Amount => _amount;

    public void Init(Action startNewCoinCreating)
    {
        _startNewCoinCreating = startNewCoinCreating;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeCoin(this);
            
            _startNewCoinCreating.Invoke();
            
            Destroy(gameObject);
        }
    }
}