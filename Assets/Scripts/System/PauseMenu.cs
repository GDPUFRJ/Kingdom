using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeTime = 0.5f;

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
    }

    public void Resume()
    {
        canvasGroup.DOFade(0, fadeTime);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        TimerPanel.SetPause(false);
    }

    public void SaveGame()
    {
        GameManager.Instance.SaveGame();
    }

    public void ReturnToMainMenu()
    {
        TimerPanel.SetPause(false);
        TimerPanel.UnsubscribeDelegates();
        SceneManager.LoadScene(0);
    }
}
