using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyPanel : AMainPanel
{
    [SerializeField] private GameObject propertyBoxPrefab;
    [SerializeField] private Transform root;

    public override void PrepareContent()
    {
        DeleteAllChilds();
        List<Property> propertyList = FindObjectOfType<PropertyManager>().Propriedades;
        foreach(Property prop in propertyList)
        {
            if (prop.dominated)
            {
                var obj = Instantiate(propertyBoxPrefab, root).GetComponent<PropertyBox>();
                obj.SetInformation(prop,this);
            }
        }
    }
    private void DeleteAllChilds()
    {
        for(int i = root.childCount-1; i >= 0; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }
}
