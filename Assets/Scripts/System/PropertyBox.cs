using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBox : MonoBehaviour {
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
        this.title.text = "[" + prop.level + "] " + prop.customTitle;
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
        this.property.LevelUp();
        this.SetInformation(property, propertyPanel);
    }
    public void GiveUp()
    {
        this.property.SetDominated(false);
        propertyPanel.PrepareContent();
    }
}
