using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHappeningManager : Singleton<KHappeningManager> {

    protected KHappeningManager() { }

    public List<KHappening> KHappenings = new List<KHappening>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
