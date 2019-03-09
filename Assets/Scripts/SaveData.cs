using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SaveData
{
    public int population;
    public float happiness;
    public int gold;
    public int food;
    public int building;
    public int day;
    public Kingdom playerKingdom;
    public PropertySaveData[] properties;
    public EventSaveData[] activeEvents;

    public SaveData(GameManager gameManager, TimerPanel timer, PropertyManager propertyManager, KEventManager eventManager, StartingKingdomController startingKingdomController)
    {
        population = gameManager.Population;
        happiness = gameManager.Happiness;
        gold = gameManager.Gold;
        food = gameManager.Food;
        building = gameManager.Building;
        day = timer.GetCurrentDay();
        playerKingdom = startingKingdomController.PlayerKingdom;
        properties = new PropertySaveData[propertyManager.Propriedades.Count];
        foreach (Property p in propertyManager.Propriedades)
        {
            properties[p.index] = new PropertySaveData(p.index, p.dominated, p.GetSoldiers(SoldierType.InProperty), p.Level);
        }
        if (eventManager.GetAllActiveEvents().Count > 0)
        {
            List<KEvent> list = eventManager.GetAllActiveEvents();
            activeEvents = new EventSaveData[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                activeEvents[i] = new EventSaveData(
                    list[i].ExhibitionName,
                    list[i].InternalName,
                    list[i].Description,
                    list[i].Duration,
                    list[i].LeftDuration,
                    list[i].ActiveIntensity,
                    list[i].mode,
                    list[i].battle,
                    list[i].chance,
                    list[i].PercentGoldLight,
                    list[i].PercentFoodLight,
                    list[i].PercentBuildingLight,
                    list[i].PercentPeopleLight,
                    list[i].PercentHappinessLight,
                    list[i].PercentGoldMedium,
                    list[i].PercentFoodMedium,
                    list[i].PercentBuildingMedium,
                    list[i].PercentPeopleMedium,
                    list[i].PercentHappinessMedium,
                    list[i].PercentGoldHeavy,
                    list[i].PercentFoodHeavy,
                    list[i].PercentBuildingHeavy,
                    list[i].PercentPeopleHeavy,
                    list[i].PercentHappinessHeavy,
                    list[i].AbsoluteGoldLight,
                    list[i].AbsoluteFoodLight,
                    list[i].AbsoluteBuildingLight,
                    list[i].AbsoluteGoldMedium,
                    list[i].AbsoluteFoodMedium,
                    list[i].AbsoluteBuildingMedium,
                    list[i].AbsoluteGoldHeavy,
                    list[i].AbsoluteFoodHeavy,
                    list[i].AbsoluteBuildingHeavy,
                    list[i].showInInspector);
            }
        }
        else
        {
            activeEvents = null;
        }
    }
}

[System.Serializable]
public class PropertySaveData
{
    public int index;
    public bool dominated;
    public int soldiers;
    public Level level;

    public PropertySaveData (int index, bool dominated, int soldiers, Level level)
    {
        this.index = index;
        this.dominated = dominated;
        this.soldiers = soldiers;
        this.level = level;
    }
}

[System.Serializable]
public class EventSaveData
{
    public string ExhibitionName;
    public string InternalName;
    public string Description;

    public int Duration;
    public int LeftDuration;

    public Intensity ActiveIntensity;
    public Mode mode;
    public Battle battle;
    public Chance chance;

    public int PercentGoldLight;
    public int PercentFoodLight;
    public int PercentBuildingLight;
    public int PercentPeopleLight;
    public int PercentHappinessLight;

    public int PercentGoldMedium;
    public int PercentFoodMedium;
    public int PercentBuildingMedium;
    public int PercentPeopleMedium;
    public int PercentHappinessMedium;

    public int PercentGoldHeavy;
    public int PercentFoodHeavy;
    public int PercentBuildingHeavy;
    public int PercentPeopleHeavy;
    public int PercentHappinessHeavy;

    public int AbsoluteGoldLight;
    public int AbsoluteFoodLight;
    public int AbsoluteBuildingLight;

    public int AbsoluteGoldMedium;
    public int AbsoluteFoodMedium;
    public int AbsoluteBuildingMedium;

    public int AbsoluteGoldHeavy;
    public int AbsoluteFoodHeavy;
    public int AbsoluteBuildingHeavy;

    public bool showInInspector;

    public EventSaveData (
        string ExhibitionName,
        string InternalName,
        string Description,
        int Duration,
        int LeftDuration,
        Intensity ActiveIntensity,
        Mode mode,
        Battle battle,
        Chance chance,
        int PercentGoldLight,
        int PercentFoodLight,
        int PercentBuildingLight,
        int PercentPeopleLight,
        int PercentHappinessLight,
        int PercentGoldMedium,
        int PercentFoodMedium,
        int PercentBuildingMedium,
        int PercentPeopleMedium,
        int PercentHappinessMedium,
        int PercentGoldHeavy,
        int PercentFoodHeavy,
        int PercentBuildingHeavy,
        int PercentPeopleHeavy,
        int PercentHappinessHeavy,
        int AbsoluteGoldLight,
        int AbsoluteFoodLight,
        int AbsoluteBuildingLight,
        int AbsoluteGoldMedium,
        int AbsoluteFoodMedium,
        int AbsoluteBuildingMedium,
        int AbsoluteGoldHeavy,
        int AbsoluteFoodHeavy,
        int AbsoluteBuildingHeavy,
        bool showInInspector)
    {
        this.ExhibitionName = ExhibitionName;
        this.InternalName = InternalName;
        this.Description = Description;
        this.Duration = Duration;
        this.LeftDuration = LeftDuration;
        this.ActiveIntensity = ActiveIntensity;
        this.mode = mode;
        this.battle = battle;
        this.chance = chance;
        this.PercentGoldLight = PercentGoldLight;
        this.PercentFoodLight = PercentFoodLight;
        this.PercentBuildingLight = PercentBuildingLight;
        this.PercentPeopleLight = PercentPeopleLight;
        this.PercentHappinessLight = PercentHappinessLight;
        this.PercentGoldMedium = PercentGoldMedium;
        this.PercentFoodMedium = PercentFoodMedium;
        this.PercentBuildingMedium = PercentBuildingMedium;
        this.PercentPeopleMedium = PercentPeopleMedium;
        this.PercentHappinessMedium = PercentHappinessMedium;
        this.PercentGoldHeavy = PercentGoldHeavy;
        this.PercentFoodHeavy = PercentFoodHeavy;
        this.PercentBuildingHeavy = PercentBuildingHeavy;
        this.PercentPeopleHeavy = PercentPeopleHeavy;
        this.PercentHappinessHeavy = PercentHappinessHeavy;
        this.AbsoluteGoldLight = AbsoluteGoldLight;
        this.AbsoluteFoodLight = AbsoluteFoodLight;
        this.AbsoluteBuildingLight = AbsoluteBuildingLight;
        this.AbsoluteGoldMedium = AbsoluteGoldMedium;
        this.AbsoluteFoodMedium = AbsoluteFoodMedium;
        this.AbsoluteBuildingMedium = AbsoluteBuildingMedium;
        this.AbsoluteGoldHeavy = AbsoluteGoldHeavy;
        this.AbsoluteFoodHeavy = AbsoluteFoodHeavy;
        this.AbsoluteBuildingHeavy = AbsoluteBuildingHeavy;
        this.showInInspector = showInInspector;
    }
}