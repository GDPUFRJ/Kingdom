using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInfoManager : MonoBehaviour {
    [Header("RIQ (Gold)")]
    [SerializeField] private Text currentRiq;
    [SerializeField] private Text nextRiq;
    [Header("CON (Building)")]
    [SerializeField] private Text currentCon;
    [SerializeField] private Text nextCon;
    [Header("ALI (Food)")]
    [SerializeField] private Text currentAli;
    [SerializeField] private Text nextAli;

    private void Start()
    {
        TimerPanel.OnAfterDayEnd += UpdateHUD;
        UpdateHUD();
    }
    public void UpdateHUD()
    {
        //GameManager pm = FindObjectOfType<GameManager>();
        currentRiq.text = GameManager.Instance.Gold.ToString();
        nextRiq.text = GameManager.Instance.GoldNext.ToString();

        currentCon.text = GameManager.Instance.Building.ToString();
        nextCon.text = GameManager.Instance.BuildingNext.ToString();

        currentAli.text = GameManager.Instance.Food.ToString();
        nextAli.text = GameManager.Instance.FoodNext.ToString();
    }
}
