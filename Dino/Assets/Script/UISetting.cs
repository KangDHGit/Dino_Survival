using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public static UISetting I;

    Slider _sliderMainBgm;
    Slider _sliderEffectBgm;

    GameObject _uiExit;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        if (transform.Find("UI_MainBgm/Slider").TryGetComponent(out _sliderMainBgm))
        {
            _sliderMainBgm.value = SoundManager.I._mainVol;
            Debug.Log("_silder_MainBgm Setting Complete");
        }
        if(transform.Find("UI_EffectBgm/Slider").TryGetComponent(out _sliderEffectBgm))
        {
            _sliderEffectBgm.value = SoundManager.I._effectVol;
        }
        _uiExit = transform.Find("UI_Exit").gameObject;
        if (_uiExit != null)
            _uiExit.SetActive(false);
    }

    public void MoveSilder(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "UI_MainBgm":
                SoundManager.I.BgmIntro.volume = _sliderMainBgm.value;
                SoundManager.I.BgmStart.volume = _sliderMainBgm.value;
                break;
            case "UI_EffectBgm":
                SoundManager.I.SfxFire.volume = _sliderEffectBgm.value;
                SoundManager.I.SfxRock.volume = _sliderEffectBgm.value;
                break;
            default:
                break;
        }
        SoundManager.I.SaveVolLevel(_sliderMainBgm.value, _sliderEffectBgm.value);
    }

    public void OnClickExit()
    {
        _uiExit.SetActive(true);
    }

    public void ExitYesOrNo(bool stat)
    {
        if(stat)
        {
            Application.Quit();
        }
        else
        {
            _uiExit.SetActive(false);
        }
    }
}
