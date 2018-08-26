using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PropertyWindow : MonoBehaviour {
    private Property property;
    [SerializeField] private float timeToAppear = 0.5f;

    [Header("UI Elements")]
    [SerializeField] private Text propertyName;
    [SerializeField] private Image icon;
    [SerializeField] private Text propertyLevel;
    [SerializeField] private Text riqResource;
    [SerializeField] private Text aliResource;
    [SerializeField] private Text conResource;

    private void Start()
    {
        GetComponent<CanvasGroup>().DOFade(0, 0);
    }
    public void Open()
    {
        GetComponent<CanvasGroup>().DOFade(0, 0);
        GetComponent<CanvasGroup>().DOFade(1, timeToAppear);
    }
    public void Close()
    {
        GetComponent<CanvasGroup>().DOFade(0, timeToAppear);
        Destroy(this.gameObject,timeToAppear);
    }
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
        else if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                //Debug.Log("Inside");
                Close();
            }
        }
    }
    public void UpdatePropertyInfo()
    {
        icon.sprite = property.GetComponent<SpriteRenderer>().sprite;
        propertyName.text = property.customTitle;
        propertyLevel.text = property.level.GetHashCode().ToString();
        riqResource.text = property.GetCurrentResource(Resource.Gold).ToString();
        aliResource.text = property.GetCurrentResource(Resource.Food).ToString();
        conResource.text = property.GetCurrentResource(Resource.Building).ToString();
    }
    public void UpgradeProperty()
    {
        //consumir itens e aprimorar nivel da propriedade

        if (property.Upgradable())
        {
            if (GameManager.Instance.ConsumeItens(property.GetUpgradeInformations()))
            {
                property.LevelUp();
                print("Upgraded");
                UpdatePropertyInfo();

            }
        }
        UpdatePropertyInfo();
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
}
