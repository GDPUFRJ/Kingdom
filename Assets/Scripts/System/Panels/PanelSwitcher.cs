using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelSwitcher : MonoBehaviour {

    private RectTransform rectTransform;
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;

    public void ActiveButton()
    {
        GetComponent<Image>().sprite = enabledSprite;
    }
    public void DisableButton()
    {
        GetComponent<Image>().sprite = disabledSprite;
    }

}
