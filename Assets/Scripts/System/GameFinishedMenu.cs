using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameFinishedMenu : MonoBehaviour
{
    private CanvasGroup gameOverPanel;

    [SerializeField] private Button backToMenuButton;
    [SerializeField] private float panelFadeTime = 3f;
    [SerializeField] private float buttonFadeTime = 0.5f;
    [SerializeField] private float buttonAppearTime = 2f;
    [SerializeField] private float buttonPunchDuration = 0.5f;
    [SerializeField] private float buttonPunchInterval = 2f;
    [SerializeField] private int buttonVibrato = 5;
    [SerializeField] private float buttonElasticity = 0f;
    [SerializeField] private float buttonScaleMultiplier = 1f;

    private void Start()
    {
        gameOverPanel = GetComponent<CanvasGroup>();
        gameOverPanel.alpha = 0f;
        gameOverPanel.interactable = false;
        gameOverPanel.blocksRaycasts = false;
        backToMenuButton.GetComponent<Image>().DOFade(0, 0);
        backToMenuButton.GetComponentInChildren<Text>().DOFade(0, 0);
        backToMenuButton.interactable = false;
    }

    public void GameFinished()
    {
        FindObjectOfType<BattleManager>().StopBattles();
        SaveSystem.ResetSaveData();
        StartCoroutine(GameFinishedCoroutine());
    }

    private IEnumerator GameFinishedCoroutine()
    {
        gameOverPanel.DOFade(1, panelFadeTime);
        gameOverPanel.interactable = true;
        gameOverPanel.blocksRaycasts = true;
        TimerPanel.SetPause(true);

        yield return new WaitForSeconds(buttonAppearTime);

        yield return BackToMenuButtonAnimation();
    }

    private IEnumerator BackToMenuButtonAnimation()
    {
        backToMenuButton.GetComponent<Image>().DOFade(1, buttonFadeTime);
        backToMenuButton.GetComponentInChildren<Text>().DOFade(1, buttonFadeTime);
        backToMenuButton.interactable = true;

        RectTransform transform = backToMenuButton.GetComponent<RectTransform>();

        while (true)
        {
            transform.DOPunchScale(transform.localScale * buttonScaleMultiplier, buttonPunchDuration, buttonVibrato, buttonElasticity);
            yield return new WaitForSeconds(buttonPunchInterval);
        }
    }

    public void BackToMenu()
    {
        StopCoroutine(BackToMenuButtonAnimation());
        TimerPanel.UnsubscribeDelegates();
        TimerPanel.SetPause(false);
        SceneManager.LoadScene(0);
    }
}
