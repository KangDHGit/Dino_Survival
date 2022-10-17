using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public static UI_Setting I;

    public Slider _slider_MainBgm;

    public Slider _slider_EffectBgm;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        if (transform.Find("UI_MainBgm/Slider").TryGetComponent(out _slider_MainBgm))
        {
            _slider_MainBgm.value = SoundManager.I._mainVolume;
            Debug.Log("_silder_MainBgm Setting Complete");
        }
    }

    public void MoveSilder(GameObject gameObject)
    {
        switch (gameObject.name)
        {
            case "UI_MainBgm":
                SoundManager.I._bgm_Intro.volume = _slider_MainBgm.value;
                SoundManager.I._bgm_Start.volume = _slider_MainBgm.value;
                break;
            default:
                break;
        }
    }
}
