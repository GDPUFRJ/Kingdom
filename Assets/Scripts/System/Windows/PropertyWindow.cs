using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyWindow : MonoBehaviour {
    private Property property;
    [Header("UI Elements")]
    [SerializeField] private Text propertyName;
    [SerializeField] private Image icon;
    [SerializeField] private Text propertyLevel;
    [SerializeField] private Text riqResource;
    [SerializeField] private Text aliResource;
    [SerializeField] private Text conResource;

    public void GetProperty(Property property)
    {
        this.property = property;
        UpdatePropertyInfo();
    }
    public void UpdatePropertyInfo()
    {
        propertyName.text = property.customTitle;
        propertyLevel.text = property.level.GetHashCode().ToString();
        //MUDAR: Criar método em todas propriedade que retorna os valores de recurso levando em conta seu level
        riqResource.text = property.goldLevel1.ToString();
        aliResource.text = property.foodLevel1.ToString();
        conResource.text = property.buildingLevel1.ToString();
    }
    public void UpgradeProperty()
    {
        print("Upgraded");
    }
    public void GiveUpProperty()
    {
        print("Give Up");
    }
    public void Close()
    {
        Destroy(this.gameObject);
    }
}
