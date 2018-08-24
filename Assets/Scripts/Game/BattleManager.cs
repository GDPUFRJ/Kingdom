using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    private List<Property> propertiesInBattle = new List<Property>();
    [SerializeField] private BattleWindow battleWindow;

    private void ResetBattleList()
    {
        propertiesInBattle = new List<Property>();
    }
    public void AddBattleProperty(Property prop)
    {
        if (propertiesInBattle == null)
            ResetBattleList();
        propertiesInBattle.Add(prop);
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
            yield return IndividualBattle(propertiesInBattle[i]);
        }
        OnBattlesEnd();
        TimerPanel.SetPause(false);
        ResetBattleList();
    }
    private IEnumerator IndividualBattle(Property property)
    {
        var cam = Camera.main.GetComponent<CameraMovement>();
        yield return cam.FollowPosition(property.gameObject.transform.position);
        yield return cam.Zoom(true);

        if (PropertyIsEmpty(property))
        {
            property.SetDominated(!property.dominated);
            property.soldiers = property.EnemySoldiers;
        }
        battleWindow.Show(property.EnemySoldiers, property.soldiers);
        if (InvaderIsTheWinner(property))
        {
            property.SetDominated(!property.dominated);
            property.soldiers = property.EnemySoldiers - property.soldiers;
        }
        else
        {
            property.soldiers = property.soldiers - property.EnemySoldiers;
        }
        property.EnemySoldiers = 0;
        yield return battleWindow.currentBattle;
        yield return cam.Zoom(false);
    }
    private bool PropertyIsEmpty(Property p) { return p.soldiers == 0; }
    private bool InvaderIsTheWinner(Property p) { return p.EnemySoldiers > p.soldiers; }
}
