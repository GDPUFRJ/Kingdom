using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineManager : MonoBehaviour
{
    
    public Line newLine;
    private List<Line> lines = new List<Line>();
    
    public void BuildLines(List<Property> properties)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
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
