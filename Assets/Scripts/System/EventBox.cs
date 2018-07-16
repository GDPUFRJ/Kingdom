using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EventBox : MonoBehaviour {
    [Header("Event UI Elements")]
    [SerializeField] private Text title;
    [SerializeField] private Text remainingDays;
    [SerializeField] private Text description;

    private KEvent _event;
    private EventPanel panel;

	public void SetInformation(KEvent _event, EventPanel _panel)
    {
        EventInfo e = _event.GetInfo();
        this._event = _event;
        this.panel = _panel;
        this.title.text = e.name;
        this.description.text = e.description;
        this.remainingDays.text = e.remainingDays.ToString();
    }
    public KEvent GetEvent()
    {
        return _event;
    }
}
