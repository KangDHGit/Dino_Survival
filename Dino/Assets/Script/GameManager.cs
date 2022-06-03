using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public bool _isIntro = true;
    public bool _isGameOver = false;
    public bool _isPause = false;
    // UI제어
    public GameObject _UIIntro;
    public GameObject _UIStart;
    public GameObject _UIPause;
        public Text _txtPause;
        public bool _isTxtPauseOn = false;
    public GameObject _UIGameOver;
    // 게임 장애물 제어
    RockManager _rockMgr;
    // 점수
    public Text _txtScore;
    float _score = 0;

    void Start()
    {
        // 게임 첫화면 UI 제어
        _UIIntro.SetActive(true);
        _UIStart.SetActive(false);
        _UIPause.SetActive(false);
        _UIGameOver.SetActive(false);

        _rockMgr = FindObjectOfType<RockManager>();
        //점수초기화
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver || _isPause)
            return;
        PlusScore();
    }
    // 게임시작, 다시생존 버튼 누를때
    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;

        _UIIntro.SetActive(false);
        _UIStart.SetActive(true);
        _UIGameOver.SetActive(false);
        // 장애물 지우고 재생성
        _rockMgr.DestroyRocks();
        _rockMgr.Start_MakeRock();
        // 점수초기화
        _score = 0;
    }
    // 일시정지 버튼
    public void OnClick_Pause()
    {
        _isPause = true;
        _UIPause.SetActive(true);
        Invoke("FlasingTxtPause", 1.0f);
    }
    public void OnClick_PauseReStart()
    {
        _rockMgr.Start_MakeRock();
        _isPause = false;
        _UIPause.SetActive(false);
    }
    // 일시정지 버튼 반짝임
    void FlasingTxtPause()
    {
        if (_isPause == false)
            return;
        if(_isTxtPauseOn == false)
        {
            _isTxtPauseOn = true;
            _txtPause.enabled = false;
            Invoke("FlasingTxtPause", 1.0f);
        }
        else if(_isTxtPauseOn == true)
        {
            _isTxtPauseOn = false;
            _txtPause.enabled = true;
            Invoke("FlasingTxtPause", 1.0f);
        }
        return;
    }

    // 게임오버시
    public void On_GameOver()
    {
        _isGameOver = true;
        _UIGameOver.SetActive(true);
    }
    // 점수 증가
    void PlusScore()
    {
        _score += 10 * Time.deltaTime;
        int score = Convert.ToInt32(_score);
        _txtScore.text = score.ToString();
    }
}
