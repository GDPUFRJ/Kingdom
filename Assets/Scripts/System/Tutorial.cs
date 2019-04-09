using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [Header("Flags")]
    [Tooltip("True when the Tutorial should be prompted at the Start routine")]
    [SerializeField] private bool promptAtStart = true;
    [Tooltip("True when the Tutorial GameObject is in the Game Scene")]
    [SerializeField] private bool thisIsTheGameScene = true;

    [Header("UI Elements")]
    [Tooltip("References to the screens of the Tutorial. The first screen always needs to be the prompt screen")]
    [SerializeField] private GameObject[] screens;

    private int currentScreen;

    private SectionManager sectionManager;
    private Image background;

    private void Awake()
    {
        sectionManager = FindObjectOfType<SectionManager>();
        background = GetComponent<Image>();
        background.enabled = false;
    }

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

        background.enabled = true;
        StartCoroutine(ShowScreen(currentScreen));
    }

    public void StartTutorial()
    {
        currentScreen = 1;

        if (thisIsTheGameScene)
        {
            TimerPanel.SetPause(true);
        }

        background.enabled = true;
        StartCoroutine(ShowScreen(currentScreen));
    }

    private void FinishTutorial()
    {
        HideAllScreens();
        background.enabled = false;

        if (thisIsTheGameScene)
        {
            TimerPanel.SetPause(false);
        }
    }

    private IEnumerator NextScreen()
    {
        if (currentScreen < screens.Length - 1)
        {
            currentScreen++;
            yield return ShowScreen(currentScreen);
            HideScreen(currentScreen-1);
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

    private IEnumerator ShowScreen(int screenIndex)
    {
        if (currentScreen < screens.Length)
        {
            yield return ProcessCurrentScreen(screenIndex);
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

    private IEnumerator ProcessCurrentScreen(int screenIndex)
    {
        switch(screenIndex)
        {
            case 8:
                HideScreen(currentScreen - 1);
                sectionManager.SelectSection(2);
                yield return new WaitForSeconds(0.4f);
                break;
            case 9:
                HideScreen(currentScreen - 1);
                sectionManager.SelectSection(0);
                yield return new WaitForSeconds(0.4f);
                break;
            case 10:
                HideScreen(currentScreen - 1);
                sectionManager.SelectSection(3);
                yield return new WaitForSeconds(0.4f);
                break;
            case 12:
                HideScreen(currentScreen - 1);
                sectionManager.SelectSection(1);
                background.DOFade(0.6509804f, 0);
                yield return new WaitForSeconds(0.4f);
                break;
            case 13:
                HideScreen(currentScreen - 1);
                background.DOFade(0.2784314f, 0);
                break;
        }
    }

    public void OnNextButtonClicked()
    {
        StartCoroutine(NextScreen());
    }

    public void OnCloseTutorialButtonClicked()
    {
        FinishTutorial();
    }
}
