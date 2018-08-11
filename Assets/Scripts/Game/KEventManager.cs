using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KEventManager : Singleton<KEventManager> {

    protected KEventManager() { }

    public int MaxActiveEvent = 3;

    public int MuitoComumChance = 20;
    public int ComumChance = 20;
    public int NormalChance = 20;
    public int RaroChance = 20;
    public int MuitoRaroChance = 20;

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
        print("ON AFTER DAY END");

        int BuildingNext = GameManager.Instance.BuildingNext;
        int FoodNext = GameManager.Instance.FoodNext;
        int GoldNext = GameManager.Instance.GoldNext;

        KEvent.Battle battle = KEvent.Battle.Allowed;

        List<KEvent> ToRemove = new List<KEvent>();

        foreach(KEvent kevt in KEventsActives)
        {
            kevt.LeftDuration--;

            if(kevt.LeftDuration == -1)
            {
                ToRemove.Add(kevt);
                continue;
            }
                

            BuildingNext = kevt.GetNextResource(Resource.Building);
            FoodNext = kevt.GetNextResource(Resource.Food);
            GoldNext = kevt.GetNextResource(Resource.Gold);

            if (kevt.battle != KEvent.Battle.Allowed)
                battle = KEvent.Battle.NotAllowed;
        }

        foreach(KEvent kevt in ToRemove)
        {
            KEventsActives.Remove(kevt);
        }

        GameManager.Instance.BuildingNextEventModifier = BuildingNext;
        GameManager.Instance.FoodNextEventModifier = FoodNext;
        GameManager.Instance.GoldNextEventModifier = GoldNext;

        GameManager.Instance.BuildingNext += BuildingNext;
        GameManager.Instance.FoodNext += FoodNext;
        GameManager.Instance.GoldNext += GoldNext;

        GameManager.Instance.CanBattle = battle == KEvent.Battle.Allowed ? true : false;

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
        //To be modified
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

    public List<KEvent> GetAllActiveEvents()
    {
        return KEventsActives;
    }
}