using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPanel : AMainPanel {
    [SerializeField] private GameObject dateBoxPrefab;
    [SerializeField] private GameObject timeBoxPrefab;

    public override void PrepareContent()
    {
        print("PREPARING");
    }
}
