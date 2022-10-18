using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour
{
    public Button _BtnLeft;
    public Button _BtnRigit;
    public Sprite _ChangeSprite;
    Sprite _OriginalSprite;

    void Start()
    {
        _OriginalSprite = _BtnLeft.image.sprite;
    }
    
    public void OnBtnLArrow(bool stat)
    {
        if (stat)
        {
            _BtnLeft.image.sprite = _ChangeSprite;
            Dino.I._keyHorizontal = -1.0f;
        }
        else if (!stat)
        {
            _BtnLeft.image.sprite = _OriginalSprite;
            Dino.I._keyHorizontal = 0.0f;
        }
    }
    public void OnBtnRArrow(bool stat)
    {
        if (stat)
        {
            _BtnRigit.image.sprite = _ChangeSprite;
            Dino.I._keyHorizontal = 1.0f;
        }
        else if (!stat)
        {
            _BtnRigit.image.sprite = _OriginalSprite;
            Dino.I._keyHorizontal = 0.0f;
        }
    }
}
