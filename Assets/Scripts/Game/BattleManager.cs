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

    private bool shouldPlayBattles = true;

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
        if (shouldPlayBattles)
            StartCoroutine(BattleRoutine());
    }
    public void StopBattles()
    {
        shouldPlayBattles = false;
        StopAllCoroutines();
    }
    public void OnIndividualBattleEnded()
    {
        //Chamado quando cada batalha individual termina
        TimerPanel.OnIndividualBattleEnd();
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
            OnIndividualBattleEnded();
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
        int attackerBattlePoints;
        int defenderBattlePoints;
        int remainingAttackerSoldiers;
        int remainingDefenderSoldiers;
        SimulateBattle(attackerSoldiers, defenderSoldiers, out remainingAttackerSoldiers, out remainingDefenderSoldiers, out attackerBattlePoints, out defenderBattlePoints);

        battleWindow.Show(attackerSoldiers, defenderSoldiers, attackerBattlePoints, defenderBattlePoints, battleInformation, remainingAttackerSoldiers, remainingDefenderSoldiers,
                          (battleInformation.attackingKingdom == StartingKingdomController.Instance.PlayerKingdom));

        if (InvaderIsTheWinner(remainingAttackerSoldiers, remainingDefenderSoldiers))
        {
            if (property.dominated)
                numberOfBattlesLostByThePlayerLastTurn++;
            else
                numberOfBattlesWonByThePlayerLastTurn++;

            property.SetDominated(!property.dominated, false);
            property.SetSoldiers(SoldierType.InProperty, remainingAttackerSoldiers);
            if (!property.mainProperty) property.WeakenCastle();
            property.ChangeKingdom(battleInformation.attackingKingdom);
        }
        else
        {
            if (!property.dominated)
                numberOfBattlesLostByThePlayerLastTurn++;
            else
                numberOfBattlesWonByThePlayerLastTurn++;

            property.SetSoldiers(SoldierType.InProperty, remainingDefenderSoldiers);
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

    private void SimulateBattle(int attackerSoldiers, int defenderSoldiers,
                                out int remainingAttackerSoldiers, out int remainingDefenderSoldiers,
                                out int attackerBattlePoints, out int defenderBattlerPoints)
    {
        attackerBattlePoints = 0;
        defenderBattlerPoints = 0;
        remainingAttackerSoldiers = attackerSoldiers;
        remainingDefenderSoldiers = defenderSoldiers;

        if (defenderSoldiers == 0)
        {
            attackerBattlePoints = GetBattlePoints(attackerSoldiers);
            defenderBattlerPoints = 0;
            return;
        }

        for (int i = 1; i <= attackerSoldiers || i <= defenderSoldiers; i++)
        {
            int currentAttackerBattlePoints;
            int currentDefenderBattlePoints;

            if (i > attackerSoldiers) currentAttackerBattlePoints = 0;
            else currentAttackerBattlePoints = GetBattlePoints(1);

            if (i > defenderSoldiers) currentDefenderBattlePoints = 0;
            else currentDefenderBattlePoints = GetBattlePoints(1);

            if (currentAttackerBattlePoints > currentDefenderBattlePoints && remainingDefenderSoldiers > 0)
            {
                remainingDefenderSoldiers--;
            }
            if (currentAttackerBattlePoints <= currentDefenderBattlePoints && remainingAttackerSoldiers > 0)
            {
                remainingAttackerSoldiers--;
            }
            attackerBattlePoints += currentAttackerBattlePoints;
            defenderBattlerPoints += currentDefenderBattlePoints;
        }

        if (remainingAttackerSoldiers > 0 && remainingDefenderSoldiers > 0)
        {
            if (attackerBattlePoints > defenderBattlerPoints)
            {
                remainingDefenderSoldiers = 0;
            }
            else
            {
                remainingAttackerSoldiers = 0;
            }
        }
    }
    
    //private bool PropertyIsEmpty(Property p) { return p.GetSoldiers(SoldierType.InProperty) == 0; }

    private bool InvaderIsTheWinner(int remainingAttackerSoldiers, int remainingDefenderSoldiers)
    {
        if (remainingAttackerSoldiers > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
