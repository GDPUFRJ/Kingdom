using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
using DG.Tweening; 

public class HappeningWindow:MonoBehaviour {
	[SerializeField] private Text title; 
    [SerializeField] private Text description; 

    [SerializeField] private Transform answersRoot; 
    [SerializeField] private GameObject answerPrefab; 

    private KHappening happening; 

    public void SetHappening(KHappening happening) {
        this.happening = happening; 
    }
	public void UpdateInfo() {
        int answerCount = happening.Answers.Count; 
        title.text = happening.Name; 
        description.text = happening.Description; 
        for (int i = 0; i < happening.Answers.Count; i++) {
            GameObject ans = Instantiate(GameManager.Instance.answerPrefab, answersRoot); 
            ans.GetComponent < HappeningWindowAnswer > ().SetAnswer(happening.Answers[i]); 
        }
    }

}
