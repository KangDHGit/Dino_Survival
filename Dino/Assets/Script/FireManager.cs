using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public static FireManager I;

    public GameObject _fireTemplate;

    float _nowTime;
    float _random_delay = 0;
    public float _minDelay;
    public float _maxDelay;

    public float _minXpos;
    public float _maxXpos;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _fireTemplate = transform.Find("Fire").gameObject;
    }

    private void Update()
    {
        if (GameManager.I._isIntro || GameManager.I._isGameOver || GameManager.I._isPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime > _random_delay)
        {
            // 경고 투명도 조절
        }
    }
    public void FireWarning()
    {

    }
}
