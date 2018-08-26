using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class KHappeningManager:Singleton < KHappeningManager >  {

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
        TimerPanel.OnAfterDayEnd += OnAfterDayEnd; 

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
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool GenerateNewKHappening() {
        //To be modified
        if (UnityEngine.Random.Range(0, 1000000) < 750000)
            return false; 

        int rarity = UnityEngine.Random.Range(0, 1000000) % 5; 

        List < KHappening > SelectedList = HappeningsByRarity[rarity];

        //Isto está aqui para evitar uma divisao por zero, caso nao tenha acontecimentos de uma determinada raridade
        if (SelectedList.Count == 0) return FireHappening((KHappening)null);

        KHappening selectedHappening = SelectedList[UnityEngine.Random.Range(0, 1000000) % SelectedList.Count];

        return FireHappening(selectedHappening); 

    }

    public bool FireHappening(KHappening khpp) {
        if (khpp != null) {
            KHappening HappeningToAdd = Instantiate(khpp); //creates a copy of the Happening
            KHappeningsHistory.Add(HappeningToAdd);

            //PEDRO, CHAME O QUE VOCE PRECISA AQUI
            TimerPanel.SetPause(true);
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

    private void OnAfterDayEnd() {
        GenerateNewKHappening(); 
    }
}
