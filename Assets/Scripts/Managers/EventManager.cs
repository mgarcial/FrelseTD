using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeStates
{
    pause,
    unpause
}

public enum EventChoices
{
    accept,
    decline
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
        OnEventStart?.Invoke();
    }

    public event Action<EventChoices, float> OnEngineerEvent;
    public void EngineerEvent(EventChoices choice, float cuantity)
    {
        OnEngineerEvent?.Invoke(choice, cuantity);
    }

    public event Action<EventChoices> OnStrikeEvent;
    public void StrikeEvent(EventChoices choice)
    {
        OnStrikeEvent?.Invoke(choice);
    }

    public event Action<EventChoices, float> OnClimateEvent;
    public void ClimateEvent(EventChoices choice, float cuantity)
    {
        OnClimateEvent?.Invoke(choice, cuantity);
    }

    public event Action<EventChoices, int> OnTerroristEvent;
    public void TerroristEvent(EventChoices choice, int cuantity)
    {
        OnTerroristEvent?.Invoke(choice, cuantity);
    }
}
