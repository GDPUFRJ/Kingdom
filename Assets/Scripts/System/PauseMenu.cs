﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private Image pauseButton;
    [SerializeField] private Sprite pausedImage;
    [SerializeField] private Sprite unpausedImage;
    [SerializeField] private CanvasGroup optionsMenu;
    [SerializeField] private GameObject[] buttons;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Pause()
    {
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        TimerPanel.SetPause(true);
        pauseButton.sprite = pausedImage;
    }

    public void Resume()
    {
        canvasGroup.DOFade(0, fadeTime);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if (!SoldierPanel.isEditButtonsEnable) TimerPanel.SetPause(false);
        pauseButton.sprite = unpausedImage;
    }

    public void SaveGame()
    {
        GameManager.Instance.SaveGame();
    }

    public void ReturnToMainMenu()
    {
        TimerPanel.SetPause(false);
        TimerPanel.UnsubscribeDelegates();
        FMODPlayer.StopAllSounds();
        SceneManager.LoadScene(0);
    }

    public void OpenOptions()
    {
        foreach (var button in buttons) { button.SetActive(false); }
        optionsMenu.alpha = 1f;
        optionsMenu.interactable = true;
        optionsMenu.blocksRaycasts = true;
        optionsMenu.GetComponent<Animator>().Play("MenuOpções_OpenScroll");
    }

    public void CloseOptions()
    {
        optionsMenu.GetComponent<Animator>().Play("MenuOpções_CloseScroll");
    }

    public void GoBackToPauseMenuFromOptions()
    {
        foreach (var button in buttons) { button.SetActive(true); }
        optionsMenu.alpha = 0f;
        optionsMenu.interactable = false;
        optionsMenu.blocksRaycasts = false;
    }
}
