using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class GameManager:Singleton < GameManager >  {

    protected GameManager() {}

    [Header("Status")]
    public int Population = 10; 
    public float Happiness = 100f; 
    public bool CanBattle = true; 
    [Space(10)]
    public int Gold = 1000; 
    public int GoldNext = 0;
    public int GoldNextEventModifier = 0;
    [Space(10)]
    public int Food = 1000; 
    public int FoodNext = 0;
    public int FoodNextEventModifier = 0;
    [Space(10)]
    public int Building = 1000; 
    public int BuildingNext = 0;
    public int BuildingNextEventModifier = 0;

    [Space(10)]
    public Queue<Property> BattleQueue = new Queue<Property>();

    private HudInfoManager hud; 

    [Header("Prefabs")]
    public GameObject happeningWindowPrefab; 
    public GameObject answerPrefab;
    public GameObject newDayPrefab;
    public GameObject battleWindow;
    public Transform canvasRoot;
    [Space(10)]
    public GameObject CanvasBattle;
    public GameObject ArrowPrefab;
    public GameObject BattlePrefab;
    public GameObject AbortPrefab;
    public GameObject NumSoldier;

    // Use this for initialization
    void Start() {
        TimerPanel.OnDayEnd += OnDayEnd;
        TimerPanel.OnBattleTime += TimerPanel_OnBattleTime;
        OnDayEnd(); 
        hud = FindObjectOfType < HudInfoManager > (); 
    }

    private void TimerPanel_OnBattleTime()
    {
        Property p;
        BattleWindow bw = battleWindow.GetComponent<BattleWindow>();

        if (BattleQueue.Count == 0) return;
        //COMO NAO TEM COMO AGUARDAR O FIM DA CORROTINA, VOU LIMITAR A UMA BATALHA POR DIA
        //while(BattleQueue.Count > 0)
        //{
            p = BattleQueue.Dequeue();
            if(p.soldiers == 0)
            {
                p.SetDominated(!p.dominated);
                p.soldiers = p.EnemySoldiers;
                //DEVERIA TER UMA ANIMACAO DE BATALHA, CASO NAO HAJA SOLDADOS?
                //continue;
            }
            bw.Show(p.EnemySoldiers, p.soldiers);
            if(p.EnemySoldiers > p.soldiers)
            {
                p.SetDominated(!p.dominated);
                p.soldiers = p.EnemySoldiers - p.soldiers;
            }
            else
            {
                p.soldiers = p.soldiers - p.EnemySoldiers;
            }
        p.EnemySoldiers = 0;
        //}
    }


    // Update is called once per frame
    void Update() {

    }

    private void OnDayEnd() {
        GoldNext = 0; 
        FoodNext = 0; 
        BuildingNext = 0; 
        int DominatedProperties = 0; 
        foreach (Property p in PropertyManager.Instance.Propriedades) {
            if (p.dominated)
                DominatedProperties++; 
            else
                continue; 

            switch (p.level) {
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
    public void UpdateComsumption() {
        OnDayEnd(); 
        hud.UpdateHUD(); 
    }

    public bool ConsumeItens(Property.UpgradeInformations upgradeInformations) {
        if (upgradeInformations == null)
            return false; 

        if (upgradeInformations.Gold <= this.Gold && 
            upgradeInformations.Building <= this.Building && 
            upgradeInformations.Food <= this.Food) {
            Gold -= upgradeInformations.Gold; 
            Building -= upgradeInformations.Building; 
            Food -= upgradeInformations.Food; 
            hud.UpdateHUD(); 
            return true; 
        }
        return false; 
    }

    private void OnApplicationQuit() {
        TimerPanel.OnDayEnd -= OnDayEnd; 
    }

    void GameWon() {
        Debug.Log("GANHOU!"); 
    }
}
