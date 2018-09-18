using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class PropertyManager : Singleton<PropertyManager> {

    protected PropertyManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    [Header("Sprites")]
    [Space(10)]
    [Header("Castle")]
    public Sprite CastleLevel1;
    public Sprite CastleLevel2;
    public Sprite CastleLevel3;

    [Header("quarter")]
    public Sprite quarterLevel1;
    public Sprite quarterLevel2;
    public Sprite quarterLevel3;

    [Header("Mine")]
    public Sprite MineLevel1;
    public Sprite MineLevel2;
    public Sprite MineLevel3;

    [Header("Village")]
    public Sprite VillageLevel1;
    public Sprite VillageLevel2;
    public Sprite VillageLevel3;

    [Header("Farm")]
    public Sprite FarmLevel1;
    public Sprite FarmLevel2;
    public Sprite FarmLevel3;

    [Header("Forest")]
    public Sprite ForestLevel1;
    public Sprite ForestLevel2;
    public Sprite ForestLevel3;

    [Header("Colors")]
    public Color DominatedLine;
    public Color NotDominatedLine;
    public Color NotDominatedProperty;

    [Header("Property Window")]
    public Transform canvasParent;
    public GameObject propertyWindowPrefab;

    [Header("Others")]
    public LineManager lineManager;

    [HideInInspector] public List<Property> Propriedades = new List<Property>();
    private int mainProperties = 0;
    [HideInInspector] public int MainProperties
    {
        private set { mainProperties = value; }
        get { return mainProperties; }
    }

    private void Start()
    {
        Propriedades = new List<Property>(GetComponentsInChildren<Property>());
        foreach(Property p in Propriedades)
        {
            if (p.mainProperty) MainProperties++;
        }
    }
}
