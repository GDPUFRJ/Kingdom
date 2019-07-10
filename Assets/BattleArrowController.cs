using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BattleArrowController : MonoBehaviour {

    public Property Source;
    public Property Destination;

    [Range(0.01f,1.0f)]public float PropertyDistance = 0.45f;

    private int SoldiersToBeTransfered = 0;
    public int GetSoldiersToBeTransfered() { return SoldiersToBeTransfered; }
    public void SetSoldiersToBeTransfered(int soldiers) { SoldiersToBeTransfered = soldiers; }

    private ArrowType tipo = ArrowType.Arrow;

    public Sprite Arrow;
    public Sprite Battle;
    public Sprite Abort;

    private BattleArrowController OpositeArrow;

    private Text ArrowText;

    [HideInInspector]public GameObject NumSoldierFather;

    // Use this for initialization
    void Start () {
        TimerPanel.OnDayEnd += TimerPanel_OnDayEnd;
        ArrowText = GetComponentInChildren<Text>();
        UpdateArrowText();
    }

    private void TimerPanel_OnDayEnd()
    {
        if (Destination == null) return;
        if (Destination.dominated) Destination.AddSoldiers(SoldierType.InProperty, SoldiersToBeTransfered);
        else
        {
            Destination.AddSoldiers(SoldierType.Enemy, SoldiersToBeTransfered, new BattleInformation(Source.kingdom, Destination.kingdom, SoldiersToBeTransfered, Destination.GetSoldiers(SoldierType.InProperty)));
        }
        SoldiersToBeTransfered = 0;
        UpdateArrowText();
        Source.UpdateSoldierInfo();
        Destination.UpdateSoldierInfo();
    }

    public void SetTipo(ArrowType type)
    {
        this.tipo = type; 
        switch (type)
        {
            case ArrowType.Arrow:
                GetComponent<Image>().enabled = true;
                GetComponent<Image>().sprite = Arrow;
                break;
            case ArrowType.Battle:
                GetComponent<Image>().enabled = true;
                GetComponent<Image>().sprite = Battle;
                break;
            case ArrowType.Abort:
                GetComponent<Image>().enabled = false;
                GetComponent<Image>().sprite = Abort;
                break;
            case ArrowType.Disabled:
                GetComponent<Image>().enabled = false;
                break;
        }
        
    }

    public ArrowType GetTipo() { return tipo; }

    public void SetPosition()
    {
        if (Source == null || Destination == null) return;

        float CamSize = Camera.main.orthographicSize;

        Vector3 SourceOnScreen = Source.transform.position;
        //Vector3 DestinationOnScreen = Destination.transform.position;
        Vector3 DestinationOnScreen = Source.GetArrowDirection(Destination.transform).position;

        Vector3 MediumPoint = (SourceOnScreen + DestinationOnScreen) / 2;
        transform.position = FindIdealPosition(SourceOnScreen, 0.45f, MediumPoint);

        float angle = Mathf.Atan2(SourceOnScreen.y - DestinationOnScreen.y, SourceOnScreen.x - DestinationOnScreen.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

    }

    public float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public void SetSourceAndDestination(Property source, Property destination)
    {
        this.Source = source;
        this.Destination = destination;
    }

    private Vector2 FindIdealPosition(Vector3 circleCenter, float radius, Vector3 SetaPos)
    {
        Vector3 NewPos = SetaPos - circleCenter;
        NewPos = NewPos.normalized * radius + circleCenter;

        return NewPos;
    }

    public void OnClick()
    {
        if (OpositeArrow == null)
        {
            OpositeArrow = Destination.ArrowsComingOut.Find(x => x.Destination == this.Source);
        }

        SendTroopsBox.Instance.Activate(this, OpositeArrow);

        FMODPlayer.Instance.Play("arrow");

        // Cancelar transferência de soldados
        //if (OpositeArrow.SoldiersToBeTransfered > 0 && tipo != ArrowType.Battle)
        //{
            //OpositeArrow.SoldiersToBeTransfered--;
            //OpositeArrow.Source.RemoveSoldiers(SoldierType.ToGetOut, 1);
            //OpositeArrow.UpdateArrowText();
        //}

        // Transferir soldados
        //else if (Source.GetSoldiers(SoldierType.ToGetOut) < Source.GetSoldiers(SoldierType.InProperty) 
        //          && tipo != ArrowType.Abort)
        //{
            //this.Source.AddSoldiers(SoldierType.ToGetOut, 1);
            //this.SoldiersToBeTransfered++;
            //this.UpdateArrowText();
        //}

        //Source.UpdateSoldierInfo();
        //Destination.UpdateSoldierInfo();
    }

    public void UpdateArrowText()
    {
        if (SoldiersToBeTransfered == 0) ArrowText.text = " ";
        else ArrowText.text = SoldiersToBeTransfered.ToString();
    }

    public void CreateSoldierButton()
    {
        if (Source.dominated && Destination.dominated)
            SetTipo(ArrowType.Arrow);
        else if (Source.dominated == false && Destination.dominated == true)
            SetTipo(ArrowType.Abort);
        else if (Source.dominated && Destination.dominated == false)
            SetTipo(ArrowType.Battle);
        else if (Source.dominated == false && Destination.dominated == false)
            SetTipo(ArrowType.Disabled);
    }

    public void UpdateSoldierButton(int callNumber = 0)
    {
        CreateSoldierButton();

        if (OpositeArrow == null)
        {
            OpositeArrow = Destination.ArrowsComingOut.Find(x => x.Destination == this.Source);
        }
        if (callNumber > 0) return; //AVOID STACKOVERFLOW
        OpositeArrow.UpdateSoldierButton(1);
    }
}
