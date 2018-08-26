using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Property : MonoBehaviour, IPointerClickHandler, IComparer
{
    [Header("Basic Informations")]
    public string customTitle = " ";
    public Tipo type;
    public bool dominated = false;
    public Level level = Level.Level1; //DO NOT CHANGE THIS DIRECTLY
    public int soldiers = 14;

    public int SoldiersToGetOut = 0;
    public int EnemySoldiers = 0;

    public List<BattleArrowController> ArrowsComingIn = new List<BattleArrowController>();
    public List<BattleArrowController> ArrowsComingOut = new List<BattleArrowController>();

    public int happiness = 90;

    public bool MoveSoldier = false;
    public bool ReadyToReceiveSoldier = false;

    [Header("Production Information")]
    [Range(-100, 100)] public int goldLevel1 = 0;
    [Range(-100, 100)] public int foodLevel1 = 0;
    [Range(-100, 100)] public int buildingLevel1 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldLevel2 = 0;
    [Range(-100, 100)] public int foodLevel2 = 0;
    [Range(-100, 100)] public int buildingLevel2 = 0;
    [Space(10)]
    [Range(-100, 100)]
    public int goldLevel3 = 0;
    [Range(-100, 100)] public int foodLevel3 = 0;
    [Range(-100, 100)] public int buildingLevel3 = 0;

    [Header("Upgrade Demands")]
    [Range(-100, 100)] public int goldToLevel2 = 0;
    [Range(-100, 100)] public int foodToLevel2 = 0;
    [Range(-100, 100)] public int buildingToLevel2 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldToLevel3 = 0;
    [Range(-100, 100)] public int foodToLevel3 = 0;
    [Range(-100, 100)] public int buildingToLevel3 = 0;

    [Header("Custom Sprites")]
    public Sprite CustomLevel1;
    public Sprite CustomLevel2;
    public Sprite CustomLevel3;

    private PanelController panelController;
    private GameObject NumSoldier;
    private GameObject EditButtons;



    [HideInInspector] public List<Property> Neighbors = new List<Property>();

    public UpgradeInformations GetUpgradeInformations()
    {
        UpgradeInformations upgradeInformations = new UpgradeInformations();

        if (level == Level.Level3)
            return null;

        switch (level)
        {
            case Level.Level1:
                upgradeInformations.Gold = goldToLevel2;
                upgradeInformations.Building = buildingToLevel2;
                upgradeInformations.Food = foodToLevel2;
                break;
            case Level.Level2:
                upgradeInformations.Gold = goldToLevel3;
                upgradeInformations.Building = buildingToLevel3;
                upgradeInformations.Food = foodToLevel3;
                break;
            default:
                break;
        }

        return upgradeInformations;
    }


    public Informations GetInfo()
    {
        Informations info = new Informations();
        info.sprite = GetComponent<SpriteRenderer>().sprite;

        switch (level)
        {
            case Level.Level1:
                info.Gold = goldLevel1;
                info.Food = foodLevel1;
                info.Building = buildingLevel1;
                break;
            case Level.Level2:
                info.Gold = goldLevel2;
                info.Food = foodLevel2;
                info.Building = buildingLevel2;
                break;
            case Level.Level3:
                info.Gold = goldLevel3;
                info.Food = foodLevel3;
                info.Building = buildingLevel3;
                break;
        }
        info.Soldiers = soldiers;
        info.happiness = happiness;

        return info;
    }


    private void Start()
    {
        TimerPanel.OnDayEnd += OnDayEnd;
        TimerPanel.OnAfterDayEnd += TimerPanel_OnAfterDayEnd;

        panelController = GameManager.Instance.canvasRoot.GetComponent<PanelController>();
        EditButtons = GameManager.Instance.EditButtons;

        if (dominated == false) soldiers = Random.Range(10, 20);

        NumSoldier = Instantiate(GameManager.Instance.NumSoldier, GameManager.Instance.CanvasBattle.transform);
        NumSoldier.GetComponent<Text>().text = soldiers.ToString();
        NumSoldier.GetComponent<NumSoldiersTextController>().Owner = this.transform;
        NumSoldier.transform.position = this.transform.position;

        foreach(Property neighbor in Neighbors)
        {
            GameObject NewArrow;
            NewArrow = Instantiate(GameManager.Instance.ArrowPrefab, EditButtons.transform);

            BattleArrowController newBAC = NewArrow.GetComponent<BattleArrowController>();
            newBAC.SetSourceAndDestination(this, destination: neighbor);
            newBAC.CreateSoldierButton();
            newBAC.NumSoldierFather = NumSoldier;

            newBAC.SetPosition();

            ArrowsComingOut.Add(newBAC);
            neighbor.ArrowsComingIn.Add(newBAC);
        }

        UpdateSoldierInfo();
    }

    private void TimerPanel_OnAfterDayEnd()
    {
        soldiers -= SoldiersToGetOut;
        SoldiersToGetOut = 0;
        UpdateSoldierInfo();

        if (EnemySoldiers > 0)
        {
            FindObjectOfType<BattleManager>().AddBattleProperty(this);
        }
    }

    private void OnDayEnd()
    {
        //Debug.Log("A propriedade " + customTitle + " Passou para o dia seguinte");
        if (!dominated) return;

        AddConsumption();
    }

    private void AddConsumption()
    {
        switch (level)
        {
            case Level.Level1:
                GameManager.Instance.Gold += goldLevel1;
                GameManager.Instance.Food += foodLevel1;
                GameManager.Instance.Building += buildingLevel1;
                break;
            case Level.Level2:
                GameManager.Instance.Gold += goldLevel2;
                GameManager.Instance.Food += foodLevel2;
                GameManager.Instance.Building += buildingLevel2;
                break;
            case Level.Level3:
                GameManager.Instance.Gold += goldLevel3;
                GameManager.Instance.Food += foodLevel3;
                GameManager.Instance.Building += buildingLevel3;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelController.currentPanel == 4) return;

        StartCoroutine(Camera.main.GetComponent<CameraMovement>().FollowPosition(transform.position));

        var pw = Instantiate(PropertyManager.Instance.propertyWindowPrefab,
                             PropertyManager.Instance.canvasParent).GetComponent<PropertyWindow>();
        pw.GetProperty(this);
        pw.Open();
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

    public void LevelUp()
    {
        if (level == Level.Level3)
            return;
        if (level == Level.Level1)
            this.level = Level.Level2;
        else if (level == Level.Level2)
            this.level = Level.Level3;
        UpdateSprite(this);
    }

    public void SetLevel(Level level)
    {
        this.level = level;
        UpdateSprite(this);
    }

    public bool Upgradable()
    {
        if (level == Level.Level1 || level == Level.Level2)
            return true;
        else
            return false;
    }

    //in game
    public bool SetDominated(bool dominated)
    {
        //TODO: VERIFICAR SE ESTA PROPRIEDADE NAO VAI ISOLAR OUTRAS
        //desativo a propriedade, depois verifico se algum vizinho ficou
        //isolado. Se ficou, desfaz, se nao, mantém o abandono.
        if (type == Tipo.Castle)
            return false;

        this.dominated = dominated;

        PropertyManager.Instance.lineManager.UpdateRelatedLines(this);

        foreach (BattleArrowController bac in ArrowsComingOut)
            bac.UpdateSoldierButton();

        return true;
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
            case Tipo.quarter:
                switch (property.level)
                {
                    case Level.Level1:
                        spriteRenderer.sprite = PropertyManager.Instance.quarterLevel1;
                        break;
                    case Level.Level2:
                        spriteRenderer.sprite = PropertyManager.Instance.quarterLevel2;
                        break;
                    case Level.Level3:
                        spriteRenderer.sprite = PropertyManager.Instance.quarterLevel3;
                        break;
                }
                property.gameObject.tag = "quarter";
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


    public int CurrentResource(Resource resource)
    {
        switch (resource)
        {
            case Resource.Gold:
                if (level == Level.Level1)
                    return goldLevel1;
                else if (level == Level.Level2)
                    return goldLevel2;
                else
                    return goldLevel3;
            case Resource.Building:
                if (level == Level.Level1)
                    return buildingLevel1;
                else if (level == Level.Level2)
                    return buildingLevel2;
                else
                    return buildingLevel3;
            case Resource.Food:
                if (level == Level.Level1)
                    return foodLevel1;
                else if (level == Level.Level2)
                    return foodLevel2;
                else
                    return foodLevel3;
            default:
                return 0;
        }
    }

    public enum Tipo
    {
        Castle, Mine, Village, Farm, Forest, Other, quarter
    }

    public class UpgradeInformations
    {
        public int Gold;
        public int Food;
        public int Building;
    }

    public void UpdateSoldierInfo()
    {
        NumSoldiersTextController numSoldiersTextController = NumSoldier.GetComponent<NumSoldiersTextController>();

        if (dominated) numSoldiersTextController.SetColor(false);
        else numSoldiersTextController.SetColor(true);

        NumSoldier.GetComponent<NumSoldiersTextController>().UpdateText(soldiers.ToString());
    }
}

public enum Level
{
    Level1 = 1, Level2 = 2, Level3 = 3
}
public enum Resource
{
    Gold, Building, Food
}

public struct Informations
{
    public Sprite sprite;
    public int Gold;
    public int Food;
    public int Building;
    public int Soldiers;
    public int happiness;
}