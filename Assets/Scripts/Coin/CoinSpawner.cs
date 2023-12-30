using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _template;
    [SerializeField] private float _creatingTimeInSeconds;

    private Coroutine _coroutine;
    
    private void Start()
    {
        Create();
    }
    
    private void Create()
    {
        Coin coin = Instantiate(_template, transform.position, Quaternion.identity);
        coin.Init(CreateWithDelay);
    }

    private void CreateWithDelay()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoinCreatingRoutine());
    }
    
    private IEnumerator CoinCreatingRoutine()
    {
        var wait = new WaitForEndOfFrame();
        float currentTime = 0;

        while (currentTime < _creatingTimeInSeconds)
        {
            if (!enabled)
                yield break;
            
            currentTime += Time.deltaTime;
            yield return wait;
        }
        
        Create();
    } 
}