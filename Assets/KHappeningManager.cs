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
                case KHappening.Chance.MuitoRaro:
                    MuitoRaroHappenings.Add(khpp); 
                    break; 
                case KHappening.Chance.Raro:
                    RaroHappenings.Add(khpp); 
                    break; 
                case KHappening.Chance.Normal:
                    NormalHappenings.Add(khpp); 
                    break; 
                case KHappening.Chance.Comum:
                    ComumHappenings.Add(khpp); 
                    break; 
                case KHappening.Chance.MuitoComum:
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
        //if (UnityEngine.Random.Range(0, 1000000) < 750000)
            //return false; 

        //int rarity = UnityEngine.Random.Range(0, 1000000) % 5; 

        //List < KHappening > SelectedList = HappeningsByRarity[rarity];

        //KHappening selectedHappening = SelectedList[UnityEngine.Random.Range(0, 1000000) % SelectedList.Count];
        KHappening selectedHappening = MuitoComumHappenings[0];
        return FireHappening(selectedHappening); 

    }

    public bool FireHappening(KHappening khpp) {
        if (khpp != null) {
            KHappening HappeningToAdd = Instantiate(khpp); //creates a copy of the Happening
            KHappeningsHistory.Add(HappeningToAdd); 

            //PEDRO, CHAME O QUE VOCE PRECISA AQUI
            GameObject window = Instantiate(GameManager.Instance.happeningWindowPrefab, GameManager.Instance.canvasRoot); 
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
