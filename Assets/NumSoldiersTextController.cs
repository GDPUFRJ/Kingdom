using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumSoldiersTextController : MonoBehaviour {

    public Transform Owner;
    private Text textComponent;

    public Color AllyTextColor;
    public Color EnemyTextColor;

    // Use this for initialization
	void Start () {
        textComponent = GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Owner == null) return;
		//this.transform.position = Camera.main.WorldToScreenPoint(Owner.position);
    }

    public void UpdateText(string num)
    {
        textComponent.text = num;
    }

    public void SetColor(bool isEnemy)
    {
        if(textComponent == null) textComponent = GetComponent<Text>();
        textComponent.color = isEnemy ? EnemyTextColor : AllyTextColor; 
    }
}
