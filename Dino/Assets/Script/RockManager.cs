using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : ObstacleManager
{
    public static RockManager I;

    Vector3 _templatePos;

    public void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _obstacleTemplate = transform.Find("Rock").gameObject;
        if(_obstacleTemplate != null)
            _obstacleTemplate.SetActive(false);
        _templatePos = _obstacleTemplate.transform.position;
        _list_Obj_Obstacle = new List<GameObject>();
        DifficultyInit();
    }

    private void Update()
    {
        if (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime >= _randomDelay)
        {
            GameObject cloneObj = Instantiate(_obstacleTemplate, this.transform);
            Rock cloneRock = cloneObj.GetComponent<Rock>();
            cloneRock.Init();
            _list_Obj_Obstacle.Add(cloneRock.gameObject);

            float xPos = Random.Range(_minXpos, _maxXpos);
            cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);

            cloneObj.SetActive(true);

            _randomDelay = Random.Range(_minDelay, _maxDelay);
            _nowTime = 0;
        }
    }
    public override void DestroyObstacles()
    {
        if (_list_Obj_Obstacle != null)
        {
            foreach (GameObject obj_Rock in _list_Obj_Obstacle)
            {
                Destroy(obj_Rock);
            }
            _list_Obj_Obstacle.Clear();
        }
    }

    public override void DifficultyInit()
    {
        _minDelay = 0.5f;
        _maxDelay = 1.2f;
        _obstacleTemplate.GetComponent<Rock>()._dropSpeed = 3.5f;
    }
    public override void DifficultyUp()
    {
        if (_maxDelay > _minDelay)
            _maxDelay -= 0.05f;
        _obstacleTemplate.GetComponent<Rock>()._dropSpeed += 0.5f;
    }
}
