using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    // 오브젝트 원형
    public GameObject _rockTemplate;
    Vector3 _templatePos;
    // 랜덤 생정주기 관련
    float _random_delay;
    public float _minDelay;
    public float _maxDelay;
    // 랜점 생성위치 관련
    public float _minXpos;
    public float _maxXpos;
    
    GameManager _gameMgr;
    public GameObject[] _Rocks;
    void Start()
    {
        _rockTemplate.SetActive(false);
        _gameMgr = FindObjectOfType<GameManager>();
        _templatePos = _rockTemplate.transform.position;
    }
    public void Start_MakeRock()
    {
        _random_delay = Random.Range(_minDelay, _maxDelay);
        Invoke("MakeRock", _random_delay);
    }
    void MakeRock()
    {
        if (_gameMgr._isGameOver == true)
            return;
        // 게임오브젝트 복제
        GameObject cloneObj = Instantiate(_rockTemplate);
        // 랜덤 위치 생성
        float xPos = Random.Range(_minXpos, _maxXpos);
        cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);
        // 활성화
        cloneObj.SetActive(true);

        // 다음 생성주기 재설정
        _random_delay = Random.Range(_minDelay, _maxDelay);

        // 지연재귀호출
        Invoke("MakeRock", _random_delay);
    }
}
