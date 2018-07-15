using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPanel : AMainPanel
{
    [SerializeField] private GameObject eventBoxPrefab;

    public override void PrepareContent()
    {
        print("PREPARING");
    }
}
