﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArrowType { Arrow, Battle, Abort, Disabled }

public enum Chance { Manual = 0, MuitoRaro = 1, Raro = 2, Normal = 3, Comum = 4, MuitoComum = 5 }
public enum Intensity { light = 0, medium = 1, heavy = 2 }
public enum Mode { UsePercentage = 0, UseAbsolute = 1 }
public enum Battle { NotAllowed = 0, Allowed = 1 }

public enum PropertyType { Castle, Mine, Village, Farm, Forest, Other, quarter }
public enum SoldierType { InProperty, Enemy, ToGetOut }

[System.Serializable]
public enum Level { Level1 = 1, Level2 = 2, Level3 = 3 }
public enum Resource { Gold, Building, Food }
[System.Serializable]
public enum Kingdom { Blue, Red, Purple, Green, Orange }

public struct Informations
{
    public Sprite sprite;
    public int Gold;
    public int Food;
    public int Building;
    public int Soldiers;
    public float happiness;
}

public class UpgradeInformations
{
    public int Gold;
    public int Food;
    public int Building;
}

public class BattleInformation
{
    public Kingdom attackingKingdom;
    public Kingdom defendingKingdom;
    public int attackingSoldiers;
    public int defendingSoldiers;

    public BattleInformation(Kingdom attackingKingdom, Kingdom defendingKingdom, int attackingSoldiers, int defendingSoldiers)
    {
        this.attackingKingdom = attackingKingdom;
        this.defendingKingdom = defendingKingdom;
        this.attackingSoldiers = attackingSoldiers;
        this.defendingSoldiers = defendingSoldiers;
    }
}

public class CommonDefinitions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class KingdomULRs
{
    private static string s_privacyPolicy = "https://kingdom-war-of-conq.flycricket.io/privacy.html";
    public static string PRIVACY_POLICY { get => s_privacyPolicy; }
}