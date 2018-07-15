using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBox : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    [SerializeField] private Text riq;
    [SerializeField] private Text con;
    [SerializeField] private Text food;

    [SerializeField] private Text happiness;
    [SerializeField] private Text soldier;

    private Property property;

    public void SetInformation(Property prop)
    {
        //funcao dentro de toda propriedade que retorna os recursos que ela atualmente gasta/produz
        //funcao que retorna facilmente seu sprite
        this.property = prop;

        this.title.text = prop.customTitle;
    }
    public Property GetProperty()
    {
        return property;
    }
}
