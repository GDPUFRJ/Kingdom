using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour {
    private const int DEFAULT_PANEL = 1;

    public int currentPanel;

    [SerializeField] private List<AMainPanel> panels;
    [SerializeField] private List<PanelSwitcher> buttons;

    public void Start()
    {
        HideAllPanels();
        currentPanel = DEFAULT_PANEL;
        TogglePanelAndButton(true, currentPanel);
    }

    public void ChangePanel(int newPanel)
    {
        if(newPanel == currentPanel)
        {
            newPanel = DEFAULT_PANEL;
        }
        TogglePanelAndButton(false, currentPanel);
        TogglePanelAndButton(true, newPanel);

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

    private void TogglePanelAndButton(bool enable, int id, float duration = -1)
    {
        if (enable)
        {
            buttons[id].ActiveButton();
            if(duration != -1)
                panels[id].ShowPanel(duration);
            else
                panels[id].ShowPanel();
        }
        else
        {
            buttons[id].DisableButton();
            if (duration != -1)
                panels[id].HidePanel(duration);
            else
                panels[id].HidePanel();
        }
    }
}
