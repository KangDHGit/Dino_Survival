using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager I;

    public GameObject _rockTemplate;
    Vector3 _templatePos;
    
    float _random_delay;
    public float _minDelay;
    public float _maxDelay;
    
    public float _minXpos;
    public float _maxXpos;
    
    List<Rock> _rocks;

    public void Awake()
    {
        I = this;
    }

    void Start()
    {
        _rockTemplate.SetActive(false);
        _templatePos = _rockTemplate.transform.position;
        _rocks = new List<Rock>();
    }
    public void Start_MakeRock()
    {
        _random_delay = Random.Range(_minDelay, _maxDelay);
        Invoke("MakeRock", _random_delay);
    }
    void MakeRock()
    {
        
        if (GameManager.I._isGameOver || GameManager.I._isPause)
            return;
        
        GameObject cloneObj = Instantiate(_rockTemplate);
        _rocks.Add(cloneObj.GetComponent<Rock>());
        
        float xPos = Random.Range(_minXpos, _maxXpos);
        cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);
        
        cloneObj.SetActive(true);
        
        _random_delay = Random.Range(_minDelay, _maxDelay);
        
        Invoke("MakeRock", _random_delay);
    }

    public void DestroyRocks()
    {
        for (int i = 0; i < _rocks.Count - 1; i++)
        {
            _rocks[i].Destroy();
        }
        _rocks.Clear();
    }
}
