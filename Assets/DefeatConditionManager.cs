using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatConditionManager : Singleton<DefeatConditionManager>
{
    // A condição de derrota é:
    //
    // O jogador fica com 0 dinheiro
    // OU
    // O castelo do reino inicial do jogador é derrotado
    // OU
    // A satisfação atinge um dado limiar

    [Range(-100000, 0)] public int GameOverMoney = 0;
    [Range(0, 100)] public int GameOverHappiness = 0;

    protected DefeatConditionManager() { }

    private void Start()
    {
        TimerPanel.OnIndividualBattleEnded += TimerPanel_OnAfterIndividualBattles;
    }

    private void TimerPanel_OnAfterIndividualBattles()
    {
        bool moneyDefeat = CheckMoneyDefeatCondition();
        bool castleDefeat = CheckCastleDefeatCondition();
        bool happinessDefeat = CheckHappinessDefeatCondition();

        if (moneyDefeat || castleDefeat || happinessDefeat)
        {
            GameManager.Instance.GameLost();
        }
    }

    private bool CheckMoneyDefeatCondition()
    {
        return GameManager.Instance.Gold <= GameOverMoney;
    }

    private bool CheckCastleDefeatCondition()
    {
        foreach (Property property in PropertyManager.Instance.Propriedades)
        {
            if (property.mainProperty && property.OriginalKingdom == StartingKingdomController.Instance.PlayerKingdom && !property.dominated)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckHappinessDefeatCondition()
    {
        return GameManager.Instance.Happiness <= GameOverHappiness;
    }
}
