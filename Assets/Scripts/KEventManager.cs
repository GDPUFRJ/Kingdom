using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KEventManager : Singleton<KEventManager> {

    protected KEventManager() { }

    private int MaxActiveEvent = 3;

    public List<KEvent> KEvents = new List<KEvent>();

    private List<KEvent> KEventsActives = new List<KEvent>();

    private List<KEvent> ManualEvents = new List<KEvent>();
    private List<KEvent> MuitoComumEvents = new List<KEvent>();
    private List<KEvent> ComumEvents = new List<KEvent>();
    private List<KEvent> NormalEvents = new List<KEvent>();
    private List<KEvent> RaroEvents = new List<KEvent>();
    private List<KEvent> MuitoRaroEvents = new List<KEvent>();

    private List<List<KEvent>> EventsByRarity = new List<List<KEvent>>();


    // Use this for initialization
    void Start() {

        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;

        EventsByRarity.Add(MuitoComumEvents);
        EventsByRarity.Add(ComumEvents);
        EventsByRarity.Add(NormalEvents);
        EventsByRarity.Add(RaroEvents);
        EventsByRarity.Add(MuitoRaroEvents);

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

    public bool FireEvent(KEvent kevt, KEvent.Intensity intensity)
    {
        if (kevt != null)
        {
            KEvent EventToAdd = Instantiate(kevt);//creates a copy of the event
            EventToAdd.LeftDuration = EventToAdd.Duration;
            EventToAdd.ActiveIntensity = intensity;
            KEventsActives.Add(EventToAdd); 

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

    private bool GenerateNewKEvent()
    {
        if (MaxActiveEvent < KEventsActives.Count)
            return false;

        if (UnityEngine.Random.Range(0, 1000000) < 750000)
            return false;

        int rarity = UnityEngine.Random.Range(0, 1000000) % 5;
        int intensity = UnityEngine.Random.Range(0, 1000000) % 3;

        List<KEvent> SelectedList = EventsByRarity[rarity];

        KEvent selectedEvent = SelectedList[UnityEngine.Random.Range(0, 1000000) % SelectedList.Count];

        return FireEvent(selectedEvent, (KEvent.Intensity)intensity);

    }

}