using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    BoxCollider2D _col;
    SpriteRenderer _spr_Warning;
    float _nowTime;

    public void Init()
    {
        if (!TryGetComponent(out _col))
            Debug.LogError("_col is Null");
        if(!transform.Find("Fire_Warning").TryGetComponent(out _spr_Warning))
            Debug.LogError("_spr_Warning is Null");
    }
    
    // Update is called once per frame
    void Update()
    {
        _nowTime += Time.deltaTime;
    }

    public void SetCol(bool stat)
    {
        if (this != null)
            _col.enabled = stat;
    }
}
