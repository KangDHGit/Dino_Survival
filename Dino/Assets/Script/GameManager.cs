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

    bool _platform = false; public bool Platform { get { return _platform; } } // true : pc or other, false : mobile

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        CheckPlatform();
        PcScreen();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause)
                UI_Manager.I.OnClickSetting(true);
            else if (UI_Manager.I._ui_Pause.gameObject.activeSelf)
                UI_Setting.I.OnClickExit();
        }
    }

    void CheckPlatform()
    {
        if (CheckPlatform_DeskTop())
            _platform = true;
        else if (CheckPlaform_Mobile())
        {
            _platform = false;
            Application.targetFrameRate = 60;
        }
        else
            _platform = true;
    }

    bool CheckPlatform_DeskTop()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return true;
            default:
                return false;
        }
    }

    bool CheckPlaform_Mobile()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:
                return true;
            default:
                return false;
        }
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

            RockManager.I.DifficultyUp();
            FireManager.I.DifficultyUp();
            Dino.I.moveSpeedUp();
        }
    }

    public void FeverInit()
    {
        UI_Manager.I._txt_Fever.gameObject.SetActive(false);
        RockManager.I.DifficultyInit();
        FireManager.I.DifficultyInit();
        Dino.I.moveSpeedInit();
        _fever = 1;
        _nextFeverScore = 200;
    }

    public void PcScreen()
    {
        if (CheckPlatform_DeskTop())
            Screen.SetResolution(472, 972, false);
    }
}
