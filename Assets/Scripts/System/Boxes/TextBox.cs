using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

    [SerializeField] private Text txt;

    public void SetInformation(string text)
    {
        txt.text = text;
    }
}
