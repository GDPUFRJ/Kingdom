using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelSwitcher : MonoBehaviour {

    private const float ACTIVE_ANGLE = 35;
    private const float DURATION = 0.2f;

    private RectTransform rectTransform;

    public void ActiveButton()
    {
        if(rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        rectTransform.DORotate(new Vector3(ACTIVE_ANGLE, 0, 0),DURATION);
    }
    public void DisableButton()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        rectTransform.DORotate(new Vector3(0, 0, 0), DURATION);
    }

}
