using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : Singleton<LineManager> {

    public Line newLine;

    public void BuildLines()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Property p in PropertyManager.Instance.Propriedades)
        {
            foreach (Property p2 in p.Neighbors)
            {
                Instantiate(newLine, transform);
                newLine.GetComponent<Line>().start = p;
                newLine.GetComponent<Line>().end = p2;

            }
        }
    }
}
