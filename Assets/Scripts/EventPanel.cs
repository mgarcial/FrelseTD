using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private List<EventSO> events;
    private EventSO currentEvent;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text acceptButtonText;
    [SerializeField] private TMP_Text declineButtonText;

    //Engineer event parameters in percentages
    [SerializeField] float engineerBuffToTowers = 1.2f;
    [SerializeField] float engineerCircuitsToTake = 0.3f;

    [SerializeField] int terroristTowersToDestroy = 2;
    [SerializeField] int terroristHealthToTake = 3;

    [SerializeField] float climateDebuffToTowers = 0.7f;
    [SerializeField] int climateCircuitsToTake = 250;

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
                EventManager.instance.ClimateEvent(EventChoices.accept, climateCircuitsToTake);
                break;
            case Events.EngineerEvent:
                int circuitsToTake = (int)Mathf.Round(GameManager.instance.GetCurrentCircuits() * engineerCircuitsToTake);
                GameManager.instance.ChangeCircuits(-circuitsToTake);
                EventManager.instance.EngineerEvent(EventChoices.accept, engineerBuffToTowers);
                break;
            case Events.StrikeEvent:
                EventManager.instance.StrikeEvent(EventChoices.accept);
                break;
            case Events.SurvivorsEvent:
                EventManager.instance.SurvivorsEvent(EventChoices.accept);
                break;
            case Events.TerroristEvent:
                EventManager.instance.TerroristEvent(EventChoices.accept, terroristTowersToDestroy);
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
                EventManager.instance.ClimateEvent(EventChoices.decline, climateDebuffToTowers);
                break;
            case Events.EngineerEvent:
                EventManager.instance.EngineerEvent(EventChoices.decline, 0);
                break;
            case Events.StrikeEvent:
                EventManager.instance.StrikeEvent(EventChoices.decline);
                break;
            case Events.SurvivorsEvent:
                EventManager.instance.SurvivorsEvent(EventChoices.decline);
                break;
            case Events.TerroristEvent:
                EventManager.instance.TerroristEvent(EventChoices.decline, terroristHealthToTake);
                break;
        }
        EventManager.instance.TimeChange(TimeStates.unpause);
        this.gameObject.SetActive(false);
    }
}
