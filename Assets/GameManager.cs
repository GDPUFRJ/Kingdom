using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    protected GameManager() { }

    [Header("Status")]
    public int Population = 10;
    public float Happiness = 100f;
    public bool CanBattle = true;
    [Space(10)]
    public int Gold = 1000;
    public int GoldNext = 0;
    public int Food = 1000;
    public int FoodNext = 0;
    public int Building = 1000;
    public int BuildingNext = 0;

    private HudInfoManager hud;

    // Use this for initialization
    void Start()
    {
        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;
        OnAfterDayEnd();
        hud = FindObjectOfType<HudInfoManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAfterDayEnd()
    {
        GoldNext = 0;
        FoodNext = 0;
        BuildingNext = 0;
        int DominatedProperties = 0;
        foreach (Property p in PropertyManager.Instance.Propriedades)
        {
            if (p.dominated)
                DominatedProperties++;
            else
                continue;

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

        if (DominatedProperties == PropertyManager.Instance.Propriedades.Count)
            GameWon();
    }

    //called only when a new property is added or removed
    public void UpdateComsumption()
    {
        OnAfterDayEnd();
        hud.UpdateHUD();
    }

    public bool ConsumeItens(int gold, int building, int food = 0)
    {
        if (gold <= this.Gold && building <= this.Building && food <= this.Food)
        {
            Gold -= gold;
            Building -= building;
            Food -= food;
            hud.UpdateHUD();
            return true;
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        TimerPanel.OnAfterDayEnd -= OnAfterDayEnd;
    }

    void GameWon()
    {
        Debug.Log("GANHOU!");
    }
}
