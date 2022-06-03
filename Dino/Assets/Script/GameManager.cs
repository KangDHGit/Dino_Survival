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
    // UI����
    public GameObject _UIIntro;
    public GameObject _UIStart;
    public GameObject _UIPause;
        public Text _txtPause;
        public bool _isTxtPauseOn = false;
    public GameObject _UIGameOver;
    // ���� ��ֹ� ����
    RockManager _rockMgr;
    // ����
    public Text _txtScore;
    float _score = 0;

    void Start()
    {
        // ���� ùȭ�� UI ����
        _UIIntro.SetActive(true);
        _UIStart.SetActive(false);
        _UIPause.SetActive(false);
        _UIGameOver.SetActive(false);

        _rockMgr = FindObjectOfType<RockManager>();
        //�����ʱ�ȭ
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver || _isPause)
            return;
        PlusScore();
    }
    // ���ӽ���, �ٽû��� ��ư ������
    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;

        _UIIntro.SetActive(false);
        _UIStart.SetActive(true);
        _UIGameOver.SetActive(false);
        // ��ֹ� ����� �����
        _rockMgr.DestroyRocks();
        _rockMgr.Start_MakeRock();
        // �����ʱ�ȭ
        _score = 0;
    }
    // �Ͻ����� ��ư
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
    // �Ͻ����� ��ư ��¦��
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

    // ���ӿ�����
    public void On_GameOver()
    {
        _isGameOver = true;
        _UIGameOver.SetActive(true);
    }
    // ���� ����
    void PlusScore()
    {
        _score += 10 * Time.deltaTime;
        int score = Convert.ToInt32(_score);
        _txtScore.text = score.ToString();
    }
}
