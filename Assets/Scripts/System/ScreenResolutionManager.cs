using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutionManager : MonoBehaviour
{
    [SerializeField] private int desiredWidth = 720;
    [SerializeField] private int desiredHeight = 1280;
    [SerializeField] private FullScreenMode desiredFullScreenMode = FullScreenMode.Windowed;
    [SerializeField] private int preferredFreshRate = 60;

    void Start ()
    {
        Screen.SetResolution(desiredWidth, desiredHeight, desiredFullScreenMode, preferredFreshRate);
	}
}
