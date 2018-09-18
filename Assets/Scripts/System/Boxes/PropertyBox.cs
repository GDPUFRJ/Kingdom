using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PropertyBox : MonoBehaviour {
    private const float TIME_TO_GIVEUP = 0.1f;
    private const float TIME_TO_UPGRADE = 0.1f;
    private const float BOX_MAX_INCREASE_TAX = 0.1f;

    [Header("Property HUD Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private Text title;
    [SerializeField] private Text gold;
    [SerializeField] private Text building;
    [SerializeField] private Text food;
    [SerializeField] private Text happiness;
    [SerializeField] private Text soldier;

    private Property property;
    private PropertyPanel propertyPanel;

    public void SetInformation(Property prop, PropertyPanel panel)
    {
        Informations info = prop.GetInfo();

        this.propertyPanel = panel;
        this.property = prop;
        this.icon.sprite = info.sprite;
        this.title.text = "[" + prop.Level + "] " + prop.customTitle;
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
    }
}
