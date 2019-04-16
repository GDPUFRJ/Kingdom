using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInfoManager : MonoBehaviour {
    [Header("Top Bar Info")]
    [SerializeField] private Text currentDate;
    [SerializeField] private Text currentHappiness;
    [SerializeField] private Text currentPopulation;
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
        currentDate.text = ((TranslationManager.GameLanguage == Language.Portuguese) ? ("dia ") : ("day ")) + FindObjectOfType<TimerPanel>().GetCurrentDay().ToString();
        currentHappiness.text = GameManager.Instance.Happiness.ToString();
        currentPopulation.text = GameManager.Instance.Population.ToString();

        currentRiq.text = GameManager.Instance.Gold.ToString();
        nextRiq.text = "+" + GameManager.Instance.GoldNext.ToString();

        currentCon.text = GameManager.Instance.Building.ToString();
        nextCon.text = "+" + GameManager.Instance.BuildingNext.ToString();

        currentAli.text = GameManager.Instance.Food.ToString();
        nextAli.text = "+" + GameManager.Instance.FoodNext.ToString();
    }
}
