using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeStates
{
    pause,
    unpause
}

public class EventManager : MonoBehaviour
{
    //singleton inplementation
    public static EventManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<TimeStates> OnTimeChange;
    public void TimeChange(TimeStates timeStates)
    {
        OnTimeChange?.Invoke(timeStates);
    }

    public event Action OnWaveEvent;
    public void WaveEvent()
    {
        OnWaveEvent?.Invoke();
    }

    public event Action OnEventStart;
    public void EventStart()
    {
        GameManager.instance.eventPanel.SetActive(true);
        Debug.Log(OnEventStart == null);
        OnEventStart?.Invoke();
    }
}
