using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    public AudioSource _bgm_Intro;
    public AudioSource _bgm_Start;
    public AudioSource _sfx_Rock;
    public AudioSource _sfx_Fire;

    float _initMainVol = 0.6f;
    public float _mainVol;
    float _initEffectVol = 1.0f;
    public float _effectVol;

    const string MAIN_VOL_LEVEL = "user_setting_main_vol";
    const string SFX_VOL_LEVEL = "user_setting_sfx_vol";

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        LoadVolLevel();
        if (!transform.Find("Bgm_Intro").TryGetComponent(out _bgm_Intro))
            Debug.LogError("_bgm_Intro is Null");
        else
            _bgm_Intro.volume = _mainVol;

        if (!transform.Find("Bgm_Start").TryGetComponent(out _bgm_Start))
            Debug.LogError("_bgm_Start is Null");
        else
            _bgm_Start.volume = _mainVol;

        if (!RockManager.I._rockTemplate.TryGetComponent(out _sfx_Rock))
            Debug.LogError("_sfx_Rock is Null");
        else
            _sfx_Rock.volume = _effectVol;

        if (!FireManager.I._fireTemplate.transform.Find("Fire").TryGetComponent(out _sfx_Fire))
            Debug.LogError("_sfx_Fire");
        else
            _sfx_Fire.volume = _effectVol;
    }

    public void LoadVolLevel()
    {
        if (PlayerPrefs.HasKey(MAIN_VOL_LEVEL))
            _mainVol = PlayerPrefs.GetFloat(MAIN_VOL_LEVEL);
        else
            _mainVol = _initMainVol;
        if (PlayerPrefs.HasKey(SFX_VOL_LEVEL))
            _effectVol = PlayerPrefs.GetFloat(SFX_VOL_LEVEL);
        else
            _effectVol = _initEffectVol;
    }
    public void SaveVolLevel(float mainVol, float sfxVol)
    {
        PlayerPrefs.SetFloat(MAIN_VOL_LEVEL, mainVol);
        PlayerPrefs.SetFloat(SFX_VOL_LEVEL, sfxVol);
    }
}
