using UnityEngine;
using DG.Tweening;

public abstract class AMainPanel: MonoBehaviour{
    private const float FADE_DURATION = 0.5f;

    public void HidePanel(float duration = FADE_DURATION)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.DOFade(0, duration);
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    public void ShowPanel(float duration = FADE_DURATION)
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.DOFade(1, duration);
        group.interactable = true;
        group.blocksRaycasts = true;
        PrepareContent();
    }
    abstract public void PrepareContent();
}
