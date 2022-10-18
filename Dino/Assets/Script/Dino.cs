using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public static Dino I;

    public float _keyHorizontal;       
    Vector3 _moveVec;           
    Quaternion _moveQua;        
    public float _moveSpeed;    

    public BtnManager _btnMgr;
    Animator _animator;

    public bool _pushRightBtn = false;
    public bool _pushLeftBtn = false;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _pushRightBtn = false;
        _pushLeftBtn = false;
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
            if(Application.platform == RuntimePlatform.WindowsEditor)
                GetPCKey();
            Move();
            Look();
        }
    }
    
    void GetPCKey()
    {
        if(_keyHorizontal > 0)
            _btnMgr.OnBtnRArrow(true);
        else if(_keyHorizontal < 0)
            _btnMgr.OnBtnLArrow(true); 
        else
        {
            _btnMgr.OnBtnRArrow(false);
            _btnMgr.OnBtnLArrow(false);
        }
        _keyHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void GetAndroidKey(bool stat, GameObject objBtn) // true = PushBtn false = NotPushBtn
    {
        if(stat)
        {
            if (objBtn.gameObject.name == "Btn_Right")
                _keyHorizontal = 1.0f;
            else if (objBtn.gameObject.name == "Btn_Left")
                _keyHorizontal = -1.0f;
        }
        else
        {
            _keyHorizontal = 0.0f;
        }
            
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
            MoveAnima();
        }
        else if (_keyHorizontal < 0)
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
            GameManager.I.On_GameOver();
            GameManager.I.FeverInit();
        }
    }
}
