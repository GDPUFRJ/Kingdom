using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class GameManager:Singleton < GameManager >  {

    protected GameManager() {}

    [Header("Status")]
    public int Population = 10; 
    public float Happiness = 100f; 
    public bool CanBattle = true;
    public int PopulationNextEventModifier = 0;
    public float HappinessNextEventModifier = 0f;
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
    public BattleManager battleManager;

    [Header("Prefabs")]
    public GameObject happeningWindowPrefab; 
    public GameObject answerPrefab;
    public GameObject newDayPrefab;
    public GameObject battleWindow;
    [Space(10)]
    public GameObject ArrowPrefab;
    public GameObject NumSoldier;
    public GameObject EditButtons;
    [Header("Canvas")]
    public Transform CanvasHUD;
    public GameObject CanvasBattle;

    // Use this for initialization
    void Start() {
        battleManager = FindObjectOfType<BattleManager>();
        TimerPanel.OnDayEnd += OnDayEnd;
        TimerPanel.OnBattleTime += TimerPanel_OnBattleTime;
        OnDayEnd(); 
        hud = FindObjectOfType < HudInfoManager > ();

        if (!SaveSystem.newGame)
        {
            StartCoroutine(LoadGameCoroutine());
        }
    }

    private void TimerPanel_OnBattleTime()
    {
        battleManager.BeginBattles();
    }

    private void OnDayEnd() {
        GoldNext = 0; 
        FoodNext = 0; 
        BuildingNext = 0; 
        int mainPropertiesDominated = 0; 
        foreach (Property p in PropertyManager.Instance.Propriedades) {
            if (p.dominated && p.mainProperty)
                mainPropertiesDominated++;
            //else
            //continue; 

            if (p.dominated)
            {
                switch (p.Level)
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

        if (mainPropertiesDominated == PropertyManager.Instance.MainProperties)
            GameWon(); 
    }

    //called only when a new property is added or removed
    public void UpdateComsumption() {
        OnDayEnd(); 
        hud.UpdateHUD(); 
    }

    public bool ConsumeItens(UpgradeInformations upgradeInformations) {
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
        //ToDo
        print("You won the game!");
        Time.timeScale = 0f;
    }

    public void GameLost()
    {
        //ToDo
        print("You lost the game!");
        Time.timeScale = 0f;
    }

    private IEnumerator LoadGameCoroutine()
    {
        // Waits for the PropertyManager to finish setting up
        TimerPanel.SetPause(true);
        while(!PropertyManager.Instance.HasFinishedSettingUp)
        {
            yield return null;
        }

        // Loads game
        SaveData saveData = SaveSystem.LoadGame();

        // Overwrites saved game state over current game state
        Population = saveData.population;
        Happiness = saveData.happiness;
        Gold = saveData.gold;
        Food = saveData.food;
        Building = saveData.building;
        FindObjectOfType<TimerPanel>().SetCurrentDay(saveData.day);
        StartingKingdomController.Instance.PlayerKingdom = saveData.playerKingdom;
        Camera.main.transform.position = new Vector3(saveData.cameraX, saveData.cameraY, Camera.main.transform.position.z);
        foreach (PropertySaveData pSaveData in saveData.properties)
        {
            PropertyManager.Instance.Propriedades[pSaveData.index].SetDominated(pSaveData.dominated, false);
            PropertyManager.Instance.Propriedades[pSaveData.index].SetSoldiers(SoldierType.InProperty, pSaveData.soldiers);
            PropertyManager.Instance.Propriedades[pSaveData.index].Level = pSaveData.level;
            PropertyManager.Instance.Propriedades[pSaveData.index].UpdateSprite(PropertyManager.Instance.Propriedades[pSaveData.index]);
            PropertyManager.Instance.Propriedades[pSaveData.index].UpdateSoldierInfo();
        }
        if (saveData.activeEvents != null)
        {
            KEventManager.Instance.ClearActiveEvents();
            foreach (EventSaveData eSaveData in saveData.activeEvents)
            {
                KEvent kevt = ScriptableObject.CreateInstance<KEvent>();
                kevt.ExhibitionName = eSaveData.ExhibitionName;
                kevt.InternalName = eSaveData.InternalName;
                kevt.Description = eSaveData.Description;
                kevt.Duration = eSaveData.Duration;
                kevt.LeftDuration = eSaveData.LeftDuration;
                kevt.ActiveIntensity = eSaveData.ActiveIntensity;
                kevt.mode = eSaveData.mode;
                kevt.battle = eSaveData.battle;
                kevt.chance = eSaveData.chance;
                kevt.PercentGoldLight = eSaveData.PercentGoldLight;
                kevt.PercentFoodLight = eSaveData.PercentFoodLight;
                kevt.PercentBuildingLight = eSaveData.PercentBuildingLight;
                kevt.PercentPeopleLight = eSaveData.PercentPeopleLight;
                kevt.PercentHappinessLight = eSaveData.PercentHappinessLight;
                kevt.PercentGoldMedium = eSaveData.PercentGoldMedium;
                kevt.PercentFoodMedium = eSaveData.PercentFoodMedium;
                kevt.PercentBuildingMedium = eSaveData.PercentBuildingMedium;
                kevt.PercentPeopleMedium = eSaveData.PercentPeopleMedium;
                kevt.PercentHappinessMedium = eSaveData.PercentHappinessMedium;
                kevt.PercentGoldHeavy = eSaveData.PercentGoldHeavy;
                kevt.PercentFoodHeavy = eSaveData.PercentFoodHeavy;
                kevt.PercentBuildingHeavy = eSaveData.PercentBuildingHeavy;
                kevt.PercentPeopleHeavy = eSaveData.PercentPeopleHeavy;
                kevt.PercentHappinessHeavy = eSaveData.PercentHappinessHeavy;
                kevt.AbsoluteGoldLight = eSaveData.AbsoluteGoldLight;
                kevt.AbsoluteFoodLight = eSaveData.AbsoluteFoodLight;
                kevt.AbsoluteBuildingLight = eSaveData.AbsoluteBuildingLight;
                kevt.AbsoluteGoldMedium = eSaveData.AbsoluteGoldMedium;
                kevt.AbsoluteFoodMedium = eSaveData.AbsoluteFoodMedium;
                kevt.AbsoluteBuildingMedium = eSaveData.AbsoluteBuildingMedium;
                kevt.AbsoluteGoldHeavy = eSaveData.AbsoluteGoldHeavy;
                kevt.AbsoluteFoodHeavy = eSaveData.AbsoluteFoodHeavy;
                kevt.AbsoluteBuildingHeavy = eSaveData.AbsoluteBuildingHeavy;
                kevt.showInInspector = eSaveData.showInInspector;
                KEventManager.Instance.AddActiveEvent(kevt);
            }
        }

        hud.UpdateHUD();
        TimerPanel.SetPause(false);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }
}
