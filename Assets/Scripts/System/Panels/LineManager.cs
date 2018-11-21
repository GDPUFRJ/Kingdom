using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

    public GameObject NeighborLine;

	// Use this for initialization
	void Start () {
        Property.OnPropertyStateHasChanged += Property_OnPropertyStateHasChanged;
	}

    private void Property_OnPropertyStateHasChanged()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        PropertyManager.Instance.MapGraph.edges.Clear();

        foreach (Property p in PropertyManager.Instance.MapGraph.vertexes)
        {
            foreach(Property p1 in p.Neighbors)
            {
                Instantiate(NeighborLine, this.transform);                
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
