using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineManager : MonoBehaviour
{
    public Line newLine;
    public List<Line> lines = new List<Line>();
    private List<Line> toRemove = new List<Line>();

#if UNITY_EDITOR
    private void Update()
    {
        RemoveAnyInvalidLine();
    }
#endif

    public void AddLine(Property start, Property end)
    {
        newLine.GetComponent<Line>().start = start;
        newLine.GetComponent<Line>().end = end;

        Line line = Instantiate(newLine, transform);

        start.Linhas.Add(line); //grafo
        end.Linhas.Add(line);   //grafo

        lines.Add(line);
    }

    public void UpdateRelatedLines(Property property)
    {
        lines = new List<Line>(GetComponentsInChildren<Line>());
        foreach (Line line in lines)
        {
            if (line.start.Equals(property) || line.end.Equals(property))
                line.UpdateLineColor();
        }
    }

    public void RemoveAnyLineConnecting(Property p1, Property p2)
    {
        lines = new List<Line>(GetComponentsInChildren<Line>());
        foreach (Line line in lines)
        {
            if (line.start.Equals(p1) && line.end.Equals(p2))
                toRemove.Add(line);
            if (line.start.Equals(p2) && line.end.Equals(p1))
                toRemove.Add(line);
        }

        foreach (Line line in toRemove)
        {
            lines.Remove(line);
            DestroyImmediate(line.gameObject);
        }

        toRemove.Clear();
    }

    public void RemoveAnyInvalidLine()
    {
        lines = new List<Line>(GetComponentsInChildren<Line>());
        foreach (Line line in lines)
        {
            if (line.start == null || line.end == null)
                toRemove.Add(line);
        }

        foreach (Line line in toRemove)
        {
            lines.Remove(line);
            DestroyImmediate(line.gameObject);
        }
        
        toRemove.Clear();
    }
    
    [Obsolete("Por favor, não usa isso aqui, tá aqui só pq deu trabalho de escrever.")]
    public void BuildLines(List<Property> properties)
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (Property p in properties)
        {
            foreach (Property p2 in p.Neighbors)
            {
                if (FindDup(p, p2)) continue;

                newLine.GetComponent<Line>().start = p;
                newLine.GetComponent<Line>().end = p2;
                lines.Add(Instantiate(newLine, transform));
            }
        }
    }

    bool FindDup(Property start, Property end)
    {
        foreach (Line l in lines)
        {
            if (l.start.Equals(start) && l.end.Equals(end))
                return true;
            if (l.start.Equals(end) && l.end.Equals(start))
                return true;
        }
        return false;
    }
}
