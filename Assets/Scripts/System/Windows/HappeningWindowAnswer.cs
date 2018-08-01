using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class HappeningWindowAnswer:MonoBehaviour {
    [SerializeField] private Text description; 
    [SerializeField] private KEvent kevent; 
	
    public void SetAnswer(KAnswer ans) {
        string s = ans.answer;
        this.description.text = s; 
        kevent = ans.answerEvent;
        print("A");
    }
}
