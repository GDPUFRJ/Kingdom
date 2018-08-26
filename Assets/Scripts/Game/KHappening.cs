using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KHappening : ScriptableObject {

    public string Name = "New Happening";
    public string Description = "Describe it here";
    public string Question = "Ask a Question";

    
    public Chance chance = Chance.Normal;
    public List<KAnswer> Answers = new List<KAnswer>();

    public bool showInInspector = true;
    public bool showAnswers = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
