using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    public Button _BtnLeft;
    public Button _BtnRigit;
    public Sprite _ChangeSprite;
    Sprite _OriginalSprite;

    // Start is called before the first frame update
    void Start()
    {
        _OriginalSprite = _BtnLeft.image.sprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBtnLArrow()
    {
        _BtnLeft.image.sprite = _ChangeSprite;
    }
    public void OffBtnLArrow()
    {
        _BtnLeft.image.sprite = _OriginalSprite;
    }
    public void OnBtnRArrow()
    {
        _BtnRigit.image.sprite = _ChangeSprite;
    }
    public void OffBtnRArrow()
    {
        _BtnRigit.image.sprite = _OriginalSprite;
    }
}
