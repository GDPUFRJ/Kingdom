using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KEvent : ScriptableObject
{

    public string ExhibitionName = "New Event";
    public string InternalName = "New Event";
    public string Description = "Describe it here";

    public int Duration = 1;
    public int LeftDuration = 1;

    //public Intensity intensity = Intensity.light;
    public Intensity ActiveIntensity = Intensity.light;
    public Mode mode = Mode.UsePercentage;
    public Battle battle = Battle.Allowed;
    public Chance chance = Chance.Normal;

    public int PercentGoldLight = 0;
    public int PercentFoodLight = 0;
    public int PercentBuildingLight = 0;
    public int PercentPeopleLight = 0;
    public int PercentHappinessLight = 0;

    public int PercentGoldMedium = 0;
    public int PercentFoodMedium = 0;
    public int PercentBuildingMedium = 0;
    public int PercentPeopleMedium = 0;
    public int PercentHappinessMedium = 0;

    public int PercentGoldHeavy = 0;
    public int PercentFoodHeavy = 0;
    public int PercentBuildingHeavy = 0;
    public int PercentPeopleHeavy = 0;
    public int PercentHappinessHeavy = 0;

    public int AbsoluteGoldLight = 0;
    public int AbsoluteFoodLight = 0;
    public int AbsoluteBuildingLight = 0;

    public int AbsoluteGoldMedium = 0;
    public int AbsoluteFoodMedium = 0;
    public int AbsoluteBuildingMedium = 0;

    public int AbsoluteGoldHeavy = 0;
    public int AbsoluteFoodHeavy = 0;
    public int AbsoluteBuildingHeavy = 0;

    public bool showInInspector = true;

    public EventInfo GetInfo()
    {
        return new EventInfo(this.ExhibitionName, this.InternalName, this.Description, this.LeftDuration, this.battle);
    }

    public int GetNextResource(Resource resource)
    {
        switch (resource)
        {
            case Resource.Gold:
                switch (ActiveIntensity)
                {
                    case Intensity.light:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteGoldLight;
                        else
                            return ((PercentGoldLight * GameManager.Instance.GoldNext) / 100);

                    case Intensity.medium:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteGoldMedium;
                        else
                            return ((PercentGoldMedium * GameManager.Instance.GoldNext) / 100);

                    case Intensity.heavy:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteGoldHeavy;
                        else
                            return ((PercentGoldHeavy * GameManager.Instance.GoldNext) / 100);
                }
                break;
            case Resource.Building:
                switch (ActiveIntensity)
                {
                    case Intensity.light:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteBuildingLight;
                        else
                            return ((PercentBuildingLight * GameManager.Instance.BuildingNext) / 100);

                    case Intensity.medium:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteBuildingMedium;
                        else
                            return ((PercentBuildingMedium * GameManager.Instance.BuildingNext) / 100);

                    case Intensity.heavy:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteBuildingHeavy;
                        else
                            return ((PercentBuildingHeavy * GameManager.Instance.BuildingNext) / 100);
                }
                break;
            case Resource.Food:
                switch (ActiveIntensity)
                {
                    case Intensity.light:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteFoodLight;
                        else
                            return ((PercentFoodLight * GameManager.Instance.FoodNext) / 100);

                    case Intensity.medium:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteFoodMedium;
                        else
                            return ((PercentFoodMedium * GameManager.Instance.FoodNext) / 100);

                    case Intensity.heavy:
                        if (mode == Mode.UseAbsolute)
                            return AbsoluteFoodHeavy;
                        else
                            return ((PercentFoodHeavy * GameManager.Instance.FoodNext) / 100);
                }
                break;
        }
        return 0;
    }

    public int GetPopulationModifier()
    {
        if (mode == Mode.UseAbsolute)
            return 0;

        switch (ActiveIntensity)
        {
            case Intensity.light:
                return (PercentPeopleLight * GameManager.Instance.Population) / 100;
            case Intensity.medium:
                return (PercentPeopleMedium * GameManager.Instance.Population) / 100;
            case Intensity.heavy:
                return (PercentPeopleHeavy * GameManager.Instance.Population) / 100;
        }

        return 0;
    }

    public float GetHappinessModifier()
    {
        if (mode == Mode.UseAbsolute)
            return 0;

        switch (ActiveIntensity)
        {
            case Intensity.light:
                return (PercentHappinessLight / 100f) * GameManager.Instance.Happiness;
            case Intensity.medium:
                return (PercentHappinessMedium / 100f) * GameManager.Instance.Happiness;
            case Intensity.heavy:
                return (PercentHappinessHeavy / 100f) * GameManager.Instance.Happiness;
        }

        return 0;
    }
}
public class EventInfo
{
    public string exhibitionName;
    public string internalName;
    public string description;
    public int remainingDays;

    public int BuildingToAddOrRemove;
    public int FoodToAddOrRemove;
    public int GoldToAddOrRemove;
    public Battle BattleEnabled;

    public EventInfo(string exhibitionName, string internalName, string description, 
                     int remainingDays, Battle BattleEnabled)
    {
        this.exhibitionName = exhibitionName;
        this.internalName = internalName;
        this.description = description;
        this.remainingDays = remainingDays;
        this.BattleEnabled = BattleEnabled;
    }
}
