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

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _mainVol = _initMainVol;
        _effectVol = _initEffectVol;
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
}
