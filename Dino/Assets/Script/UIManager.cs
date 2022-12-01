using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager I;

    public GameObject _ui_Intro;
    public GameObject _ui_Start;
    public GameObject _ui_Pause;
    public GameObject _ui_GameOver;

    public Text _txt_Pause;
    public bool _isTxtPauseOn = false;

    public Text _txt_BestScoreNum;
    public Text _txt_ScoreNum;
    public Text _txt_Fever;
    int _fever_OrigSize;

    Image _img_Right;
    Image _img_Left;
    [SerializeField] Sprite _sprUp;
    [SerializeField] Sprite _sprPush;
    float _rightValue; public float RBtnValue { get { return _rightValue; } }
    float _leftValue; public float LBtnValue { get { return _leftValue; } }

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        UISetting.I.Init();
        _ui_Intro = transform.Find("UI_Intro").gameObject;
        if (_ui_Intro != null)
            _ui_Intro.SetActive(true);

        _ui_Start = transform.Find("UI_Start").gameObject;
        if (_ui_Start != null)
        {
            _ui_Start.SetActive(false);
            if (!transform.Find("UI_Start/Trigger_Right").TryGetComponent(out _img_Right))
                Debug.LogError("_img_Right");
            if (!transform.Find("UI_Start/Trigger_Left").TryGetComponent(out _img_Left))
                Debug.LogError("_img_Left");
        }

        _ui_Pause = transform.Find("UI_Pause").gameObject;
        if (_ui_Pause != null)
        {
            if (!_ui_Pause.transform.Find("Txt_Pause").TryGetComponent(out _txt_Pause))
                Debug.LogError("_txt_Pause is Null");
            _ui_Pause.SetActive(false);
        }

        _ui_GameOver = transform.Find("UI_GameOver").gameObject;
        if (_ui_GameOver != null)
            _ui_GameOver.SetActive(false);

        _txt_ScoreNum = transform.Find("UI_Start/Txt_ScoreNum").GetComponent<Text>();
        if (_txt_ScoreNum != null)
            _txt_ScoreNum.text = "0";

        if (transform.Find("Txt_Fever").TryGetComponent(out _txt_Fever))
        {
            _fever_OrigSize = _txt_Fever.fontSize;
            _txt_Fever.gameObject.SetActive(false);
        }
        else
            Debug.LogError("_txt_Fever is Null");

        if (transform.Find("Txt_BestScoreNum").TryGetComponent(out _txt_BestScoreNum))
            _txt_BestScoreNum.text = GameManager.I.BestScore.ToString();
        else
            Debug.LogError("_txt_BestScoreNum is Null");

        TriggerRePos();
    }

    public IEnumerator TextSizeEffect(Text text, int speed)
    {
        while (_txt_Fever.gameObject.activeSelf)
        {
            int count = 0;
            float time = 0;
            _txt_Fever.fontSize = _fever_OrigSize;
            while (count <= 10)
            {
                time += Time.deltaTime;
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
                time += Time.deltaTime;
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
        GameManager.I.IsPause = stat;
        if (GameManager.I.IsGameOver || GameManager.I.IsIntro)
            _txt_Pause.gameObject.SetActive(false);
        else
        {
            _txt_Pause.gameObject.SetActive(true);
            Invoke("FlasingTxtPause", 1.0f);
        }

        _ui_Pause.gameObject.SetActive(stat);
    }

    void FlasingTxtPause()
    {
        if (GameManager.I.IsPause == false)
            return;
        if (_isTxtPauseOn == false)
        {
            _isTxtPauseOn = true;
            _txt_Pause.enabled = false;
            Invoke("FlasingTxtPause", 1.0f);
        }
        else if (_isTxtPauseOn == true)
        {
            _isTxtPauseOn = false;
            _txt_Pause.enabled = true;
            Invoke("FlasingTxtPause", 1.0f);
        }
        return;
    }


    public void DownTrigger_L(bool stat) // true = Down , false = Up
    {
        if (stat)
        {
            _img_Left.sprite = _sprPush;
            _leftValue = -1;
        }
        else
        {
            _img_Left.sprite = _sprUp;
            _leftValue = 0;
        }
    }
    public void DownTrigger_R(bool stat)
    {
        if (stat)
        {
            _img_Right.sprite = _sprPush;
            _rightValue = 1;
        }
        else
        {
            _img_Right.sprite = _sprUp;
            _rightValue = 0;
        }
    }

    public void TriggerRePos()
    {
        RectTransform rect_R = _img_Right.gameObject.GetComponent<RectTransform>();
        Vector2 rePos_R = rect_R.anchoredPosition;
        rePos_R.x *= CameraManager.I.RatioValue;
        rect_R.anchoredPosition = rePos_R;

        RectTransform rect_L = _img_Left.gameObject.GetComponent<RectTransform>();
        Vector2 rePos_L = rect_L.anchoredPosition;
        rePos_L.x *= CameraManager.I.RatioValue;
        rect_L.anchoredPosition = rePos_L;
    }
}
