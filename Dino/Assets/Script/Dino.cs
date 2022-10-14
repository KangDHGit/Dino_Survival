using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    float _keyHorizontal;       
    Vector3 _moveVec;           
    Quaternion _moveQua;        
    public float _moveSpeed;    

    public BtnManager _btnMgr;
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I._isGameOver == true || GameManager.I._isPause)
        {
            _animator.enabled = false;
            return;
        }
        _animator.enabled = true;
        if (!GameManager.I._isIntro)
        {
            GetKey();
            Move();
            Look();
        }
    }
    
    void GetKey()
    {
        _keyHorizontal = Input.GetAxisRaw("Horizontal");
    }
    
    void Move()
    {
        _moveVec = new Vector3(_keyHorizontal, 0);
        transform.position += _moveVec * _moveSpeed * Time.deltaTime;
    }
    
    void Look()
    {
        if (_keyHorizontal > 0)
        {
            _moveQua = new Quaternion();
            _btnMgr.OnBtnRArrow();
            MoveAnima();
        }
        else if (_keyHorizontal < 0)
        {
            _moveQua = new Quaternion(0, 180, 0, 0);
            _btnMgr.OnBtnLArrow();
            MoveAnima();
        }
        else
        {
            _moveQua = transform.rotation;
            _btnMgr.OffBtnLArrow();
            _btnMgr.OffBtnRArrow();
            StayAnima();
        }
        transform.rotation = _moveQua;
    }
    void MoveAnima()
    {
        _animator.SetBool("isMove", true);
    }
    void StayAnima()
    {
        _animator.SetBool("isMove", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Rock" && collision.GetComponent<Rock>()._onGround == false)
        {
            GameManager.I.On_GameOver();
        }
    }
}
