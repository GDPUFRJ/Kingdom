using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvasionBehaviour : MonoBehaviour
{
    private Property property;

    private void Start()
    {
        property = GetComponent<Property>();
        TimerPanel.OnDayEnd += OnDayEnd;
    }

    private void OnDayEnd()
    {
        AttemptInvasion();
    }

    private void AttemptInvasion()
    {
        if (!property.dominated && HasDominatedNeighbor() && property.GetSoldiers(SoldierType.Enemy) == 0 && Random.Range(0f, 1f) < PropertyManager.Instance.invasionChancePerProperty && property.GetSoldiers(SoldierType.InProperty) > 0)
        {
            PerformInvasion();
        }
    }

    private bool HasDominatedNeighbor()
    {
        foreach (var neighbor in property.Neighbors)
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
        foreach (var neighbor in property.Neighbors)
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
        int attackingSoldiers = property.GetSoldiers(SoldierType.InProperty);
        target.AddSoldiers(SoldierType.Enemy, attackingSoldiers, new BattleInformation(property.kingdom, target.kingdom, attackingSoldiers, target.GetSoldiers(SoldierType.InProperty)));
        property.AddSoldiers(SoldierType.ToGetOut, attackingSoldiers);
    }
}
