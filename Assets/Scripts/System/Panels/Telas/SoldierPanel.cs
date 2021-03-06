﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SoldierPanel : Section
{
    public GameObject CanvasBattle;
    [SerializeField] private CanvasGroup editButtonsGroup;
    [SerializeField] private Image toggle;
    [Space(5)]
    [SerializeField] private float editButtonsFadeTime = 0.3f;
    [SerializeField] private Color editEnabledColor = Color.green;
    [SerializeField] private Color editDisabledColor = Color.red;

    public static bool isEditButtonsEnable = false;

    private new void Start()
    {
        InitializeEditToggle();
        CanvasBattle.SetActive(false);
    }
    public override void PrepareContent()
    {
        CanvasBattle.SetActive(true);
    }
    public override void UnprepareContent()
    {
        CanvasBattle.SetActive(false);
    }
    private void InitializeEditToggle()
    {
        isEditButtonsEnable = false;
        toggle.color = isEditButtonsEnable ? editEnabledColor : editDisabledColor;
        editButtonsGroup.interactable = isEditButtonsEnable;
        editButtonsGroup.blocksRaycasts = isEditButtonsEnable;
        editButtonsGroup.DOFade(0, 0);
    }
    public void ToggleEditButtons()
    {
        isEditButtonsEnable = !isEditButtonsEnable;
        if (isEditButtonsEnable) FMODPlayer.Instance.Play("enter battle mode");
        TimerPanel.SetPause(isEditButtonsEnable);
        toggle.color = isEditButtonsEnable ? editEnabledColor : editDisabledColor;
        float target = isEditButtonsEnable ? 1 : 0;
        editButtonsGroup.interactable = isEditButtonsEnable;
        editButtonsGroup.blocksRaycasts = isEditButtonsEnable;
        editButtonsGroup.DOFade(target, editButtonsFadeTime);

        if (!isEditButtonsEnable && SendTroopsBox.Instance.IsActivated)
            SendTroopsBox.Instance.CancelOperation();
    }
}