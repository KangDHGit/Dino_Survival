using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : ObstacleManager
{
    public static FireManager I;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _obstacleTemplate = transform.Find("FireSet").gameObject;
        if (_obstacleTemplate != null)
            _obstacleTemplate.SetActive(false);
        _list_Obj_Obstacle = new List<GameObject>();
        DifficultyInit();
    }

    private void Update()
    {
        if (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime >=_randomDelay)
        {
            GameObject clone = Instantiate(_obstacleTemplate, this.transform);
            float xPos = Random.Range(_minXpos, _maxXpos);
            clone.transform.position = new Vector3(xPos, clone.transform.position.y, 0);
            clone.SetActive(true);
            if(clone.TryGetComponent(out FireSet fire))
            {
                fire.Init();
                _list_Obj_Obstacle.Add(clone.gameObject);
                StartCoroutine(fire.ActiveWarning());
            }
            _randomDelay = Random.Range(_minDelay, _maxDelay);
            _nowTime = 0;
        }
    }

    public override void DestroyObstacles()
    {
        if(_list_Obj_Obstacle != null)
        {
            foreach (GameObject obj_FireSet in _list_Obj_Obstacle)
            {
                Destroy(obj_FireSet);
            }
            _list_Obj_Obstacle.Clear();
        }
    }

    public override void DifficultyInit()
    {
        _minDelay = 0.7f;
        _maxDelay = 1.3f;
    }
    public override void DifficultyUp()
    {
        if(_maxDelay > _minDelay)
            _maxDelay -= 0.05f;
    }
}
