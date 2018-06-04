using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

    public Line newLine;

	// Use this for initialization
	void Start () {

        //try //DAR UM JEITO DE REMOVER ESSE TRY-CATCH DAQUI
        //{
            
        //}
        //catch (System.Exception) { }
    }

    private void OnEnable()
    {
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

    // Update is called once per frame
    void Update () {
		
	}
}
