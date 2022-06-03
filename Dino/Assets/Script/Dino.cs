using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    float _keyHorizontal;       //  좌우키입력
    Vector3 _moveVec;           //  이동방향
    Quaternion _moveQua;        //  이동각도(캐릭터회전)
    public float _moveSpeed;    //  이동속드

    public BtnManager _btnMgr;
    GameManager _GameMgr;
    Animator _animator;

    void Start()
    {
        _GameMgr = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 인트로이거나 게임오버, 일시정지 일경우 애니메이터 비활성 및 Dino 멈추기
        if (_GameMgr._isIntro == true || _GameMgr._isGameOver == true || _GameMgr._isPause)
        {   
            _animator.enabled = false;
            return;
        }
        else
            _animator.enabled = true;
        GetKey();
        Move();
        Look();
    }
    // 좌우 화살표 키보드 입력
    void GetKey()
    {
        _keyHorizontal = Input.GetAxisRaw("Horizontal");
    }
    // 입력된 방향으로 이동
    void Move()
    {
        _moveVec = new Vector3(_keyHorizontal, 0);
        transform.position += _moveVec * _moveSpeed * Time.deltaTime;
    }
    // 이동방향으로 캐릭터 각도회전 및 버튼UI 활성화
    void Look()
    {
        if (_keyHorizontal > 0)
        {
            _moveQua = new Quaternion();
            _btnMgr.OnBtnRArrow();
        }
        else if (_keyHorizontal < 0)
        {
            _moveQua = new Quaternion(0, 180, 0, 0);
            _btnMgr.OnBtnLArrow();
        }
        else
        {
            _moveQua = transform.rotation;
            _btnMgr.OffBtnLArrow();
            _btnMgr.OffBtnRArrow();
        }
        transform.rotation = _moveQua;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 땅에 닿지 않은 운석일경우 게임오버
        if(collision.tag == "Rock" && collision.GetComponent<Rock>()._onGround == false)
        {
            _GameMgr.On_GameOver();
        }
    }
}
