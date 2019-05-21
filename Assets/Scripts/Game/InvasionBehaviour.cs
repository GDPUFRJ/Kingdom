using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvasionBehaviour : MonoBehaviour
{
    private Property _thisProperty;

    private void Start()
    {
        _thisProperty = GetComponent<Property>();
        TimerPanel.OnDayEnd += OnDayEnd;
    }

    private void OnDayEnd()
    {
        AttemptInvasion();
    }
    
/// <summary>
/// Different kinds of properties have different invasion behaviours.
/// Barracks invade random neighbour enemy properties whatever the odds.
/// Other kinds of properties only invade if they have more soldiers than their enemy.
/// </summary>
    private void AttemptInvasion()
    {
        switch (_thisProperty.type)
        {
            case PropertyType.quarter:
                if (!_thisProperty.dominated &&
                    HasDominatedNeighbor() &&
                    _thisProperty.GetSoldiers(SoldierType.Enemy) == 0 &&
                    Random.Range(0f, 1f) < PropertyManager.Instance.invasionChancePerProperty &&
                    _thisProperty.GetSoldiers(SoldierType.InProperty) > 0)
                {
                    PerformInvasion();
                }
                break;
            default:
                if (!_thisProperty.dominated &&
                    HasDominatedNeighbor() &&
                    _thisProperty.GetSoldiers(SoldierType.Enemy) == 0 &&
                    Random.Range(0f, 1f) < PropertyManager.Instance.invasionChancePerProperty &&
                    _thisProperty.GetSoldiers(SoldierType.InProperty) > 0 &&
                    _thisProperty.GetSoldiers(SoldierType.InProperty) > (GetRandomDominatedNeighbor().GetSoldiers(SoldierType.InProperty) / 2))
                {
                    PerformInvasion();
                }
                break;
        }
    }

    private bool HasDominatedNeighbor()
    {
        foreach (var neighbor in _thisProperty.Neighbors)
        {
            if (neighbor.dominated && neighbor.GetSoldiers(SoldierType.ToGetOut) == 0)
            {
                return true;
            }
        }

        return false;
    }

    private Property GetRandomDominatedNeighbor()
    {
        List<Property> candidates = new List<Property>();
        foreach (var neighbor in _thisProperty.Neighbors)
        {
            if (neighbor.dominated && neighbor.GetSoldiers(SoldierType.ToGetOut) == 0)
            {
                candidates.Add(neighbor);
            }
        }

        return candidates[Random.Range(0, candidates.Count)];
    }

    private void PerformInvasion()
    {
        // Attacks a random dominated neighbor with every soldier possible
        var target = GetRandomDominatedNeighbor();
        int attackingSoldiers = _thisProperty.GetSoldiers(SoldierType.InProperty);
        target.AddSoldiers(SoldierType.Enemy, attackingSoldiers, new BattleInformation(_thisProperty.kingdom, target.kingdom, attackingSoldiers, target.GetSoldiers(SoldierType.InProperty)));
        _thisProperty.AddSoldiers(SoldierType.ToGetOut, attackingSoldiers);
    }
}
