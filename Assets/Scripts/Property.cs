using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Property : MonoBehaviour, IPointerClickHandler, IComparer{
    [Header("Basic Informations")]
    public string customTitle = " ";
    public Tipo type;
    public bool dominated = false;
    public Level level = Level.Level1; //DO NOT CHANGE THIS DIRECTLY

    [Header("Production Information")]
    [Range(-100, 100)] public int goldLevel1 = 0;
    [Range(-100, 100)] public int foodLevel1 = 0;
    [Range(-100, 100)] public int buildingLevel1 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldLevel2 = 0;
    [Range(-100, 100)] public int foodLevel2 = 0;
    [Range(-100, 100)] public int buildingLevel2 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldLevel3 = 0;
    [Range(-100, 100)] public int foodLevel3 = 0;
    [Range(-100, 100)] public int buildingLevel3 = 0;

    [Header("Custom Sprites")]
    public Sprite CustomLevel1;
    public Sprite CustomLevel2;
    public Sprite CustomLevel3;

    [Header("Neighborhood")]
    [HideInInspector]public List<Property> Neighbors = new List<Property>();
    

    private void Start()
    {
        TimerPanel.OnDayEnd += OnDayEnd;

        // DestroyNeighborLines();
        //BuildNeighborLines();
    }



    private void OnDayEnd()
    {
        //Debug.Log("A propriedade " + customTitle + " Passou para o dia seguinte");
        if (!dominated) return;

        switch (level)
        {
            case Level.Level1:
                PropertyManager.Instance.Gold += goldLevel1;
                PropertyManager.Instance.Food += foodLevel1;
                PropertyManager.Instance.Building += buildingLevel1;
                break;
            case Level.Level2:
                PropertyManager.Instance.Gold += goldLevel2;
                PropertyManager.Instance.Food += foodLevel2;
                PropertyManager.Instance.Building += buildingLevel2;
                break;
            case Level.Level3:
                PropertyManager.Instance.Gold += goldLevel3;
                PropertyManager.Instance.Food += foodLevel3;
                PropertyManager.Instance.Building += buildingLevel3;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ative aqui o metodo para mostrar propriedades
        Debug.Log("Tocou!");
    }

    private void OnApplicationQuit()
    {
        TimerPanel.OnDayEnd -= OnDayEnd;
    }

    public int Compare(object x, object y)
    {
        Property a = x as Property;
        Property b = y as Property;

        if (a.gameObject.Equals(b.gameObject))
            return 0;
        else
            return 1;
    }

    public void SetLevel(Level level)
    {
        this.level = level;
        UpdateSprite(this);
    }
    //in game
    public void SetDominated(bool dominated)
    {
        if (dominated)
        {
            this.dominated = dominated;
            PropertyManager.Instance.lineManager.UpdateRelatedLines(this);
        }
        else
        {
            this.dominated = dominated;
            PropertyManager.Instance.lineManager.UpdateRelatedLines(this);
        }
    }

    //in editor
    public void UpdateSprite(Property property)
    {
        SpriteRenderer spriteRenderer = property.GetComponent<SpriteRenderer>();

        if (property.dominated)
        {
            spriteRenderer.color = Color.white;
            PropertyManager.Instance.lineManager.UpdateRelatedLines(this);
        }
        else
        {
            spriteRenderer.color = PropertyManager.Instance.NotDominatedProperty;
            PropertyManager.Instance.lineManager.UpdateRelatedLines(this);
        }
            

        switch (property.type)
        {
            case Tipo.Castle:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.CastleLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.CastleLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.CastleLevel3;
                        break;
                }
                property.gameObject.tag = "castle";
                break;
            case Tipo.Mine:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.MineLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.MineLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.MineLevel3;
                        break;
                }
                property.gameObject.tag = "mine";

                break;
            case Tipo.Village:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.VillageLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.VillageLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.VillageLevel3;
                        break;
                }
                property.gameObject.tag = "village";
                break;
            case Tipo.Farm:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.FarmLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.FarmLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.FarmLevel3;
                        break;
                }
                property.gameObject.tag = "farm";

                break;
            case Tipo.Forest:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.ForestLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.ForestLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.ForestLevel3;
                        break;
                }
                property.gameObject.tag = "forest";

                break;
            default:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = property.CustomLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = property.CustomLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = property.CustomLevel3;
                        break;
                }
                property.gameObject.tag = "other";
                // spriteRenderer.sprite = PropertyManager.Instance.Castelo;
                break;
        }
    }
}
public enum Tipo
{
    Castle, Mine, Village, Farm, Forest, Other 
}
public enum Level
{
    Level1 = 1, Level2 = 2, Level3 = 3
}