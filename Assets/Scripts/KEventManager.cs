using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KEventManager : Singleton<KEventManager> {

    protected KEventManager() { }

    public List<KEvent> KEvents = new List<KEvent>();

    private List<KEvent> ManualEvents = new List<KEvent>();
    private List<KEvent> MuitoComumEvents = new List<KEvent>();
    private List<KEvent> ComumEvents = new List<KEvent>();
    private List<KEvent> NormalEvents = new List<KEvent>();
    private List<KEvent> RaroEvents = new List<KEvent>();
    private List<KEvent> MuitoRaroEvents = new List<KEvent>();

    // Use this for initialization
    void Start() {

        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;

        foreach (KEvent kevt in KEvents)
        {
            switch (kevt.chance)
            {
                case KEvent.Chance.Manual:
                    ManualEvents.Add(kevt);
                    break;
                case KEvent.Chance.MuitoRaro:
                    MuitoRaroEvents.Add(kevt);
                    break;
                case KEvent.Chance.Raro:
                    RaroEvents.Add(kevt);
                    break;
                case KEvent.Chance.Normal:
                    NormalEvents.Add(kevt);
                    break;
                case KEvent.Chance.Comum:
                    ComumEvents.Add(kevt);
                    break;
                case KEvent.Chance.MuitoComum:
                    MuitoComumEvents.Add(kevt);
                    break;
            }
        }
    }

    private void OnAfterDayEnd()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private bool FireEvent(KEvent kevt, KEvent.Intensity intensity)
    {
        if (kevt != null)
        {

            return true;
        }
        else
            return false;
    }

    public bool FireEvent(string name, KEvent.Intensity intensity)
    {
        return FireEvent(FindEventByName(name), intensity);
    }
    
    private KEvent FindEventByName(string name)
    {
        foreach(KEvent kevt in KEvents)
        {
            if (kevt.name.Equals(name))
                return kevt;
        }

        return null;
    }

}