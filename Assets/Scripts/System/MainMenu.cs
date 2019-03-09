using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
    private const float FADE_DURATION = 0.5f;
    private const float MAP_SPEED = 5f;
    [SerializeField] private List<CanvasGroup> groups;
    [SerializeField] private CanvasGroup transitionPanel;
    [SerializeField] private LoadingPanel loadingPanel;

    private void Start()
    {
        transitionPanel.DOFade(1, 0);
        transitionPanel.DOFade(0, FADE_DURATION * 3);
        ToggleGroup(groups[0], true, 0);
        ToggleGroup(groups[1], false, 0);
        ToggleGroup(groups[2], false, 0);
    }
    public void TouchToStart()
    {
        ToggleGroup(groups[0], false);
        ToggleGroup(groups[1], true);
    }
    public void Continue()
    {
        if (SaveSystem.CheckSaveDataExistence())
        {
            SaveSystem.newGame = false;
            loadingPanel.Open("SampleScene");
        }
    }
    public void NewGame()
    {
        SaveSystem.newGame = true;
        SaveSystem.ResetSaveData();
        loadingPanel.Open("SampleScene");
    }
    public void Credits()
    {
        print("Credits");
        ToggleGroup(groups[0], false, FADE_DURATION);
        ToggleGroup(groups[1], false, 0);
        ToggleGroup(groups[2], true, FADE_DURATION);
        groups[2].GetComponent<Animator>().Play("MenuCréditos_OpenScroll");
    }
    public void Options()
    {
        print("Options");
        ToggleGroup(groups[0], false, FADE_DURATION);
        ToggleGroup(groups[1], true, FADE_DURATION);
        ToggleGroup(groups[2], false, 0);
        groups[1].GetComponent<Animator>().Play("MenuOpções_OpenScroll");
    }
    public void CloseOptionsAnimation()
    {
        groups[1].GetComponent<Animator>().Play("MenuOpções_CloseScroll");
    }
    public void GoBackToMenuFromOptions()
    {
        ToggleGroup(groups[0], true, FADE_DURATION);
        ToggleGroup(groups[1], false, 0);
    }
    public void CloseCreditsAnimation()
    {
        groups[2].GetComponent<Animator>().Play("MenuCréditos_CloseScroll");
    }
    public void GoBackToMenuFromCredits()
    {
        ToggleGroup(groups[0], true, FADE_DURATION);
        ToggleGroup(groups[2], false, 0);
    }
    private void ToggleGroup(CanvasGroup group, bool b, float duration = FADE_DURATION)
    {
        float i = b ? 1 : 0;
        group.DOFade(i, duration);
        group.interactable = b;
        group.blocksRaycasts = b;
    }
}
