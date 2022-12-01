using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public static Dino I;

    public float _horizontal;       
    Vector3 _moveVec;           
    Quaternion _moveQua;        
    float _moveSpeed;    

    Animator _animator;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
        MoveSpeedInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I.IsGameOver || GameManager.I.IsPause)
        {
            _animator.enabled = false;
            return;
        }
        _animator.enabled = true;
        if (!GameManager.I.IsIntro)
        {
            if (GameManager.I.Platform)
                GetPcKey();
            else 
                _horizontal = UIManager.I.LBtnValue + UIManager.I.RBtnValue;
            Move();
            Look();
        }
    }
    
    void GetPcKey()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        if (_horizontal > 0)
            UIManager.I.DownTrigger_R(true);
        else if (_horizontal < 0)
            UIManager.I.DownTrigger_L(true);
        else
        {
            UIManager.I.DownTrigger_R(false);
            UIManager.I.DownTrigger_L(false);
        }
    }

    void Move()
    {
        _moveVec = new Vector3(_horizontal, 0);
        transform.position += _moveVec * _moveSpeed * Time.deltaTime;
    }
    
    void Look()
    {
        if (_horizontal > 0)
        {
            _moveQua = new Quaternion();
            MoveAnima();
        }
        else if (_horizontal < 0)
        {
            _moveQua = new Quaternion(0, 180, 0, 0);
            MoveAnima();
        }
        else
        {
            _moveQua = transform.rotation;
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
        if(collision.tag == "Obstacle")
        {
            GameManager.I.OnGameOver();
            GameManager.I.FeverInit();
        }
    }
    public void MoveSpeedUp()
    {
        _moveSpeed += 0.2f;
    }

    public void MoveSpeedInit()
    {
        _moveSpeed = 3.0f;
    }
}
