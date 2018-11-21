using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Line : Edge<Property>, IEquatable<Line>
{
    private LineRenderer lr;
    private bool Available = false;

    public override int GetWeight()
    {
        if (Available)
            return 1000;
        else
            return 1;
    }

    // Use this for initialization
    void Start()
    {


        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, start.transform.position);
        lr.SetPosition(1, end.transform.position);

        if (PropertyManager.Instance == null) return;

        if (start.dominated == false || end.dominated == false)
        {
            lr.startColor = PropertyManager.Instance.NotDominatedLine;
            lr.endColor = PropertyManager.Instance.NotDominatedLine;
        }

        if (PropertyManager.Instance.MapGraph.edges.Contains(this) == false)
            PropertyManager.Instance.MapGraph.edges.Add(this);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (start == null || end == null) return;

        lr = GetComponent<LineRenderer>();
        if (start.transform.hasChanged)
            lr.SetPosition(0, start.transform.position);

        if (end.transform.hasChanged)
            lr.SetPosition(1, end.transform.position);
    }
#endif

    private void OnDestroy()
    {
        start.Neighbors.Remove(end);
        end.Neighbors.Remove(start);

        if (PropertyManager.Instance.MapGraph.edges.Contains(this) == true)
            PropertyManager.Instance.MapGraph.edges.Remove(this);
    }

    public void UpdateLineColor()
    {
        lr = GetComponent<LineRenderer>();

        if (start.dominated == false || end.dominated == false)
        {
            lr.startColor = PropertyManager.Instance.NotDominatedLine;
            lr.endColor = PropertyManager.Instance.NotDominatedLine;

            Available = true;
        }
        else
        {
            lr.startColor = PropertyManager.Instance.DominatedLine;
            lr.endColor = PropertyManager.Instance.DominatedLine;

            Available = false;
        }
    }

    public bool Equals(Line other)
    {
        if (this.start == other.start && this.end == other.end)
            return true;

        if (this.start == other.end && this.end == other.start)
            return true;

        return false;
    }
}
