using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float _dropSpeed;
    public float _rotSpeed;
    float _rockRot;
    float _alpha = 1.0f;
    SpriteRenderer _sprRen;
    public bool _onGround = false;
    GameManager _GameMgr;
    public Object _rock;
    // Start is called before the first frame update
    void Start()
    {
        _GameMgr = FindObjectOfType<GameManager>();
        _sprRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_GameMgr._isIntro || _GameMgr._isGameOver)
            return;
        Drop();
        Rotation();
        Alpha();
    }
    // 일정한 속도로 떨어짐
    void Drop()
    {
        transform.position += Vector3.down * _dropSpeed * Time.deltaTime;
    }
    // 일정한 속도로 회전
    void Rotation()
    {

        _rockRot += _rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _rockRot);
    }
    void Alpha()
    {
        // 투명화
        if (_onGround == true)
        {
            _sprRen.color = new Color(_sprRen.color.r, _sprRen.color.g, _sprRen.color.b, _alpha);
            if (_alpha > 0)
                _alpha -= 0.5f * Time.deltaTime;
        }
    }
    // 바닥충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 추락 및 회전 멈춤
            _dropSpeed = 0.0f;
            _rotSpeed = 0.0f;
            // 투명화 변수 활성화
            _onGround = true;
            // 객체 삭제 및 컬렉션 삭제
            Destroy(_rock, 1.0f);
        }
    }

    public void Destroy()
    {
        Destroy(_rock);
    }
}
