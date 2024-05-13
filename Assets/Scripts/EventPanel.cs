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
    public float engineerBuffToTowers = 0.8f;
    public float engineerCircuitsToTake = 0.3f;

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
                break;
            case Events.EngineerEvent:
                int circuitsToTake = (int)Mathf.Round(GameManager.instance.GetCurrentCircuits() * engineerCircuitsToTake);
                GameManager.instance.ChangeCircuits(-circuitsToTake);
                Debug.Log(circuitsToTake);
                EventManager.instance.AcceptEngineerEvent(engineerBuffToTowers);
                break;
            case Events.StrikeEvent:
                break;
            case Events.SurvivorsEvent:
                break;
            case Events.TerroristEvent:
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
                break;
            case Events.EngineerEvent:
                //EventManager.instance.DeclineEngineerEvent();
                break;
            case Events.StrikeEvent:
                break;
            case Events.SurvivorsEvent:
                break;
            case Events.TerroristEvent:
                break;
        }
        EventManager.instance.TimeChange(TimeStates.unpause);
        this.gameObject.SetActive(false);
    }
}
