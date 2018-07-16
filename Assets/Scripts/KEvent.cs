using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEvent : ScriptableObject
{

    public string Name = "New Event";
    public string Description = "Describe it here";

    public int Duration = 1;
    public int LeftDuration = 1;

    public enum Intensity { light = 0, medium = 1, heavy = 2 }
    public Intensity intensity = Intensity.light;
    public Intensity ActiveIntensity = Intensity.light;

    public enum Mode { UsePercentage = 0, UseAbsolute = 1}
    public Mode mode = Mode.UsePercentage;

    public enum Battle { NotAllowed = 0, Allowed = 1}
    public Battle battle = Battle.Allowed;

    public enum Chance { Manual = 0, MuitoRaro = 1, Raro = 2, Normal = 3, Comum = 4, MuitoComum = 5 }
    public Chance chance = Chance.Normal;

    public int PercentGoldLight = 0;
    public int PercentFoodLight = 0;
    public int PercentBuildingLight = 0;
    public int PercentPeopleLight = 0;

    public int PercentGoldMedium = 0;
    public int PercentFoodMedium = 0;
    public int PercentBuildingMedium = 0;
    public int PercentPeopleMedium = 0;

    public int PercentGoldHeavy = 0;
    public int PercentFoodHeavy = 0;
    public int PercentBuildingHeavy = 0;
    public int PercentPeopleHeavy = 0;

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





}
