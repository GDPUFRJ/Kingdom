using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PropertyBox : MonoBehaviour {
    private const float TIME_TO_GIVEUP = 0.1f;
    private const float TIME_TO_UPGRADE = 0.1f;
    private const float BOX_MAX_INCREASE_TAX = 0.1f;
    private const float TIME_TO_OPEN_OR_CLOSE_UPGRADE_PANEL = 0.5f;

    [Header("Property HUD Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text gold;
    [SerializeField] private Text building;
    [SerializeField] private Text food;
    [SerializeField] private Text happiness;
    [SerializeField] private Text soldier;
    [SerializeField] private RectTransform upgradePanel;

    [Header("Upgrade Panel HUD Elements")]
    [SerializeField] private Text upgradeBuilding;
    [SerializeField] private Text upgradeGold;
    [SerializeField] private Text upgradeFood;
    [SerializeField] private Text upgradeSoldier;
    [SerializeField] private Text upgradeHappiness;
    [SerializeField] private Text buildingCost;
    [SerializeField] private Text goldCost;
    [SerializeField] private Text foodCost;
    [SerializeField] private Text nextLevelText;
    [SerializeField] private Image silhouetteIcon;

    private Property property;
    private PropertyPanel propertyPanel;
    private bool isUpgradePanelOpen = false;

    public void SetInformation(Property prop, PropertyPanel panel)
    {
        Informations info = prop.GetInfo();

        this.propertyPanel = panel;
        this.property = prop;
        this.icon.sprite = info.sprite;
        this.title.text = /*"[" + prop.Level + "] " + */prop.customTitle;
        this.food.text = info.Food.ToString();
        this.gold.text = info.Gold.ToString();
        this.building.text = info.Building.ToString();
        this.happiness.text = info.happiness.ToString();
        this.soldier.text = info.Soldiers.ToString();
    }
    public Property GetProperty()
    {
        return this.property;
    }
    public void Upgrade()
    {
        if(property.Level != Level.Level3)
            StartCoroutine(UpgradeAnimation());
    }
    public void GiveUp()
    {
        if (property.type != PropertyType.Castle)
            StartCoroutine(GiveUpAnimation());
    }
    private IEnumerator GiveUpAnimation()
    {
        this.property.SetDominated(false, true);
        this.gameObject.transform.DOScale(0, TIME_TO_GIVEUP);
        yield return new WaitForSeconds(TIME_TO_GIVEUP);
        propertyPanel.PrepareContent();
    }
    private IEnumerator UpgradeAnimation()
    {
        this.property.LevelUp();
        this.gameObject.transform.DOPunchScale(new Vector3(BOX_MAX_INCREASE_TAX, BOX_MAX_INCREASE_TAX, BOX_MAX_INCREASE_TAX), TIME_TO_UPGRADE);
        yield return new WaitForSeconds(TIME_TO_UPGRADE);
        this.SetInformation(property, propertyPanel);
        SetUpgradeInformation();

        if (property.Level == Level.Level3)
        {
            CloseUpgradePanelImmediately();
        }
    }
    public void OpenOrCloseUpgradePanel()
    {
        if (!isUpgradePanelOpen)
        {
            OpenUpgradePanel();
        }
        else
        {
            CloseUpgradePanel();
        }
    }
    private void OpenUpgradePanel()
    {
        if (property.Level != Level.Level3)
        {
            RectTransform propertyBox = GetComponent<RectTransform>();
            upgradePanel.gameObject.SetActive(true);
            propertyBox.DOSizeDelta(new Vector2(propertyBox.sizeDelta.x, 443.2f), TIME_TO_OPEN_OR_CLOSE_UPGRADE_PANEL);
            SetUpgradeInformation();
            isUpgradePanelOpen = true;
        }
    }
    public void CloseUpgradePanel()
    {
        StartCoroutine(CloseUpgradePanelAnimation());
        isUpgradePanelOpen = false;
    }
    private IEnumerator CloseUpgradePanelAnimation()
    {
        RectTransform propertyBox = GetComponent<RectTransform>();
        upgradePanel.gameObject.SetActive(false);
        Tween t = propertyBox.DOSizeDelta(new Vector2(propertyBox.sizeDelta.x, 169.07f), TIME_TO_OPEN_OR_CLOSE_UPGRADE_PANEL);
        yield return t.WaitForCompletion();
    }
    private void CloseUpgradePanelImmediately()
    {
        RectTransform propertyBox = GetComponent<RectTransform>();
        upgradePanel.gameObject.SetActive(false);
        propertyBox.sizeDelta = new Vector2(propertyBox.sizeDelta.x, 169.07f);
        isUpgradePanelOpen = false;
    }
    private void SetUpgradeInformation()
    {
        switch(property.Level)
        {
            case Level.Level1:
                upgradeBuilding.text  = property.buildingLevel2.ToString();
                upgradeGold.text      = property.goldLevel2.ToString();
                upgradeFood.text      = property.foodLevel2.ToString();
                upgradeSoldier.text   = property.soldierLevel2.ToString();
                upgradeHappiness.text = property.happiness.ToString();
                buildingCost.text     = property.buildingToLevel2.ToString();
                goldCost.text         = property.goldToLevel2.ToString();
                foodCost.text         = property.foodToLevel2.ToString();
                nextLevelText.text    = "nivel 2";
                break;

            case Level.Level2:
                upgradeBuilding.text  = property.buildingLevel3.ToString();
                upgradeGold.text      = property.goldLevel3.ToString();
                upgradeFood.text      = property.foodLevel3.ToString();
                upgradeSoldier.text   = property.soldierLevel3.ToString();
                upgradeHappiness.text = property.happiness.ToString();
                buildingCost.text     = property.buildingToLevel3.ToString();
                goldCost.text         = property.goldToLevel3.ToString();
                foodCost.text         = property.foodToLevel3.ToString();
                nextLevelText.text    = "nivel 3";
                break;
        }

        UpdateSilhouetteIcon();
    }
    private void UpdateSilhouetteIcon()
    {
        switch (property.type)
        {
            case PropertyType.Castle:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.CastleLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.CastleLevel3;
                        break;
                }
                break;
            case PropertyType.Mine:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.MineLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.MineLevel3;
                        break;
                }
                break;
            case PropertyType.Village:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.VillageLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.VillageLevel3;
                        break;
                }
                break;
            case PropertyType.Farm:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.FarmLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.FarmLevel3;
                        break;
                }
                break;
            case PropertyType.Forest:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.ForestLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.ForestLevel3;
                        break;
                }
                break;
            case PropertyType.quarter:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = PropertyManager.Instance.quarterLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = PropertyManager.Instance.quarterLevel3;
                        break;
                }
                break;

            default:
                switch (property.Level)
                {
                    case Level.Level1:
                        silhouetteIcon.sprite = property.CustomLevel2;
                        break;
                    case Level.Level2:
                        silhouetteIcon.sprite = property.CustomLevel3;
                        break;
                }
                break;
        }
    }
}
