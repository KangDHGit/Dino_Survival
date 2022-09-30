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
    
    public Text _txtScore;
    float _score = 0;

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        UI_Manager.I.Init();

        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver || _isPause)
            return;
        PlusScore();
    }
    
    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;

        UI_Manager.I._ui_Intro.SetActive(false);
        UI_Manager.I._ui_Start.SetActive(true);
        UI_Manager.I._ui_GameOver.SetActive(false);
        
        RockManager.I.DestroyRocks();
        RockManager.I.Start_MakeRock();
        
        _score = 0;
    }
    
    public void OnClick_Pause()
    {
        _isPause = true;
        UI_Manager.I._ui_Pause.SetActive(true);
        Invoke("FlasingTxtPause", 1.0f);
    }
    public void OnClick_PauseReStart()
    {
        RockManager.I.Start_MakeRock();
        _isPause = false;
        UI_Manager.I._ui_Pause.SetActive(false);
    }
    
    void FlasingTxtPause()
    {
        if (_isPause == false)
            return;
        if(_isTxtPauseOn == false)
        {
            _isTxtPauseOn = true;
            UI_Manager.I._txt_Pause.enabled = false;
            Invoke("FlasingTxtPause", 1.0f);
        }
        else if(_isTxtPauseOn == true)
        {
            _isTxtPauseOn = false;
            UI_Manager.I._txt_Pause.enabled = true;
            Invoke("FlasingTxtPause", 1.0f);
        }
        return;
    }

    
    public void On_GameOver()
    {
        _isGameOver = true;
        UI_Manager.I._ui_GameOver.SetActive(true);
    }
    
    void PlusScore()
    {
        _score += 10 * Time.deltaTime;
        int score = Convert.ToInt32(_score);
        _txtScore.text = score.ToString();
    }
}
