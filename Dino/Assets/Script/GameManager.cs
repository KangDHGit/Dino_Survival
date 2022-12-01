using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    static public GameManager I;

    bool _isIntro = true; public bool IsIntro { get { return _isIntro; } }
    bool _isGameOver = false; public bool IsGameOver { get { return _isGameOver; } }
    bool _isPause = false; public bool IsPause { get { return _isPause; } set { _isPause = value; } }

    public bool _isTxtPauseOn = false;

    double _nextFeverScore = 200;
    [SerializeField] float _fever = 1;
    int _score = 0;
    int _bestScore; public int BestScore { get { return _bestScore; } }

    const string KEY_BEST_SCORE = "user_data_best_score";

    bool _platform = false; public bool Platform { get { return _platform; } } // true : pc or other, false : mobile

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        SetPlatform();
        SetPcScreen();
        CameraManager.I.Init();
        LoadScore();
        RockManager.I.Init();
        FireManager.I.Init();
        Dino.I.Init();
        UISetting.I.Init();
        SoundManager.I.Init();
        UIManager.I.Init();
        FireManager.I.Init();

        _score = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause)
                UIManager.I.OnClickSetting(true);
            else if (UIManager.I._ui_Pause.gameObject.activeSelf)
                UISetting.I.OnClickExit();
        }
    }

    void SetPlatform()
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

    void LoadScore()
    {
        if (PlayerPrefs.HasKey(KEY_BEST_SCORE))
            _bestScore = PlayerPrefs.GetInt(KEY_BEST_SCORE);
        else
            _bestScore = 0;
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt(KEY_BEST_SCORE, _bestScore);
    }

    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;
        _isPause = false;

        UIManager.I._ui_Intro.SetActive(false);
        UIManager.I._ui_Start.SetActive(true);
        UIManager.I._ui_GameOver.SetActive(false);
        
        RockManager.I.DestroyObstacles();
        FireManager.I.DestroyObstacles();

        _score = 0;
        UIManager.I._txt_ScoreNum.text = _score.ToString();

        SoundManager.I._bgmIntro.Stop();
        SoundManager.I._bgmStart.Play();
    }
    
    public void OnGameOver()
    {
        _isGameOver = true;
        UIManager.I._ui_GameOver.SetActive(true);
    }
    
    public void PlusScore(int point)
    {
        _score += (int)(point * _fever);
        if(_score > _bestScore)
        {
            _bestScore = _score;
            SaveScore();
            UIManager.I._txt_BestScoreNum.text = _score.ToString();
        }
        UIManager.I._txt_ScoreNum.text = _score.ToString();

        //ScoreCheck
        if(_score >= _nextFeverScore)
        {
            _fever += 0.5f;
            if (!UIManager.I._txt_Fever.gameObject.activeSelf)
            {
                UIManager.I._txt_Fever.gameObject.SetActive(true);
                StartCoroutine(UIManager.I.TextSizeEffect(UIManager.I._txt_Fever, 1));
            }
            UIManager.I._txt_Fever.text = "FEVER " + String.Format($"{_fever : 0.0}");
            _nextFeverScore *= 2;

            RockManager.I.DifficultyUp();
            FireManager.I.DifficultyUp();
            Dino.I.MoveSpeedUp();
        }
    }

    public void FeverInit()
    {
        UIManager.I._txt_Fever.gameObject.SetActive(false);
        RockManager.I.DifficultyInit();
        FireManager.I.DifficultyInit();
        Dino.I.MoveSpeedInit();
        _fever = 1;
        _nextFeverScore = 200;
    }

    void SetPcScreen()
    {
        if (CheckPlatform_DeskTop())
            Screen.SetResolution(472, 972, false);
    }
}
