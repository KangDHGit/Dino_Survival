using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    static public GameManager I;

    public bool _isIntro = true;
    public bool _isGameOver = false;
    public bool _isPause = false;
    
    public bool _isTxtPauseOn = false;

    double _nextFeverScore = 200;
    public float _fever = 1;
    int _score = 0;
    public int _bestScore;

    const string KEY_BEST_SCORE = "user_data_best_score";

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        CameraManager.I.Init();
        LoadScore();
        RockManager.I.Init();
        FireManager.I.Init();
        Dino.I.Init();
        UI_Setting.I.Init();
        SoundManager.I.Init();
        UI_Manager.I.Init();
        FireManager.I.Init();

        _score = 0;
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey(KEY_BEST_SCORE))
            _bestScore = PlayerPrefs.GetInt(KEY_BEST_SCORE);
        else
            _bestScore = 0;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(KEY_BEST_SCORE, _bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause)
                UI_Manager.I.OnClickSetting(true);
            else if(UI_Manager.I._ui_Pause.gameObject.activeSelf)
                    UI_Setting.I.OnClickExit();
        }
    }
    
    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;
        _isPause = false;

        UI_Manager.I._ui_Intro.SetActive(false);
        UI_Manager.I._ui_Start.SetActive(true);
        UI_Manager.I._ui_GameOver.SetActive(false);
        
        RockManager.I.DestroyRocks();
        FireManager.I.DestroyFireSets();

        _score = 0;
        UI_Manager.I._txt_ScoreNum.text = _score.ToString();

        SoundManager.I._bgm_Intro.Stop();
        SoundManager.I._bgm_Start.Play();
    }
    
    public void On_GameOver()
    {
        _isGameOver = true;
        UI_Manager.I._ui_GameOver.SetActive(true);
    }
    
    public void PlusScore(int point)
    {
        _score += (int)(point * _fever);
        if(_score > _bestScore)
        {
            _bestScore = _score;
            SaveScore();
            UI_Manager.I._txt_BestScoreNum.text = _score.ToString();
        }
        UI_Manager.I._txt_ScoreNum.text = _score.ToString();

        //ScoreCheck
        if(_score >= _nextFeverScore)
        {
            _fever += 0.5f;
            if (!UI_Manager.I._txt_Fever.gameObject.activeSelf)
            {
                UI_Manager.I._txt_Fever.gameObject.SetActive(true);
                StartCoroutine(UI_Manager.I.TextSizeEffect(UI_Manager.I._txt_Fever, 1));
            }
            UI_Manager.I._txt_Fever.text = "FEVER " + String.Format($"{_fever : 0.0}");
            _nextFeverScore *= 2;

            RockManager.I._rockTemplate.GetComponent<Rock>()._dropSpeed++;
            if(RockManager.I._maxDelay > RockManager.I._minDelay)
                RockManager.I._maxDelay -= 0.05f;

            Debug.Log("ScoreCheck Success");
        }
    }

    public void FeverInit()
    {
        UI_Manager.I._txt_Fever.gameObject.SetActive(false);
        RockManager.I._rockTemplate.GetComponent<Rock>()._dropSpeed = 4;
        RockManager.I._maxDelay = 0.75f;
        _fever = 1;
        _nextFeverScore = 200;
    }
}
