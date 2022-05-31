using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    // Start is called before the first frame update
    float _keyHorizontal;
    Vector3 _moveVec;
    Quaternion _moveQua;
    public float _moveSpeed;
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
        if (_GameMgr._isIntro == true || _GameMgr._isGameOver == true)
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
    // �¿� ȭ��ǥ Ű���� �Է�
    void GetKey()
    {
        _keyHorizontal = Input.GetAxisRaw("Horizontal");
    }
    // �Էµ� �������� �̵�
    void Move()
    {
        _moveVec = new Vector3(_keyHorizontal, 0);
        transform.position += _moveVec * _moveSpeed * Time.deltaTime;
    }
    // �̵��������� ĳ���� �¿�ȸ��
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
        if(collision.tag == "Rock" && collision.GetComponent<Rock>()._onGround == false)
        {
            _GameMgr.On_GameOver();
        }
    }
}
