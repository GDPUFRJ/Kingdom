using UnityEngine;
using DG.Tweening;

public class Section : MonoBehaviour
{
    private const float FINAL_FADE = 0.3f;
    public float nativePos = 720;
    [SerializeField] private PanelSwitcher panelSwitcher;
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void show(int bias, float duration = 0.25f)
    {
        panelSwitcher.ActiveButton();
        Start();
        if (bias == 0) return;
        rect.DOAnchorPosX(nativePos + bias * 720, 0);
        rect.DOAnchorPosX(nativePos, duration);
        OnShow();
    }
    public void hide(int bias, float duration = 0.25f)
    {
        panelSwitcher.DisableButton();
        Start();
        if (bias == 0) return;
        bias = bias / Mathf.Abs(bias);
        rect.DOAnchorPosX(nativePos, 0);
        rect.DOAnchorPosX(nativePos + bias * 720, duration);
        OnHide();
    }
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
    public virtual void PrepareContent() { }
    public virtual void UnprepareContent() { }
}