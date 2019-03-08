using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : Singleton<HappinessManager> {

    protected HappinessManager() { }

    GameManager gm;
    PropertyManager pm;


	// Use this for initialization
	void Start () {
        TimerPanel.OnAfterDayEnd += TimerPanel_OnAfterDayEnd;

        gm = GameManager.Instance;
        pm = PropertyManager.Instance;
	}

    private void TimerPanel_OnAfterDayEnd()
    {
        float NewHappyness = 0;
        int dominatedProperties = 0;
        foreach(Property p in pm.Propriedades)
        {
            if (p.dominated == true)
            {
                NewHappyness += p.happiness;
                dominatedProperties++;
            }
        }

        gm.Happiness = NewHappyness / dominatedProperties;
        gm.Happiness += gm.HappinessNextEventModifier;
    }



    // Update is called once per frame
    void Update () {
		
	}
}
