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
    // ������ �ӵ��� ������
    void Drop()
    {
        transform.position += Vector3.down * _dropSpeed * Time.deltaTime;
    }
    // ������ �ӵ��� ȸ��
    void Rotation()
    {

        _rockRot += _rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _rockRot);
    }
    void Alpha()
    {
        // ����ȭ
        if (_onGround == true)
        {
            _sprRen.color = new Color(_sprRen.color.r, _sprRen.color.g, _sprRen.color.b, _alpha);
            if (_alpha > 0)
                _alpha -= 0.5f * Time.deltaTime;
        }
    }
    // �ٴ��浹ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // �߶� �� ȸ�� ����
            _dropSpeed = 0.0f;
            _rotSpeed = 0.0f;
            // ����ȭ ���� Ȱ��ȭ
            _onGround = true;
            // ��ü ���� �� �÷��� ����
            Destroy(_rock, 1.0f);
        }
    }

    public void Destroy()
    {
        Destroy(_rock);
    }
}
