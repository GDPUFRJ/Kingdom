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

    [HideInInspector]public List<Property> Propriedades = new List<Property>();

    //private string filepath;

    private void Awake()
    {
        //carregar coisas
        //filepath = Application.persistentDataPath + "/properties.sav";
    }

    private void Start()
    {
        Propriedades = new List<Property>(GetComponentsInChildren<Property>());
    }

    private void OnApplicationQuit()
    {

        //salvar as coisas
        //FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        //StreamWriter sw 
        //Propriedades = new List<Property>(GetComponentsInChildren<Property>());
        //foreach(Property prop in Propriedades)
        //{
        //ToDo
        //}
    }
}
