using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class AMainPanel: MonoBehaviour{
    [SerializeField]
    private bool comeFromRight;
    [SerializeField]
    private bool changePosition;
    [SerializeField]
    private float movingVelocity;
    [SerializeField]
    public string panelName;

    private const int CANVAS_WIDTH = 720;

    public void HidePanel(float velocity = -1)
    {
        if (!changePosition) return;
        if (velocity == -1) velocity = movingVelocity;
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOAnchorPosX(720 * GetBias(), velocity);
    }
    public void ShowPanel(float velocity = -1)
    {
        if (!changePosition) return;
        if (velocity == -1) velocity = movingVelocity;
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOAnchorPosX(0, velocity);
    }
    private int GetBias()
    {
        if (comeFromRight) return -1;
        else return 1;
    }
}
