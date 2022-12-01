using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleManager : MonoBehaviour
{
    public GameObject _obstacleTemplate;
    [SerializeField] protected List<GameObject> _list_Obj_Obstacle;
    protected float _nowTime;
    protected float _randomDelay = 0;
    [SerializeField] protected float _minDelay;
    [SerializeField] protected float _maxDelay;

    [SerializeField] protected float _minXpos;
    [SerializeField] protected float _maxXpos;

    public abstract void DifficultyInit();
    public abstract void DifficultyUp();
    public abstract void DestroyObstacles();
}
