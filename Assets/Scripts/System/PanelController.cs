using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour{
    [SerializeField]
    private List<AMainPanel> panels;
    [SerializeField]
    private int currentPanel;
    [SerializeField]
    private Text currentPanelName;

    public void Start()
    {
        foreach(AMainPanel panel in panels)
        {
            panel.HidePanel(0);
        }
        panels[currentPanel].ShowPanel(0);
    }
    public void ChangePanel(int direction)
    {
        int newPanel = currentPanel + direction;
        newPanel = Mathf.Clamp(newPanel, 0, panels.Count-1);

        panels[currentPanel].HidePanel();
        panels[newPanel].ShowPanel();

        currentPanel = newPanel;
        ChangeName(panels[currentPanel].panelName);
    }
    private void ChangeName(string name)
    {
        currentPanelName.DOText(name, 0.5f);
    }
}
