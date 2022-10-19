using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager I;

    public Camera _camera;

    double _defaultAspectRatio = 1.777777777777778;
    double _currentAspectRatio;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _currentAspectRatio = (double)Screen.height / (double)Screen.width;

        _camera.orthographicSize = (float)(_currentAspectRatio / _defaultAspectRatio * 5);
    }
}
