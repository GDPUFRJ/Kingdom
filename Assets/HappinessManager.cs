using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : Singleton<HappinessManager> {

    protected HappinessManager() { }

    GameManager gm;
    BattleManager bm;
    private List<BattleEffectOverHappiness> battleEffectsOverHappiness = new List<BattleEffectOverHappiness>();

    [Range(0, 100)]
    [Tooltip("Happiness lost by the player when they lose a battle.")]
    [SerializeField] private float happinessLostByLosingBattle = 5f;
    [Range(0, 100)]
    [Tooltip("Happiness gained by the player when they lose a battle.")]
    [SerializeField] private float happinessGainedByWinningBattle = 5f;
    [Tooltip("Number of turns that each battle affects happiness.")]
    [SerializeField] private int numberOfTurnsBattlesAffectHappiness = 5;

    // Use this for initialization
    void Start () {
        TimerPanel.OnAfterDayEnd += TimerPanel_OnAfterDayEnd;
        TimerPanel.OnBattleEnded += OnBattlesEnded;

        gm = GameManager.Instance;
        bm = FindObjectOfType<BattleManager>();
	}

    private void TimerPanel_OnAfterDayEnd()
    {
        //float NewHappyness = 0;
        //int dominatedProperties = 0;
        //foreach(Property p in pm.Propriedades)
        //{
        //    if (p.dominated == true)
        //    {
        //        NewHappyness += p.happiness;
        //        dominatedProperties++;
        //    }
        //}

        //gm.Happiness = NewHappyness / dominatedProperties;
        gm.Happiness += gm.HappinessNextEventModifier;
    }

    private void OnBattlesEnded()
    {
        if (bm.NumberOfBattlesWonByThePlayerLastTurn > 0 || bm.NumberOfBattlesLostByThePlayerLastTurn > 0)
        {
            NewBattleModifier();
        }

        if (battleEffectsOverHappiness.Count > 0)
        {
            ApplyBattleModifiers();
        }
    }

    private void NewBattleModifier()
    {
        float modifier = bm.NumberOfBattlesWonByThePlayerLastTurn * happinessGainedByWinningBattle - bm.NumberOfBattlesLostByThePlayerLastTurn * happinessLostByLosingBattle;
        int turns = numberOfTurnsBattlesAffectHappiness;
        battleEffectsOverHappiness.Add(new BattleEffectOverHappiness(modifier, turns));
    }

    private void ApplyBattleModifiers()
    {
        foreach (var battleEffect in battleEffectsOverHappiness)
        {
            gm.Happiness += battleEffect.happinessModifier;
            battleEffect.numberOfTurns--;
        }
        for (int i = 0; i < battleEffectsOverHappiness.Count; i++)
        {
            if (battleEffectsOverHappiness[i].numberOfTurns == 0)
            {
                battleEffectsOverHappiness.Remove(battleEffectsOverHappiness[i]);
                i--;
            }
        }

        FindObjectOfType<HudInfoManager>().UpdateHUD();
    }
}

public class BattleEffectOverHappiness
{
    public float happinessModifier;
    public int numberOfTurns;

    public BattleEffectOverHappiness(float modifier, int turns)
    {
        happinessModifier = modifier;
        numberOfTurns = turns;
    }
}