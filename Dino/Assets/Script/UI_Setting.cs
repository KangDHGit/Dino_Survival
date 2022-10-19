using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public static UI_Setting I;

    Slider _slider_MainBgm;
    Slider _slider_EffectBgm;

    GameObject _ui_Exit;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        if (transform.Find("UI_MainBgm/Slider").TryGetComponent(out _slider_MainBgm))
        {
            _slider_MainBgm.value = SoundManager.I._mainVol;
            Debug.Log("_silder_MainBgm Setting Complete");
        }
        if(transform.Find("UI_EffectBgm/Slider").TryGetComponent(out _slider_EffectBgm))
        {
            _slider_EffectBgm.value = SoundManager.I._effectVol;
        }
        _ui_Exit = transform.Find("UI_Exit").gameObject;
        if (_ui_Exit != null)
            _ui_Exit.SetActive(false);
    }

    public void MoveSilder(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "UI_MainBgm":
                SoundManager.I._bgm_Intro.volume = _slider_MainBgm.value;
                SoundManager.I._bgm_Start.volume = _slider_MainBgm.value;
                break;
            case "UI_EffectBgm":
                SoundManager.I._sfx_Fire.volume = _slider_EffectBgm.value;
                SoundManager.I._sfx_Rock.volume = _slider_EffectBgm.value;
                break;
            default:
                break;
        }
        SoundManager.I.SaveVolLevel(_slider_MainBgm.value, _slider_EffectBgm.value);
    }

    public void OnClickExit()
    {
        _ui_Exit.SetActive(true);
    }

    public void ExitYesOrNo(bool stat)
    {
        if(stat)
        {
            Application.Quit();
        }
        else
        {
            _ui_Exit.SetActive(false);
        }
    }
}
