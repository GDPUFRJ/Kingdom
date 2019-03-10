using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class KHappeningManager:Singleton < KHappeningManager >
{
    protected KHappeningManager() {}

    // Chance de ocorrer um acontecimento de qualquer raridade em um dado turno
    public float HappenningChance = 0.3f;

    // Chances de ocorrer um acontecimento de certa raridade, dado que ocorrerá um acontecimento em um dado turno
    // As probabilidades abaixo precisam somar 1
    public float ChanceMuitoComum = 0.4f;
    public float ChanceComum = 0.3f;
    public float ChanceNormal = 0.2f;
    public float ChanceRaro = 0.07f;
    public float ChanceMuitoRaro = 0.03f;

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
	
    private bool AttemptToGenerateNewKHappening()
    {
        float value = Random.value;
        if (value < HappenningChance)
        {
            return GenerateNewKHappening();
        }
        else
        {
            return false;
        }
    }

    private bool GenerateNewKHappening()
    {
        float value = Random.value;
        List<KHappening> SelectedList = new List<KHappening>();

        if (value > 1 - ChanceMuitoComum)
        {
            SelectedList = HappeningsByRarity[0];
        }
        else if (value > 1 - ChanceMuitoComum - ChanceComum)
        {
            SelectedList = HappeningsByRarity[1];
        }
        else if (value > 1 - ChanceMuitoComum - ChanceComum - ChanceNormal)
        {
            SelectedList = HappeningsByRarity[2];
        }
        else if (value > 1 - ChanceMuitoComum - ChanceComum - ChanceNormal - ChanceRaro)
        {
            SelectedList = HappeningsByRarity[3];
        }
        else
        {
            SelectedList = HappeningsByRarity[4];
        }

        if (SelectedList.Count == 0)
        {
            return false;
        }

        KHappening selectedHappening = SelectedList[Random.Range(0, 1000000) % SelectedList.Count];

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
