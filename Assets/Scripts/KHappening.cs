﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHappening : ScriptableObject {

    public string Name = "New Happening";
    public string Description = "Describe it here";
    public string Question = "Ask a Question";

    public enum Chance { MuitoRaro = 1, Raro = 2, Normal = 3, Comum = 4, MuitoComum = 5}
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
