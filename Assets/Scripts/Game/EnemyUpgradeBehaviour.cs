using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUpgradeBehaviour : MonoBehaviour
{
    [SerializeField] private List<Property> myProperties;

    private List<Property> _myBarracks = new List<Property>();
    private List<Property> _myForests = new List<Property>();
    private List<Property> _myFarms = new List<Property>();
    private List<Property> _myCastles = new List<Property>();
    private List<Property> _myMines = new List<Property>();
    private List<Property> _myOtherProperties = new List<Property>();

    private void Start()
    {
        foreach (var property in myProperties)
        {
            switch (property.type)
            {
                case PropertyType.quarter:
                    _myBarracks.Add(property);
                    break;
                case PropertyType.Forest:
                    _myForests.Add(property);
                    break;
                case PropertyType.Farm:
                    _myFarms.Add(property);
                    break;
                case PropertyType.Castle:
                    _myCastles.Add(property);
                    break;
                case PropertyType.Mine:
                    _myMines.Add(property);
                    break;
                case PropertyType.Other:
                    _myOtherProperties.Add(property);
                    break;
            }
        }
    }

    public void OnPlayerUpgraded(Property propertyUpgraded)
    {
        switch (propertyUpgraded.type)
        {
            case PropertyType.quarter:
                UpgradeRandomProperty(_myBarracks);
                break;
            case PropertyType.Forest:
                UpgradeRandomProperty(_myForests);
                break;
            case PropertyType.Farm:
                UpgradeRandomProperty(_myFarms);
                break;
            case PropertyType.Castle:
                UpgradeRandomProperty(_myCastles);
                break;
            case PropertyType.Mine:
                UpgradeRandomProperty(_myMines);
                break;
            case PropertyType.Other:
                UpgradeRandomProperty(_myOtherProperties);
                break;
        }
    }

    private void UpgradeRandomProperty(List<Property> properties)
    {
        if (properties.Count > 0)
        {
            properties[Random.Range(0, properties.Count)].LevelUp(false);
        }
    }
}
