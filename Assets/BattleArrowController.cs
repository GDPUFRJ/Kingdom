using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleArrowController : MonoBehaviour {

    public Property Source;
    public Property Destination;

    private int SoldiersToBeTransfered = 0;

    private BattleArrowController OpositeArrow;

    private Text ArrowText;

    public GameObject NumSoldierFather;

    // Use this for initialization
    void Start () {
        TimerPanel.OnDayEnd += TimerPanel_OnDayEnd;
        ArrowText = GetComponentInChildren<Text>();
        UpdateArrowText();
    }

    private void TimerPanel_OnDayEnd()
    {
        if (Destination == null) return;
        if (Destination.dominated) Destination.soldiers += SoldiersToBeTransfered;
        else Destination.EnemySoldiers += SoldiersToBeTransfered;
        SoldiersToBeTransfered = 0;
        UpdateArrowText();
        Source.UpdateSoldierInfo();
        Destination.UpdateSoldierInfo();
    }

    // Update is called once per frame
    void Update () {
        SetPosition();

    }

    void SetPosition()
    {
        if (Source == null || Destination == null) return;

        float CamSize = Camera.main.orthographicSize;

        Vector3 SourceOnScreen = Camera.main.WorldToScreenPoint(Source.transform.position);
        Vector3 DestinationOnScreen = Camera.main.WorldToScreenPoint(Destination.transform.position);

        Vector3 MediumPoint = (SourceOnScreen + DestinationOnScreen) / 2;
        transform.position = FindIdealPosition(SourceOnScreen, 350/CamSize, MediumPoint);
        float NewFloat = Map(CamSize, 1, 10, 4, 1.5f);
        transform.localScale = new Vector3(NewFloat, NewFloat, NewFloat);

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

        if (OpositeArrow.SoldiersToBeTransfered > 0 && !tag.Equals("battle"))
        {
            OpositeArrow.SoldiersToBeTransfered--;
            OpositeArrow.Source.SoldiersToGetOut--;
            OpositeArrow.UpdateArrowText();
        }

        else if (Source.SoldiersToGetOut < Source.soldiers && !tag.Equals("abort"))
        {
            this.Source.SoldiersToGetOut++;
            this.SoldiersToBeTransfered++;
            this.UpdateArrowText();
        }

        Source.UpdateSoldierInfo();
        Destination.UpdateSoldierInfo();
    }

    public void UpdateArrowText()
    {
        if (SoldiersToBeTransfered == 0) ArrowText.text = " ";
        else ArrowText.text = SoldiersToBeTransfered.ToString();
    }
}
