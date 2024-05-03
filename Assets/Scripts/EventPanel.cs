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

    private void Awake()
    {
        EventManager.instance.OnEventStart += StartEvent;
    }

    private void StartEvent()
    {
        Debug.Log("Empezó el evento");
        int rnd = Random.Range(0, events.Count);
        currentEvent = events[rnd];
        //events.RemoveAt(rnd);

        title.text = currentEvent.eventName;
        description.text = currentEvent.description;
        acceptButtonText.text = currentEvent.option1Text;
        declineButtonText.text = currentEvent.option2Text;
    }

    public void Accept()
    {

    }

    public void Decline()
    {

    }
}
