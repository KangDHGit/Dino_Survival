using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float _dropSpeed;
    [SerializeField] float _rotSpeed;         
    float _rockRot;                 
    bool _onGround = false;  
    float _alpha = 1.0f;            

    SpriteRenderer _sprRen;
    AudioSource _landAudio;

    public void Init()
    {
        _sprRen = GetComponent<SpriteRenderer>();
        _landAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
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
                _alpha -= 0.75f * Time.deltaTime;
            else
            {
                if (this != null)
                    Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _landAudio.Play();
            _dropSpeed = 0.0f;
            _rotSpeed = 0.0f;
            _onGround = true;
            GameManager.I.PlusScore(10);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
