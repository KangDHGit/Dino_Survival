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

        while (!alphaCheck)
        {
            while (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
            {
                yield return null;
            }

            if (this != null)
            {
                Color color = _spr_Warning.color;
                color.a += (2.0f * Time.deltaTime);
                _spr_Warning.color = color;

                if (color.a >= 1.0f)
                    alphaCheck = true;
            }

            yield return null;
        }

        while (alphaCheck)
        {
            while (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
            {
                yield return null;
            }

            if (this != null)
            {
                Color color = _spr_Warning.color;
                color.a -= (2.0f * Time.deltaTime);
                _spr_Warning.color = color;

                if (color.a <= 0)
                {
                    break;
                }
            }
            yield return null;
        }

        while (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
        {
            yield return null;
        }

        if (this != null)
        {
            _obj_Fire.SetActive(true);
            _col_Fire.enabled = true;
        }

        while (this != null && _anim_Fire.GetCurrentAnimatorStateInfo(0).IsName("Fire") && _anim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.7f && _col_Fire.isActiveAndEnabled)
        {
            if (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
                _anim_Fire.speed = 0.0f;
            else
                _anim_Fire.speed = 1.0f;

            yield return null;
        }

        if (this != null)
        {
            _col_Fire.enabled = false;
        }

        while (this != null && _anim_Fire.GetCurrentAnimatorStateInfo(0).IsName("Fire") && _anim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if (GameManager.I.IsIntro || GameManager.I.IsGameOver || GameManager.I.IsPause)
                _anim_Fire.speed = 0.0f;
            else
                _anim_Fire.speed = 1.0f;

            yield return null;
        }
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
