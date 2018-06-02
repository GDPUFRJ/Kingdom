using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBox : MonoBehaviour {
    [SerializeField] private Text title;
    [SerializeField] private Text level;
    [SerializeField] private Image icon;

    private Property property;

    public void SetInformation(string name, string level, Sprite icon, Property prop)
    {
        if(icon != null)
            this.icon.sprite = icon;
        this.title.text = name;
        this.level.text = "Level " + level;
        this.property = prop;
    }
}
