using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _isIntro = true;
    public bool _isGameOver = false;
    public GameObject _UIIntro;
    public GameObject _UIStart;
    public GameObject _UIGameOver;
    RockManager _rockMgr;
    // 운석 중력제어
    // Start is called before the first frame update
    void Start()
    {
        _UIIntro.SetActive(true);
        _UIStart.SetActive(false);
        _UIGameOver.SetActive(false);
        _rockMgr = FindObjectOfType<RockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick_GameStart()
    {
        _isIntro = false;
        _isGameOver = false;
        _UIIntro.SetActive(false);
        _UIStart.SetActive(true);
        _UIGameOver.SetActive(false);
        _rockMgr.Start_MakeRock();
    }
    public void On_GameOver()
    {
        _isGameOver = true;
        _UIGameOver.SetActive(true);
    }
}
