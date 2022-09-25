using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager I;

    [SerializeField] GameObject _ui_Intro;
    [SerializeField] GameObject _ui_Start;
    [SerializeField] GameObject _ui_Pause;
    [SerializeField] Text _txt_Pause;
    [SerializeField] bool _isTxtPauseOn = false;
    [SerializeField] GameObject _ui_GameOver;
    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _ui_Intro = transform.Find("UI_Intro").gameObject;
        _ui_Start = transform.Find("UI_Start").gameObject;
        _ui_Pause = transform.Find("UI_GameOver").gameObject;
        _ui_Pause.transform.Find("Txt_Pause").TryGetComponent(out _txt_Pause);
    }
}
