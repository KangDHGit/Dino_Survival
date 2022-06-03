using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    // ������Ʈ ����
    public GameObject _rockTemplate;
    Vector3 _templatePos;
    // ���� �����ֱ� ����
    float _random_delay;
    public float _minDelay;
    public float _maxDelay;
    // ���� ������ġ ����
    public float _minXpos;
    public float _maxXpos;
    
    GameManager _gameMgr;
    //public GameObject[] _rocks;
    List<Rock> _rocks;
    void Start()
    {
        _rockTemplate.SetActive(false);
        _gameMgr = FindObjectOfType<GameManager>();
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
        
        if (_gameMgr._isGameOver || _gameMgr._isPause)
            return;
        // ���ӿ�����Ʈ ���� �� ť�� ���
        GameObject cloneObj = Instantiate(_rockTemplate);
        _rocks.Add(cloneObj.GetComponent<Rock>());
        // ���� ��ġ ����
        float xPos = Random.Range(_minXpos, _maxXpos);
        cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);
        // Ȱ��ȭ
        cloneObj.SetActive(true);

        // ���� �����ֱ� �缳��
        _random_delay = Random.Range(_minDelay, _maxDelay);

        // �������ȣ��
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
