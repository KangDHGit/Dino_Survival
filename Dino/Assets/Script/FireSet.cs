using System.Collections;
using UnityEngine;

public class FireSet : MonoBehaviour
{
    GameObject _obj_Fire;
    BoxCollider2D _col_Fire;
    Animator _anim_Fire;

    SpriteRenderer _spr_Warning;

    public void Init()
    {
        if (transform.Find("Fire_Warning").TryGetComponent(out _spr_Warning))
            _spr_Warning.color = new Color(1,1,1,0);
        else
            Debug.LogError("_spr_Warning is Null");

        _obj_Fire = transform.Find("Fire").gameObject;
        if (_obj_Fire != null)
        {
            _obj_Fire.SetActive(false);
            if (_obj_Fire.TryGetComponent(out _col_Fire))
                _col_Fire.enabled = false;
            else
                Debug.LogError("_col is Null");
            if(!_obj_Fire.TryGetComponent(out _anim_Fire))
                Debug.LogError("_obj_Fire is Null");
        }
    }

    public IEnumerator ActiveWarning()
    {
        bool alphaCheck = false;
        
        while (!GameManager.I._isIntro || !GameManager.I._isGameOver || !GameManager.I._isPause)
        {
            if(!alphaCheck)
            {
                Color color = _spr_Warning.color;
                color.a += (2.0f * Time.deltaTime);
                _spr_Warning.color = color;

                if (color.a >= 1.0f)
                    alphaCheck = true;
                yield return null;
            }
            
            if(alphaCheck)
            {
                Color color = _spr_Warning.color;
                color.a -= (2.0f * Time.deltaTime);
                _spr_Warning.color = color;

                if (color.a <= 0)
                    break;
                yield return null;
            }
        }
        
        if (!GameManager.I._isIntro || !GameManager.I._isGameOver || !GameManager.I._isPause)
        {
            if (this != null)
            {
                _obj_Fire.SetActive(true);
                _col_Fire.enabled = true;
            }
        }

        while (!GameManager.I._isIntro || !GameManager.I._isGameOver || !GameManager.I._isPause)
        {
            if (_anim_Fire.GetCurrentAnimatorStateInfo(0).IsName("Fire") && _anim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && _col_Fire.isActiveAndEnabled)
                _col_Fire.enabled = false;

            if (_anim_Fire.GetCurrentAnimatorStateInfo(0).IsName("Fire") && _anim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(this.gameObject);
                break;
            }
            yield return null;
        }
        _col_Fire.enabled = false;
        yield return null;
    }
}
