using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    public AudioSource _bgm_Intro;
    public AudioSource _bgm_Start;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        if (!transform.Find("Bgm_Intro").TryGetComponent(out _bgm_Intro))
            Debug.LogError("_bgm_Intro is Null");
        if (!transform.Find("Bgm_Start").TryGetComponent(out _bgm_Start))
            Debug.LogError("_bgm_Start is Null");
    }
}
