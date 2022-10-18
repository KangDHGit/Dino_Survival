using System.Collections;
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

    public Text _txt_ScoreNum;
    public Text _txt_Fever;
    public int _fever_InitSize;

    public Text _txt_BestScoreNum;
    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        UI_Setting.I.Init();
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
        _txt_ScoreNum = transform.Find("UI_Start/Txt_ScoreNum").GetComponent<Text>();
        if (_txt_ScoreNum != null)
            _txt_ScoreNum.text = "0";
        _txt_Fever = transform.Find("Txt_Fever").GetComponent<Text>();
        if (_txt_Fever != null)
        {
            _fever_InitSize = _txt_Fever.fontSize;
            _txt_Fever.gameObject.SetActive(false);
        }
        if(transform.Find("Txt_BestScoreNum").TryGetComponent(out _txt_BestScoreNum))
        {
            _txt_BestScoreNum.text = GameManager.I._bestScore.ToString();
        }
    }

    public IEnumerator TextSizeEffect(Text text, int speed)
    {
        while (_txt_Fever.gameObject.activeSelf)
        {
            int count = 0;
            float time = 0;
            _txt_Fever.fontSize = _fever_InitSize;
            while (count <= 10)
            {
                time += UnityEngine.Time.deltaTime;
                if (time >= 0.05f)
                {
                    _txt_Fever.fontSize += speed;
                    time = 0.0f;
                    count++;
                }
                yield return null;
            }
            while (count <= 20)
            {
                time += UnityEngine.Time.deltaTime;
                if (time >= 0.05f)
                {
                    _txt_Fever.fontSize -= speed;
                    time = 0.0f;
                    count++;
                }
                yield return null;
            }
        }
        yield return null;
    }

    public void OnClickSetting(bool stat)
    {
        if (GameManager.I._isGameOver || GameManager.I._isIntro)
            _txt_Pause.gameObject.SetActive(false);
        else
            _txt_Pause.gameObject.SetActive(true);
        GameManager.I._isPause = stat;
        _ui_Pause.gameObject.SetActive(stat);
    }
}
