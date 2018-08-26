using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAnswer : ScriptableObject
{
    public string answer = "New Answer";
    public KEvent answerEvent;
    public Intensity intensity = Intensity.light;

    public bool showInInspector = true;
}