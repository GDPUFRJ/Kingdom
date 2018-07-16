using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour{
    private const int DEFAULT_PANEL = 1;

    private int currentPanel;

    [SerializeField] private List<AMainPanel> panels;
    [SerializeField] private List<PanelSwitcher> buttons;

    public void Start()
    {
        HideAllPanels();
        currentPanel = DEFAULT_PANEL;
        panels[currentPanel].ShowPanel(0);
        buttons[currentPanel].ActiveButton();
    }

    public void ChangePanel(int newPanel)
    {
        if(newPanel == currentPanel)
        {
            newPanel = DEFAULT_PANEL;
        }
        panels[currentPanel].HidePanel();
        buttons[currentPanel].DisableButton();
        panels[newPanel].ShowPanel();
        buttons[newPanel].ActiveButton();

        currentPanel = newPanel;
        panels[currentPanel].PrepareContent();
    }

    private void HideAllPanels()
    {
        foreach (AMainPanel panel in panels)
        {
            panel.HidePanel(0);
        }
    }
}
