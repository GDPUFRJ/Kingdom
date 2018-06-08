using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public Property start;
    public Property end;

    private LineRenderer lr;
    /*
    private void OnValidate()
    {
        lr = GetComponent<LineRenderer>();
        if (start || end == null)
            return;
        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);
    }
    */
    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);

        if (start.dominated == false || end.dominated == false)
        {
            lr.startColor = PropertyManager.Instance.NotDominatedLine;
            lr.endColor = PropertyManager.Instance.NotDominatedLine;
        }
            
    }

    public override bool Equals(object other)
    {
        if (start == (other as Line).start && end == (other as Line).end)
            return true;
        else
            return false;

    }
}
