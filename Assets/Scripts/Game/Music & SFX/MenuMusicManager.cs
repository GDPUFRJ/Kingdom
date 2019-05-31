using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private void Awake()
    {
        FMODPlayer.Instance.Play("menuBG");
    }
}
