using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PropertyManager : Singleton<PropertyManager> {

    protected PropertyManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    [Header("Status")]
    public int Population = 10;
    public float Happiness = 100f;
    [Space(10)]
    public int Gold = 1000;
    public int GoldNext = 0;
    public int Food = 1000;
    public int FoodNext = 0;
    public int Building = 1000;
    public int BuildingNext = 0;

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

    [Header("Others")]
    [SerializeField] private LineManager lineManager;
    public List<Property> Propriedades = new List<Property>();



    //private string filepath;

    private void Awake()
    {
        //carregar coisas
        //filepath = Application.persistentDataPath + "/properties.sav";
    }

    private void Start()
    {
        Propriedades = new List<Property>(GetComponentsInChildren<Property>());
       
        lineManager.BuildLines(Propriedades);

        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;
        OnAfterDayEnd();
        //LineManager.SetActive(true); //ensure that it will only trace lines after fill up properties list
    }

    private void OnAfterDayEnd()
    {
        GoldNext = 0;
        FoodNext = 0;
        BuildingNext = 0;
        foreach(Property p in Propriedades)
        {
            if (!p.dominated) continue;
            switch (p.level)
            {
                case Level.Level1:
                    GoldNext += p.goldLevel1;
                    FoodNext += p.foodLevel1;
                    BuildingNext += p.buildingLevel1;
                    break;
                case Level.Level2:
                    GoldNext += p.goldLevel2;
                    FoodNext += p.foodLevel2;
                    BuildingNext += p.buildingLevel2;
                    break;
                case Level.Level3:
                    GoldNext += p.goldLevel3;
                    FoodNext += p.foodLevel3;
                    BuildingNext += p.buildingLevel3;
                    break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        TimerPanel.OnAfterDayEnd -= OnAfterDayEnd;
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
