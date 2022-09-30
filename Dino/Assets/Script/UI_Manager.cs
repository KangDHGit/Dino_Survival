using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager I;

    public GameObject _ui_Intro;
    public GameObject _ui_Start;
    public GameObject _ui_Pause;
    public Text _txt_Pause;
    public bool _isTxtPauseOn = false;
    public GameObject _ui_GameOver;
    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _ui_Intro = transform.Find("UI_Intro").gameObject;
        if (_ui_Intro != null)
            _ui_Intro.SetActive(true);
        _ui_Start = transform.Find("UI_Start").gameObject;
        if (_ui_Start != null)
            _ui_Start.SetActive(false);
        _ui_Pause = transform.Find("UI_Pause").gameObject;
        if (_ui_Pause != null)
            _ui_Pause.SetActive(false);
        _ui_Pause.transform.Find("Txt_Pause").TryGetComponent(out _txt_Pause);
        _ui_GameOver = transform.Find("UI_GameOver").gameObject;
        if (_ui_GameOver != null)
            _ui_GameOver.SetActive(false);
    }
}
