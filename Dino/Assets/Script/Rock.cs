using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float _dropSpeed;        
    public float _rotSpeed;         
    float _rockRot;                 
    public bool _onGround = false;  
    float _alpha = 1.0f;            

    SpriteRenderer _sprRen;
    AudioSource _landaudio;

    public void Init()
    {
        _sprRen = GetComponent<SpriteRenderer>();
        _landaudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (GameManager.I._isIntro || GameManager.I._isGameOver || GameManager.I._isPause)
            return;
        Drop();
        Rotation();
        Alpha();
    }
    void Drop()
    {
        if(!_onGround)
            transform.position += Vector3.down * _dropSpeed * Time.deltaTime;
    }
    void Rotation()
    {
        if (!_onGround)
        {
            _rockRot += _rotSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, _rockRot);
        }
    }
    void Alpha()
    {
        if (_onGround == true)
        {
            _sprRen.color = new Color(_sprRen.color.r, _sprRen.color.g, _sprRen.color.b, _alpha);
            if (_alpha > 0)
                _alpha -= 0.5f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _landaudio.Play();
            _dropSpeed = 0.0f;
            _rotSpeed = 0.0f;
            _onGround = true;
            Destroy(this.gameObject, 1.0f);
            GameManager.I.PlusScore(10);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
