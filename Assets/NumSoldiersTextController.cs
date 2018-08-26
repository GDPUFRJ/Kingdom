using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumSoldiersTextController : MonoBehaviour {

    public Transform Owner;
    private Text textComponent;

    public Color AllyTextColor;
    public Color EnemyTextColor;

    public void UpdateText(string num)
    {
        if (textComponent == null) textComponent = GetComponent<Text>();
        textComponent.text = num;
    }

    public void SetColor(bool isEnemy)
    {
        if(textComponent == null) textComponent = GetComponent<Text>();
        textComponent.color = isEnemy ? EnemyTextColor : AllyTextColor; 
    }
}
