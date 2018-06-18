using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            //Armazena o toque
            Touch touchZero = Input.GetTouch(0);

            if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(),touchZero.position))
            {
                //Debug.Log("Inside");
                Close();
            }
        }
    }

    public void UpdatePropertyInfo()
    {
        propertyName.text = property.customTitle;
        propertyLevel.text = property.level.GetHashCode().ToString();
        riqResource.text = property.CurrentResource(Resource.Gold).ToString();
        aliResource.text = property.CurrentResource(Resource.Food).ToString();
        conResource.text = property.CurrentResource(Resource.Building).ToString();
    }
    public void UpgradeProperty()
    {
        //consumir itens e aprimorar nivel da propriedade

        if (property.level == Level.Level1)
        {
            if (GameManager.Instance.ConsumeItens(property.goldToLevel2, 
                                                      property.buildingToLevel2,
                                                      property.foodToLevel2))
            {
                property.LevelUp(Level.Level2);
                print("Upgraded");
                UpdatePropertyInfo();
            }
                
        }
        else if(property.level == Level.Level2)
        {
            if (GameManager.Instance.ConsumeItens(property.goldToLevel3,
                                                      property.buildingToLevel3,
                                                      property.foodToLevel3))
            {
                property.LevelUp(Level.Level3);
                print("Upgraded");
                UpdatePropertyInfo();
            }
        }
        
    }
    public void GiveUpProperty()
    {
        //TODO: VERIFICAR SE ESTA PROPRIEDADE NAO VAI ISOLAR OUTRAS
        //desativo a propriedade, depois verifico se algum vizinho ficou
        //isolado. Se ficou, desfaz, se nao, mantém o abandono.
        if (property.SetDominated(false))
        {
            GameManager.Instance.UpdateComsumption();
            print("Give Up");
        }
            
    }
    public void Close()
    {
        Destroy(this.gameObject);
    }


}
