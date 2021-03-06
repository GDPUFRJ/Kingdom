﻿using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class HappeningWindowAnswer:MonoBehaviour {
    [SerializeField] private Text description; 
    [SerializeField] private KEvent kevent;
    [SerializeField] private Intensity eventIntensity;
    [SerializeField] private HappeningWindow happeningWindow;
	
    public void SetAnswer(KAnswer ans) {
        string s = TranslationManager.GameLanguage == Language.Portuguese ? ans.portugueseAnswer : ans.englishAnswer;
        this.description.text = s;
        this.eventIntensity = ans.intensity;
    }
    public void SetEvent(KEvent eve)
    {
        this.kevent = eve;
    }
    public void SetHappeningWindow(HappeningWindow window)
    {
        happeningWindow = window;
    }
    public void ActivateEvent()
    {
        KEventManager.Instance.FireEvent(kevent, eventIntensity);
        FMODPlayer.Instance.Play("ui click");
        happeningWindow.DestroyWindow();
    }
}
