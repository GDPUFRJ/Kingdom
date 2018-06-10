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
    private void UpdateHUD()
    {
        PropertyManager pm = FindObjectOfType<PropertyManager>();
        currentRiq.text = pm.Gold.ToString();
        nextRiq.text = pm.GoldNext.ToString();

        currentCon.text = pm.Building.ToString();
        nextCon.text = pm.BuildingNext.ToString();

        currentAli.text = pm.Food.ToString();
        nextAli.text = pm.FoodNext.ToString();
    }
}
