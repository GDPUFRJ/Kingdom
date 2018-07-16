using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelSwitcher : MonoBehaviour {

    private const float ACTIVE_ANGLE = 35;
    private const float DURATION = 0.2f;

    private RectTransform rectTransform;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void ActiveButton()
    {

        rectTransform.DORotate(new Vector3(ACTIVE_ANGLE, 0, 0),DURATION);
    }
    public void DisableButton()
    {
        rectTransform.DORotate(new Vector3(0, 0, 0), DURATION);
    }

}
