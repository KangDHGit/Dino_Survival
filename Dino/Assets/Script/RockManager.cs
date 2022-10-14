using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager I;

    public GameObject _rockTemplate;
    Vector3 _templatePos;

    float _nowTime;
    float _random_delay = 0;
    public float _minDelay;
    public float _maxDelay;
    
    public float _minXpos;
    public float _maxXpos;
    
    //public List<Rock> _rocks;
    public List<Rock> _rocks;

    public void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _rockTemplate = transform.Find("Rock").gameObject;
        if(_rockTemplate != null)
            _rockTemplate.SetActive(false);
        _templatePos = _rockTemplate.transform.position;
        _rocks = new List<Rock>();
    }

    private void Update()
    {
        if (GameManager.I._isIntro || GameManager.I._isGameOver || GameManager.I._isPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime >= _random_delay)
        {
            GameObject cloneObj = Instantiate(_rockTemplate, this.transform);
            Rock cloneRock = cloneObj.GetComponent<Rock>();
            cloneRock.Init();
            _rocks.Add(cloneRock);

            float xPos = Random.Range(_minXpos, _maxXpos);
            cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);

            cloneObj.SetActive(true);

            _random_delay = Random.Range(_minDelay, _maxDelay);
            _nowTime = 0;
        }
    }
    public void Start_MakeRock()
    {
        _random_delay = Random.Range(_minDelay, _maxDelay);
        Invoke("MakeRock", _random_delay);
    }

    public void DestroyRocks()
    {
        for (int i = 0; i < _rocks.Count; i++)
        {
            _rocks[i].Destroy();
        }
        _rocks.Clear();
    }
}
