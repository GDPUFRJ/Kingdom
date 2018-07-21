using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateBox : MonoBehaviour {
    private const string DATE_TEXT = "DIA ";

    [SerializeField] private Text date;

    public void SetInformation(int day)
    {
        date.text = DATE_TEXT + day;
    }
}
