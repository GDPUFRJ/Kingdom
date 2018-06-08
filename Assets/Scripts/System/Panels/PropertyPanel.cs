using System.Collections.Generic;
using UnityEngine;

public class PropertyPanel : AMainPanel {
    private CanvasGroup canvasGroup;
    [SerializeField] private Transform propertiesParent;
    [SerializeField] private GameObject propertyPrefab;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void PrepareContent()
    {
        EraseAllPropertiesInPanel();
        CreatePropertiesInPanel(PropertyManager.Instance.Propriedades);
    }

    private void EraseAllPropertiesInPanel()
    {
        var propList = GetAllAttachedProperties();
        for(int i = propList.Count-1; i >= 0; i--)
        {
            propList[i].SetParent(null);
            Destroy(propList[i].gameObject);
        }
    }

    private List<Transform> GetAllAttachedProperties()
    {
        List<Transform> properties = new List<Transform>();
        foreach(Transform property in propertiesParent)
        {
            properties.Add(property);
        }
        return properties;
    }

    private void CreatePropertiesInPanel(List<Property> propertyList)
    {
        foreach(Property property in propertyList)
        {
            if (!property.dominated)
                continue;
            PropertyBox box = Instantiate(propertyPrefab, propertiesParent).GetComponent<PropertyBox>();
            box.SetInformation(property.customTitle, property.level, null, property);
        }
    }
}
