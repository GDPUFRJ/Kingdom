using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanel : MonoBehaviour {
    [Header("Information")]
    [SerializeField]
    private float time;
    [SerializeField]
    private float dayLength;
    [SerializeField]
    private int currentDay;

    private bool paused;

    [Header("UI Elements")]
    [SerializeField]
    private Image passedTime;

    public delegate void DayEnd();
    public static event DayEnd OnDayEnd;



    public float GetTime()
    {
        return time;
    }
    public float GetDayLenght()
    {
        return dayLength;
    }

    private void Start()
    {
        StartCoroutine(DayRoutine());
    }
    private IEnumerator DayRoutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (!paused)
            {
                time += Time.deltaTime;
                UpdateUI();
                if(time >= dayLength)
                {
                    RiseNewDay();
                    time = 0;
                }
            }
        }
    }
    private void UpdateUI()
    {
        passedTime.fillAmount = time / dayLength;
    }
    public void RiseNewDay()
    {
        currentDay++;
        print("DAWN OF THE "+(currentDay+1)+"th DAY");
        if(OnDayEnd != null)
            OnDayEnd();
    }

    public void SetPause(bool isPaused)
    {
        paused = isPaused;
    }
}
