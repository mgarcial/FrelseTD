using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private List<EventSO> events;
    private EventSO currentEvent;

    [Header("Showing Text")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text acceptButtonText;
    [SerializeField] private TMP_Text declineButtonText;

    [Header("Engineer Event Stats")]
    [SerializeField] float BuffToTowers = 1.2f;
    [SerializeField] float CircuitsToTake = 0.3f;
    [SerializeField] float CircuitsToReimburse = 0.5f;

    [Header("Terrorist Event Stats")]
    [SerializeField] int TowersToDestroy = 2;
    [SerializeField] int HealthToTake = 3;

    [Header("Climate Event Stats")]
    [SerializeField] float DebuffToTowers = 0.7f;
    [SerializeField] int CircuitsToWaste = 250;

    private void Awake()
    {
        EventManager.instance.OnEventStart += StartEvent;
    }

    private void StartEvent()
    {
        int rnd = Random.Range(0, events.Count);
        currentEvent = events[rnd];
        events.RemoveAt(rnd);

        title.text = currentEvent.eventName;
        description.text = currentEvent.description;
        acceptButtonText.text = currentEvent.option1Text;
        declineButtonText.text = currentEvent.option2Text;
    }

    public void Accept()
    {
        switch (currentEvent.eventType)
        {
            case Events.ClimateEvent:
                EventManager.instance.ClimateEvent(EventChoices.accept, CircuitsToWaste);
                break;
            case Events.EngineerEvent:
                int circuitsToTake = (int)Mathf.Round(GameManager.instance.GetCurrentCircuits() * CircuitsToTake);
                GameManager.instance.ChangeCircuits(-circuitsToTake);
                EventManager.instance.EngineerEvent(EventChoices.accept, BuffToTowers);
                break;
            case Events.StrikeEvent:
                //EventManager.instance.StrikeEvent(EventChoices.accept);
                break;
            case Events.TerroristEvent:
                EventManager.instance.TerroristEvent(EventChoices.accept, TowersToDestroy);
                break;
        }

        EventManager.instance.TimeChange(TimeStates.unpause);
        this.gameObject.SetActive(false);
    }

    public void Decline()
    {
        switch (currentEvent.eventType)
        {
            case Events.ClimateEvent:
                EventManager.instance.ClimateEvent(EventChoices.decline, DebuffToTowers);
                break;
            case Events.EngineerEvent:
                EventManager.instance.EngineerEvent(EventChoices.decline, CircuitsToReimburse);
                break;
            case Events.StrikeEvent:
                //EventManager.instance.StrikeEvent(EventChoices.decline);
                break;
            case Events.TerroristEvent:
                EventManager.instance.TerroristEvent(EventChoices.decline, HealthToTake);
                break;
        }
        EventManager.instance.TimeChange(TimeStates.unpause);
        this.gameObject.SetActive(false);
    }
}
