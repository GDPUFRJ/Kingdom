using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    private List<Property> propertiesInBattle = new List<Property>();
    private List<BattleInformation> battleInformations = new List<BattleInformation>();
    [SerializeField] private BattleWindow battleWindow;

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
    }
    private IEnumerator BattleRoutine()
    {
        TimerPanel.SetPause(true);
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

        if (PropertyIsEmpty(property))
        {
            property.SetDominated(!property.dominated, false);
            property.SetSoldiers(SoldierType.InProperty, property.GetSoldiers(SoldierType.Enemy));
            property.kingdom = battleInformation.attackingKingdom;
        }
        battleWindow.Show(property.GetSoldiers(SoldierType.Enemy), property.GetSoldiers(SoldierType.InProperty), battleInformation);
        if (InvaderIsTheWinner(property))
        {
            property.SetDominated(!property.dominated, false);
            property.SetSoldiers(SoldierType.InProperty, property.GetSoldiers(SoldierType.Enemy) - property.GetSoldiers(SoldierType.InProperty));
            property.kingdom = battleInformation.attackingKingdom;
        }
        else
        {
            property.SetSoldiers(SoldierType.InProperty, property.GetSoldiers(SoldierType.InProperty) - property.GetSoldiers(SoldierType.Enemy));
        }
        property.SetSoldiers(SoldierType.Enemy, 0);
        yield return battleWindow.currentBattle;
        yield return cam.Zoom(false);
        property.UpdateSoldierInfo();
        property.UpdateSprite(property);
    }

    private bool PropertyIsEmpty(Property p) { return p.GetSoldiers(SoldierType.InProperty) == 0; }
    private bool InvaderIsTheWinner(Property p) { return p.GetSoldiers(SoldierType.Enemy) > p.GetSoldiers(SoldierType.InProperty); }
}
