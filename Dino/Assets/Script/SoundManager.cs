using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    public AudioSource _bgm_Intro;
    public AudioSource _bgm_Start;
    float _initMainVolume = 0.6f;
    public float _mainVolume;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _mainVolume = _initMainVolume;
        if (!transform.Find("Bgm_Intro").TryGetComponent(out _bgm_Intro))
            Debug.LogError("_bgm_Intro is Null");
        else
            _bgm_Intro.volume = _mainVolume;

        if (!transform.Find("Bgm_Start").TryGetComponent(out _bgm_Start))
            Debug.LogError("_bgm_Start is Null");
        else
            _bgm_Start.volume = _mainVolume;
    }
}
