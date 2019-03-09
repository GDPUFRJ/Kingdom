using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Property : MonoBehaviour, IVertex<Property>, IPointerClickHandler, IComparer
{
    public int index;
    public Property Data { get; set; } //IVertex
    public List<IEdge<Line>> Linhas = new List<IEdge<Line>>();

    [Header("Basic Informations")]
    public string customTitle = " ";
    public PropertyType type;
    public bool canBeAbandonedOrLostInBattle = true;
    public bool dominated = false;
    public bool mainProperty = false;
    public Kingdom kingdom;

    [SerializeField]
    private Level level = Level.Level1;
    public Level Level {
        get { return level; }
        set { level = value; UpdateSprite(this); }
    }



    private int soldiers = 14;
    private int SoldiersToGetOut = 0;
    private int EnemySoldiers = 0;
    private BattleInformation attackInformation = null;

    [HideInInspector] public List<BattleArrowController> ArrowsComingIn = new List<BattleArrowController>();
    [HideInInspector] public List<BattleArrowController> ArrowsComingOut = new List<BattleArrowController>();

    public float happiness = 60;

    [Header("Production Information")]
    [Range(-100, 100)] public int goldLevel1 = 0;
    [Range(-100, 100)] public int foodLevel1 = 0;
    [Range(-100, 100)] public int buildingLevel1 = 0;
    [Range(0, 100)] public int soldierLevel1 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldLevel2 = 0;
    [Range(-100, 100)] public int foodLevel2 = 0;
    [Range(-100, 100)] public int buildingLevel2 = 0;
    [Range(0, 100)] public int soldierLevel2 = 0;
    [Space(10)]
    [Range(-100, 100)] public int goldLevel3 = 0;
    [Range(-100, 100)] public int foodLevel3 = 0;
    [Range(-100, 100)] public int buildingLevel3 = 0;
    [Range(0, 100)] public int soldierLevel3 = 0;

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

    private SectionManager sectionManager;
    private GameObject NumSoldier;
    private GameObject EditButtons;

    [HideInInspector] public List<Property> Neighbors = new List<Property>();

    private void Start()
    {
        TimerPanel.OnDayEnd += OnDayEnd;
        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;

        sectionManager = GameManager.Instance.CanvasHUD.GetComponent<SectionManager>();
        EditButtons = GameManager.Instance.EditButtons;

        if (dominated == false) soldiers = Random.Range(10, 20);

        NumSoldier = Instantiate(GameManager.Instance.NumSoldier, GameManager.Instance.CanvasBattle.transform);
        NumSoldier.GetComponent<Text>().text = soldiers.ToString();
        NumSoldier.GetComponent<NumSoldiersTextController>().Owner = this.transform;
        NumSoldier.transform.position = this.transform.position;

        foreach (Property neighbor in Neighbors)
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

    private void OnDayEnd()
    {
        if (dominated)
        {
            AddConsumption();
        }

        RestockSoldiers();
    }

    private void OnAfterDayEnd()
    {
        soldiers -= SoldiersToGetOut;
        SoldiersToGetOut = 0;
        UpdateSoldierInfo();

        if (EnemySoldiers > 0)
        {
            FindObjectOfType<BattleManager>().AddBattleProperty(this, attackInformation);
        }

        attackInformation = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sectionManager.currentSection == 4 && SoldierPanel.isEditButtonsEnable == true) return;

        StartCoroutine(Camera.main.GetComponent<CameraMovement>().FollowPosition(transform.position));

        var pw = Instantiate(PropertyManager.Instance.propertyWindowPrefab,
                             PropertyManager.Instance.canvasParent).GetComponent<PropertyWindow>();
        pw.GetProperty(this);
        pw.Open();
    }

    public UpgradeInformations GetUpgradeInformations()
    {
        UpgradeInformations upgradeInformations = new UpgradeInformations();

        if (Level == Level.Level3)
            return null;

        switch (Level)
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

        switch (Level)
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

    private void ProcessHappiness()
    {
        GameManager gm = GameManager.Instance;

        if (gm.Food <= 0)
            happiness -= 2;
        if (gm.Food > 10000)
            happiness += 1;

        if (happiness > 100) happiness = 100;
        if (happiness < 0) happiness = 0;
    }

    private void AddConsumption()
    {
        switch (Level)
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

        ProcessHappiness();
    }

    private void RestockSoldiers()
    {
        switch (Level)
        {
            case Level.Level1:
                AddSoldiers(SoldierType.InProperty, soldierLevel1);
                break;
            case Level.Level2:
                AddSoldiers(SoldierType.InProperty, soldierLevel2);
                break;
            case Level.Level3:
                AddSoldiers(SoldierType.InProperty, soldierLevel3);
                break;
        }

        UpdateSoldierInfo();
    }

    public void LevelUp()
    {
        if (Level == Level.Level3)
            return;
        if (Level == Level.Level1)
            this.Level = Level.Level2;
        else if (Level == Level.Level2)
            this.Level = Level.Level3;
    }

    public bool Upgradable()
    {
        if (Level == Level.Level1 || Level == Level.Level2)
            return true;
        else
            return false;
    }

    //in game
    //byUser Means that the user requested it over UI
    //If it was a battle, byUser should be false, always
    public bool SetDominated(bool dominated, bool byUser)
    {
        //TODO: VERIFICAR SE ESTA PROPRIEDADE NAO VAI ISOLAR OUTRAS
        //desativo a propriedade, depois verifico se algum vizinho ficou
        //isolado. Se ficou, desfaz, se nao, mantém o abandono.
        if (dominated == false && byUser == true && canBeAbandonedOrLostInBattle == false)
            return false;

        this.dominated = dominated;

        PropertyManager.Instance.lineManager.UpdateRelatedLines(this);
        UpdateSprite(this);

        //distribute soldiers over near friend properties
        if (dominated == false && byUser == true)
        {
            while(soldiers > 0)
            {
                foreach(BattleArrowController bac in ArrowsComingOut)
                {
                    if (bac.GetTipo() == ArrowType.Arrow)
                    {
                        bac.Destination.soldiers++;
                        soldiers--;
                    }
                }
            }

        }

        foreach (BattleArrowController bac in ArrowsComingOut)
            bac.UpdateSoldierButton();

        //UpdateSoldierInfo(); // Não tá funcionando

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
            case PropertyType.Castle:
                switch (property.Level)
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
            case PropertyType.Mine:
                switch (property.Level)
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
            case PropertyType.Village:
                switch (property.Level)
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
            case PropertyType.Farm:
                switch (property.Level)
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
            case PropertyType.Forest:
                switch (property.Level)
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
            case PropertyType.quarter:
                switch (property.Level)
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
                switch (property.Level)
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

        UpdateOutlineSprite(property);
    }

    private void UpdateOutlineSprite(Property property)
    {
        SpriteRenderer outline;
        try { outline = property.transform.GetChild(0).GetComponent<SpriteRenderer>(); }
        catch (System.Exception) { return; }

        outline.sprite = property.GetComponent<SpriteRenderer>().sprite;

        switch (property.kingdom)
        {
            case Kingdom.Blue:
                outline.color = PropertyManager.Instance.BlueOutline;
                break;
            case Kingdom.Green:
                outline.color = PropertyManager.Instance.GreenOutline;
                break;
            case Kingdom.Orange:
                outline.color = PropertyManager.Instance.OrangeOutline;
                break;
            case Kingdom.Purple:
                outline.color = PropertyManager.Instance.PurpleOutline;
                break;
            case Kingdom.Red:
                outline.color = PropertyManager.Instance.RedOutline;
                break;
        }
    }

    public int GetCurrentResource(Resource resource)
    {
        switch (resource)
        {
            case Resource.Gold:
                if (Level == Level.Level1)
                    return goldLevel1;
                else if (Level == Level.Level2)
                    return goldLevel2;
                else
                    return goldLevel3;
            case Resource.Building:
                if (Level == Level.Level1)
                    return buildingLevel1;
                else if (Level == Level.Level2)
                    return buildingLevel2;
                else
                    return buildingLevel3;
            case Resource.Food:
                if (Level == Level.Level1)
                    return foodLevel1;
                else if (Level == Level.Level2)
                    return foodLevel2;
                else
                    return foodLevel3;
            default:
                return 0;
        }
    }

    public void AddSoldiers(SoldierType type, int SoldiersToAdd)
    {
        //SHOW ANIMATION, PRINT, PARTICLE, WHAT EVER
        switch (type)
        {
            case SoldierType.InProperty:
                soldiers += SoldiersToAdd;
                break;
            case SoldierType.Enemy:
                EnemySoldiers += SoldiersToAdd;
                break;
            case SoldierType.ToGetOut:
                SoldiersToGetOut += SoldiersToAdd;
                break;
        }
    }

    public void AddSoldiers(SoldierType type, int SoldiersToAdd, BattleInformation battleInformation)
    {
        AddSoldiers(type, SoldiersToAdd);
        if (SoldiersToAdd > 0 && type == SoldierType.Enemy)
            attackInformation = battleInformation;
    }

    public int GetSoldiers(SoldierType type)
    {
        switch (type)
        {
            case SoldierType.InProperty:
                return soldiers;
            case SoldierType.Enemy:
                return EnemySoldiers;
            case SoldierType.ToGetOut:
                return SoldiersToGetOut;
        }
        return 0;
    }

    public void SetSoldiers(SoldierType type, int SoldiersToSet)
    {
        switch (type)
        {
            case SoldierType.InProperty:
                soldiers = SoldiersToSet;
                break;
            case SoldierType.Enemy:
                EnemySoldiers = SoldiersToSet;
                break;
            case SoldierType.ToGetOut:
                SoldiersToGetOut = SoldiersToSet;
                break;
        }

    }

    public void RemoveSoldiers(SoldierType type, int SoldiersToRemove)
    {
        switch (type)
        {
            case SoldierType.InProperty:
                soldiers -= SoldiersToRemove;
                break;
            case SoldierType.Enemy:
                EnemySoldiers -= SoldiersToRemove;
                break;
            case SoldierType.ToGetOut:
                SoldiersToGetOut -= SoldiersToRemove;
                break;
        }

    }

    public void UpdateSoldierInfo()
    {
        NumSoldiersTextController numSoldiersTextController = NumSoldier.GetComponent<NumSoldiersTextController>();

        if (dominated) numSoldiersTextController.SetColor(false);
        else numSoldiersTextController.SetColor(true);

        numSoldiersTextController.UpdateText(soldiers.ToString());
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

    private void OnApplicationQuit()
    {
        TimerPanel.OnDayEnd -= OnDayEnd;
    }
}
