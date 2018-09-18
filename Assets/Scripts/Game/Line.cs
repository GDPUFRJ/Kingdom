using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Line : MonoBehaviour, IEdge<Line>
{
    Line IEdge<Line>.Data { get; set; }

    public Property start;
    public Property end;

    private LineRenderer lr;

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
    }

    public void UpdateLineColor()
    {
        lr = GetComponent<LineRenderer>();

        if (start.dominated == false || end.dominated == false)
        {
            lr.startColor = PropertyManager.Instance.NotDominatedLine;
            lr.endColor = PropertyManager.Instance.NotDominatedLine;
        }
        else
        {
            lr.startColor = PropertyManager.Instance.DominatedLine;
            lr.endColor = PropertyManager.Instance.DominatedLine;
        }
    }
}
