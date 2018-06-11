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
        //PropertyManager pm = FindObjectOfType<PropertyManager>();
        currentRiq.text = PropertyManager.Instance.Gold.ToString();
        nextRiq.text = PropertyManager.Instance.GoldNext.ToString();

        currentCon.text = PropertyManager.Instance.Building.ToString();
        nextCon.text = PropertyManager.Instance.BuildingNext.ToString();

        currentAli.text = PropertyManager.Instance.Food.ToString();
        nextAli.text = PropertyManager.Instance.FoodNext.ToString();
    }
}
