using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private List<EventSO> events;
    private EventSO currentEvent;

    [Header("Showing Text")]
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Text acceptButtonText;
    [SerializeField] private Text declineButtonText;
    [SerializeField] private Image eventImage;

    [Header("Engineer Event Stats")]
    [SerializeField] float BuffToTowers = 1.2f;
    [SerializeField] int CircuitsToTake;
    [SerializeField] float CircuitsToReimburse = 0.5f;

    [Header("Terrorist Event Stats")]
    [SerializeField] int TowersToDestroy = 2;
    [SerializeField] int HealthToTake = 3;

    [Header("Climate Event Stats")]
    [SerializeField] float DebuffToTowers = 0.7f;
    [SerializeField] float CircuitsToWaste;

    [Header("Strike Event Stats")]
    [SerializeField] int AcceptCounter;
    [SerializeField] int DeclineCounter;
    [SerializeField] int BaseHealth;
    [SerializeField] int RewardCircuits;
    [SerializeField] float ReducedMoneyRate;

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
        eventImage.sprite = currentEvent.eventImage;

        if(currentEvent.eventType == Events.EngineerEvent && GameManager.instance.GetCurrentCircuits() < CircuitsToTake)
        {
            acceptButtonText.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }

    public void Accept()
    {
        switch (currentEvent.eventType)
        {
            case Events.ClimateEvent:
                EventManager.instance.ClimateEvent(EventChoices.accept, CircuitsToWaste);
                break;
            case Events.EngineerEvent:
                if(GameManager.instance.GetCurrentCircuits() >= CircuitsToTake)
                {
                    GameManager.instance.ChangeCircuits(-CircuitsToTake);
                    EventManager.instance.EngineerEvent(EventChoices.accept, BuffToTowers);
                }
                break;
            case Events.StrikeEvent:
                EventManager.instance.StrikeEvent(EventChoices.accept, BaseHealth, AcceptCounter, RewardCircuits);
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
                EventManager.instance.StrikeEvent(EventChoices.decline, ReducedMoneyRate, DeclineCounter, 0);
                break;
            case Events.TerroristEvent:
                EventManager.instance.TerroristEvent(EventChoices.decline, HealthToTake);
                break;
        }

        EventManager.instance.TimeChange(TimeStates.unpause);
        acceptButtonText.gameObject.GetComponentInParent<Button>().interactable = true;
        this.gameObject.SetActive(false);
    }
}
