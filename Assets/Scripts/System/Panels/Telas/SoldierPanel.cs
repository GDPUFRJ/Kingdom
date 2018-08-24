using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPanel : AMainPanel
{
    public GameObject CanvasBattle;

    private void Start()
    {
        CanvasBattle.SetActive(false);
    }

    public override void PrepareContent()
    {
        print("PREPARING");
        TimerPanel.SetPause(true);
        CanvasBattle.SetActive(true);
        
    }

    public override void UnprepareContent()
    {
        CanvasBattle.SetActive(false);
        TimerPanel.SetPause(false);
    }
}