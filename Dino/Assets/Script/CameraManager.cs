using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager I;

    public Camera _camera;

    double _defaultAspectRatio = 1.777777777777778;
    double _currentAspectRatio;

    float _ratioValue; public float RatioValue { get { return _ratioValue; } }

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _currentAspectRatio = (double)Screen.height / (double)Screen.width;

        _ratioValue = (float)(_currentAspectRatio / _defaultAspectRatio);

        _camera.orthographicSize = _ratioValue * 5;
    }
}
