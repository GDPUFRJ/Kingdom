using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    private List<Property> propertiesInBattle = new List<Property>();
    private List<BattleInformation> battleInformations = new List<BattleInformation>();
    private int numberOfBattlesLostByThePlayerLastTurn = 0;
    private int numberOfBattlesWonByThePlayerLastTurn = 0;
    public int NumberOfBattlesLostByThePlayerLastTurn { get { return numberOfBattlesLostByThePlayerLastTurn; } }
    public int NumberOfBattlesWonByThePlayerLastTurn { get { return numberOfBattlesWonByThePlayerLastTurn; } }
    [SerializeField] private BattleWindow battleWindow;
    [SerializeField] private int minDiceNumber = 1;
    [SerializeField] private int maxDiceNumber = 6;

    private void ResetBattleList()
    {
        propertiesInBattle = new List<Property>();
        battleInformations = new List<BattleInformation>();
    }
    public void AddBattleProperty(Property prop, BattleInformation attackInformation)
    {
        if (propertiesInBattle == null)
            ResetBattleList();

        propertiesInBattle.Add(prop);
        battleInformations.Add(attackInformation);
    }
    public void BeginBattles()
    {
        StartCoroutine(BattleRoutine());
    }
    public void OnBattlesEnd()
    {
        //Chamado quando terminam todas as batalhas
        TimerPanel.OnBattleEnd();
    }
    private IEnumerator BattleRoutine()
    {
        TimerPanel.SetPause(true);
        numberOfBattlesLostByThePlayerLastTurn = 0;
        numberOfBattlesWonByThePlayerLastTurn = 0;
        for(int i = 0; i < propertiesInBattle.Count; i++)
        {
            yield return IndividualBattle(propertiesInBattle[i], battleInformations[i]);
        }
        OnBattlesEnd();
        TimerPanel.SetPause(false);
        ResetBattleList();
    }
    private IEnumerator IndividualBattle(Property property, BattleInformation battleInformation)
    {
        var cam = Camera.main.GetComponent<CameraMovement>();
        yield return cam.FollowPosition(property.gameObject.transform.position);
        yield return cam.Zoom(true);

        int attackerSoldiers = battleInformation.attackingSoldiers;
        int defenderSoldiers = battleInformation.defendingSoldiers;
        int attackerBattlePoints = GetBattlePoints(attackerSoldiers);
        int defenderBattlePoints = GetBattlePoints(defenderSoldiers);

        //if (PropertyIsEmpty(property))
        //{
        //    property.SetDominated(!property.dominated, false);
        //    property.SetSoldiers(SoldierType.InProperty, property.GetSoldiers(SoldierType.Enemy));
        //    property.kingdom = battleInformation.attackingKingdom;
        //}
        battleWindow.Show(attackerSoldiers, defenderSoldiers, attackerBattlePoints, defenderBattlePoints, battleInformation);
        if (InvaderIsTheWinner(attackerBattlePoints, defenderBattlePoints))
        {
            if (property.dominated)
                numberOfBattlesLostByThePlayerLastTurn++;
            else
                numberOfBattlesWonByThePlayerLastTurn++;

            property.SetDominated(!property.dominated, false);
            property.SetSoldiers(SoldierType.InProperty, attackerSoldiers - defenderSoldiers);
            if (!property.mainProperty) property.WeakenCastle();
            property.ChangeKingdom(battleInformation.attackingKingdom);
        }
        else
        {
            if (!property.dominated)
                numberOfBattlesLostByThePlayerLastTurn++;
            else
                numberOfBattlesWonByThePlayerLastTurn++;

            property.SetSoldiers(SoldierType.InProperty, defenderSoldiers - attackerSoldiers);
        }
        property.SetSoldiers(SoldierType.Enemy, 0);
        yield return battleWindow.currentBattle;
        yield return cam.Zoom(false);
        property.UpdateSoldierInfo();
        property.UpdateSprite(property);
    }

    private int GetBattlePoints(int soldiers)
    {
        int battlePoints = 0;
        for (int i = 0; i < soldiers; i++)
        {
            battlePoints += Random.Range(minDiceNumber, maxDiceNumber + 1);
        }
        return battlePoints;
    }
    
    //private bool PropertyIsEmpty(Property p) { return p.GetSoldiers(SoldierType.InProperty) == 0; }

    private bool InvaderIsTheWinner(int attackerBattlePoints, int defenderBatterPoints)
    {
        return attackerBattlePoints > defenderBatterPoints;
    }
}
