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

    Animator _animator;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
        moveSpeedInit();
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
            if (CheckPlatform_DeskTop())
                GetPCKey();
            else if (CheckPlaform_Mobile())
                _keyHorizontal = UI_Manager.I.LeftValue + UI_Manager.I.RightValue;
            else
                GetPCKey();
            Move();
            Look();
        }
    }
    
    void GetPCKey()
    {
        _keyHorizontal = Input.GetAxisRaw("Horizontal");
        if (_keyHorizontal > 0)
            UI_Manager.I.DownTrigger_R(true);
        else if (_keyHorizontal < 0)
            UI_Manager.I.DownTrigger_L(true);
        else
        {
            UI_Manager.I.DownTrigger_R(false);
            UI_Manager.I.DownTrigger_L(false);
        }
    }

    public bool CheckPlatform_DeskTop()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return true;
            default:
                return false;
        }
    }

    bool CheckPlaform_Mobile()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:
                return true;
            default:
                return false;
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
    public void moveSpeedUp()
    {
        _moveSpeed += 0.2f;
    }

    public void moveSpeedInit()
    {
        _moveSpeed = 3.0f;
    }
}
