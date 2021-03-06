﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPanel : Section
{
    [SerializeField] private GameObject eventBoxPrefab;
    [SerializeField] private Transform root;

    protected override void Start()
    {
        base.Start();
        TimerPanel.OnAfterDayEnd += OnAfterDayEnd;
    }

    private void OnAfterDayEnd()
    {
        PrepareContent();
    }

    public override void PrepareContent()
    {
        DeleteAllChilds();
        List<KEvent> eventList = FindObjectOfType<KEventManager>().GetAllActiveEvents();
        foreach (KEvent e in eventList)
        {
            var obj = Instantiate(eventBoxPrefab, root).GetComponent<EventBox>();
            obj.SetInformation(e, this);
        }
    }

    private void DeleteAllChilds()
    {
        for (int i = root.childCount - 1; i >= 0; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }
}
