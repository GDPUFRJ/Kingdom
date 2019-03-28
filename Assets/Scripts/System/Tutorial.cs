using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Flags")]
    [Tooltip("True when the Tutorial should be prompted at the Start routine")]
    [SerializeField] private bool promptAtStart = true;
    [Tooltip("True when the Tutorial GameObject is in the Game Scene")]
    [SerializeField] private bool thisIsTheGameScene = true;

    [Header("UI Elements")]
    [SerializeField] private GameObject[] screens;

    private int currentScreen;

    private void Start()
    {
        HideAllScreens();

        if (promptAtStart)
        {
            Prompt();
        }
    }

    private void Prompt()
    {
        currentScreen = 0;

        if (thisIsTheGameScene)
        {
            TimerPanel.SetPause(true);
        }

        ShowScreen(currentScreen);
    }

    public void StartTutorial()
    {
        currentScreen = 1;

        if (thisIsTheGameScene)
        {
            TimerPanel.SetPause(true);
        }

        ShowScreen(currentScreen);
    }

    private void FinishTutorial()
    {
        HideAllScreens();

        if (thisIsTheGameScene)
        {
            TimerPanel.SetPause(false);
        }
    }

    private void NextScreen()
    {
        if (currentScreen < screens.Length - 1)
        {
            HideScreen(currentScreen);
            currentScreen++;
            ShowScreen(currentScreen);
        }

        else if (currentScreen == screens.Length - 1)
        {
            FinishTutorial();
        }
    }

    private void HideAllScreens()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            HideScreen(i);
        }
    }

    private void ShowScreen(int screenIndex)
    {
        if (currentScreen < screens.Length)
        {
            screens[screenIndex].SetActive(true);
        }
    }

    private void HideScreen(int screenIndex)
    {
        if (currentScreen < screens.Length)
        {
            screens[screenIndex].SetActive(false);
        }
    }

    public void OnNextButtonClicked()
    {
        NextScreen();
    }

    public void OnCloseTutorialButtonClicked()
    {
        FinishTutorial();
    }
}
