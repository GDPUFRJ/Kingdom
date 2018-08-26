using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class HappeningWindowAnswer:MonoBehaviour {
    [SerializeField] private Text description; 
    [SerializeField] private KEvent kevent;
    [SerializeField] private HappeningWindow happeningWindow;
	
    public void SetAnswer(KAnswer ans) {
        string s = ans.answer;
        this.description.text = s; 
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
        KEventManager.Instance.FireEvent(kevent, Intensity.light);
        happeningWindow.DestroyWindow();
    }
}
