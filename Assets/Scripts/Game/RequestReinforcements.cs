using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RequestReinforcements : MonoBehaviour
{
    [SerializeField] private List<Property> neighborBarracks;
    [SerializeField] private int turnInterval = 2;

    private Property _thisCastle;
    private int _turnCounter;

    private void Start()
    {
        _thisCastle = GetComponent<Property>();
        TimerPanel.OnBattleEnded += OnDayStart;
        _turnCounter = 0;
    }

    /// <summary>
    /// Returns a list of neighbor barracks available with more soldiers than this castle 
    /// </summary>
    /// <returns></returns>
    private List<Property> GetNeighborBarracksThatCanHelp()
    {
        var mySoldiers = _thisCastle.GetSoldiers(SoldierType.InProperty);
        var barracks = new List<Property>();

        foreach (var neighborBarrack in neighborBarracks)
        {
            if (!neighborBarrack.dominated && neighborBarrack.GetSoldiers(SoldierType.InProperty) > mySoldiers)
                barracks.Add(neighborBarrack);
        }

        return barracks;
    }

    private void RequestSoldiers(List<Property> availableBarracksToHelp)
    {
        var mySoldiers = _thisCastle.GetSoldiers(SoldierType.InProperty);
        
        foreach (var barrack in availableBarracksToHelp)
        {
            var soldiersToBeTransferred = barrack.GetSoldiers(SoldierType.InProperty) - mySoldiers;
            barrack.RemoveSoldiers(SoldierType.InProperty, soldiersToBeTransferred);
            _thisCastle.AddSoldiers(SoldierType.InProperty, soldiersToBeTransferred);
        }
    }

    private void OnDayStart()
    {
        if (!_thisCastle.dominated && _turnCounter % turnInterval == 0)
        {
            RequestSoldiers(GetNeighborBarracksThatCanHelp());
        }

        _turnCounter++;
    }
}
