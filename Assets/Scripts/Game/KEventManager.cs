using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KEventManager : Singleton<KEventManager> {

    protected KEventManager() { }

    //public int MaxActiveEvent = 3;

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
                case Chance.Manual:
                    ManualEvents.Add(kevt);
                    break;
                case Chance.MuitoRaro:
                    MuitoRaroEvents.Add(kevt);
                    break;
                case Chance.Raro:
                    RaroEvents.Add(kevt);
                    break;
                case Chance.Normal:
                    NormalEvents.Add(kevt);
                    break;
                case Chance.Comum:
                    ComumEvents.Add(kevt);
                    break;
                case Chance.MuitoComum:
                    MuitoComumEvents.Add(kevt);
                    break;
            }
        }
    }

    private void OnAfterDayEnd()
    {
        //print("ON AFTER DAY END");

        //int BuildingNext = GameManager.Instance.BuildingNext;
        //int FoodNext = GameManager.Instance.FoodNext;
        //int GoldNext = GameManager.Instance.GoldNext;
        int BuildingNextEventModifier = 0;
        int FoodNextEventModifier = 0;
        int GoldNextEventModifier = 0;
        int PopulationNextEventModifier = 0;
        float HappinessNextEventModifier = 0;

        Battle battle = Battle.Allowed;

        List<KEvent> ToRemove = new List<KEvent>();

        foreach(KEvent kevt in KEventsActives)
        {
            kevt.LeftDuration--;

            if(kevt.LeftDuration == -1)
            {
                ToRemove.Add(kevt);
                continue;
            }


            BuildingNextEventModifier += kevt.GetNextResource(Resource.Building);
            FoodNextEventModifier += kevt.GetNextResource(Resource.Food);
            GoldNextEventModifier += kevt.GetNextResource(Resource.Gold);
            PopulationNextEventModifier += kevt.GetPopulationModifier();
            HappinessNextEventModifier += kevt.GetHappinessModifier();

            if (kevt.battle != Battle.Allowed)
                battle = Battle.NotAllowed;
        }

        foreach(KEvent kevt in ToRemove)
        {
            KEventsActives.Remove(kevt);
        }

        // Por enquanto, essas três próximas linhas só servem para mostrar no Inspector do Game Manager que o evento está tendo efeito
        GameManager.Instance.BuildingNextEventModifier = BuildingNextEventModifier;
        GameManager.Instance.FoodNextEventModifier = FoodNextEventModifier;
        GameManager.Instance.GoldNextEventModifier = GoldNextEventModifier;
        GameManager.Instance.PopulationNextEventModifier = PopulationNextEventModifier;
        // Para o atributo satisfação, aqui eu registro a intenção de modificá-lo, e a modificação só entra em efeito no script HappinessManager
        GameManager.Instance.HappinessNextEventModifier = HappinessNextEventModifier;

        // Essas três próximas linhas servem para mostrar visualmente no Top Bar Info que o evento está tendo efeito sobre os recursos
        GameManager.Instance.BuildingNext += BuildingNextEventModifier;
        GameManager.Instance.FoodNext += FoodNextEventModifier;
        GameManager.Instance.GoldNext += GoldNextEventModifier;

        // É aqui que ocorre efetivamente a aplicação dos efeitos do evento, para esses recursos
        GameManager.Instance.Building += BuildingNextEventModifier;
        GameManager.Instance.Food += FoodNextEventModifier;
        GameManager.Instance.Gold += GoldNextEventModifier;
        GameManager.Instance.Population += PopulationNextEventModifier;

        GameManager.Instance.CanBattle = battle == Battle.Allowed ? true : false;

    }

    public bool FireEvent(KEvent kevt, Intensity intensity)
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

    public bool FireEvent(string name, Intensity intensity)
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

    // Esse método não está sendo usado
    private bool GenerateNewKEvent()
    {
        //To be modified
        //if (MaxActiveEvent < KEventsActives.Count)
        //    return false;

        if (UnityEngine.Random.Range(0, 1000000) < 750000)
            return false;

        int rarity = UnityEngine.Random.Range(0, 1000000) % 5;
        int intensity = UnityEngine.Random.Range(0, 1000000) % 3;

        List<KEvent> SelectedList = EventsByRarity[rarity];

        KEvent selectedEvent = SelectedList[UnityEngine.Random.Range(0, 1000000) % SelectedList.Count];

        return FireEvent(selectedEvent, (Intensity)intensity);

    }

    public List<KEvent> GetAllActiveEvents()
    {
        return KEventsActives;
    }

    public void AddActiveEvent(KEvent kevt)
    {
        KEventsActives.Add(kevt);
    }

    public void ClearActiveEvents()
    {
        KEventsActives = new List<KEvent>();
    }
}