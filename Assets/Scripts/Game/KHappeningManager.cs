using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class KHappeningManager:Singleton < KHappeningManager >  {

    // Esta variável serve apenas para propósitos de debug. Ela deve ser setada como true no inspector quando você quiser que um acontecimento ocorra
    public bool forceHappeningNextTurn = false;

    protected KHappeningManager() {}

    public List < KHappening > KHappenings = new List < KHappening > (); 

    public List < KHappening > KHappeningsHistory = new List < KHappening > (); 


    private List < KHappening > MuitoComumHappenings = new List < KHappening > (); 
    private List < KHappening > ComumHappenings = new List < KHappening > (); 
    private List < KHappening > NormalHappenings = new List < KHappening > (); 
    private List < KHappening > RaroHappenings = new List < KHappening > (); 
    private List < KHappening > MuitoRaroHappenings = new List < KHappening > ();

    private List<KHappening> ManualHappenings = new List<KHappening>();

    private List < List < KHappening >> HappeningsByRarity = new List < List < KHappening >> (); 

    // Use this for initialization
    void Start () {
        TimerPanel.OnBattleEnded += OnBattlesEnded; 

        HappeningsByRarity.Add(MuitoComumHappenings); 
        HappeningsByRarity.Add(ComumHappenings); 
        HappeningsByRarity.Add(NormalHappenings); 
        HappeningsByRarity.Add(RaroHappenings); 
        HappeningsByRarity.Add(MuitoRaroHappenings); 

        foreach (KHappening khpp in KHappenings) {
            switch (khpp.chance) {
                case Chance.Manual:
                    ManualHappenings.Add(khpp);
                    break;
                case Chance.MuitoRaro:
                    MuitoRaroHappenings.Add(khpp); 
                    break; 
                case Chance.Raro:
                    RaroHappenings.Add(khpp); 
                    break; 
                case Chance.Normal:
                    NormalHappenings.Add(khpp); 
                    break; 
                case Chance.Comum:
                    ComumHappenings.Add(khpp); 
                    break; 
                case Chance.MuitoComum:
                    MuitoComumHappenings.Add(khpp); 
                    break; 
            }
        }
    }
	
    private bool AttemptToGenerateNewKHappening() {

        if (forceHappeningNextTurn)
        {
            forceHappeningNextTurn = false;
            return GenerateNewKHappening();
        }

        //To be modified
        if (UnityEngine.Random.Range(0, 1000000) < 750000)
            return false;

        return GenerateNewKHappening();
    }

    private bool GenerateNewKHappening()
    {
        bool hasSelectedAList = false;
        List<KHappening> SelectedList = new List<KHappening>();
        while (!hasSelectedAList)
        {
            int rarity = Random.Range(0, HappeningsByRarity.Count);

            SelectedList = HappeningsByRarity[rarity];

            //Isto está aqui para repetir o sorteio, caso nao tenha acontecimentos de uma determinada raridade
            if (SelectedList.Count > 0) hasSelectedAList = true;
        }

        KHappening selectedHappening = SelectedList[UnityEngine.Random.Range(0, 1000000) % SelectedList.Count];

        return FireHappening(selectedHappening);
    }

    public bool FireHappening(KHappening khpp) {
        if (khpp != null) {
            KHappening HappeningToAdd = Instantiate(khpp); //creates a copy of the Happening
            KHappeningsHistory.Add(HappeningToAdd);

            //PEDRO, CHAME O QUE VOCE PRECISA AQUI
            GameObject window = Instantiate(GameManager.Instance.happeningWindowPrefab, GameManager.Instance.CanvasHUD); 
            window.GetComponent < HappeningWindow > ().SetHappening(khpp);
            window.GetComponent<HappeningWindow>().UpdateInfo();

            return true; 
        }
        else
            return false; 
    }

    public bool FireHappening(string name) {
        return FireHappening(FindHappeningByName(name)); 
    }

    private KHappening FindHappeningByName(string name) {
        foreach (KHappening khpp in KHappenings) {
            if (khpp.name.Equals(name))
                return khpp; 
        }

        return null; 
    }

    private void OnBattlesEnded() {
        AttemptToGenerateNewKHappening(); 
    }
}
