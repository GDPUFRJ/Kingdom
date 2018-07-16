using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    [SerializeField] private Text gold;
    [SerializeField] private Text building;
    [SerializeField] private Text food;

    [SerializeField] private Text happiness;
    [SerializeField] private Text soldier;

    private Property property;

    public void SetInformation(Property prop, Informations info)
    {
        //funcao dentro de toda propriedade que retorna os recursos que ela atualmente gasta/produz
        //funcao que retorna facilmente seu sprite
        this.property = prop;
        this.icon.sprite = info.sprite;

        this.title.text = prop.customTitle;

        this.food.text = "+" + info.Food.ToString();
        this.gold.text = "+" + info.Gold.ToString();
        this.building.text = "+" + info.Building.ToString();

        this.happiness.text = "+" + info.happiness.ToString();
        this.soldier.text = "+" + info.Soldiers.ToString();
    }
    public Property GetProperty()
    {
        return property;
    }
    public void Upgrade()
    {
        //levelUp
    }
    public void GiveUp()
    {
        //isDominated = false
    }
}
