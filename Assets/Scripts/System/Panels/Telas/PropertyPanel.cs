using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyPanel : AMainPanel
{
    [SerializeField] private GameObject propertyBoxPrefab;

    public override void PrepareContent()
    {
        print("PREPARING");
    }
}
