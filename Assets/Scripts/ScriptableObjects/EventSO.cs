using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class EventSO : ScriptableObject
{
    public Events eventType;
    public string eventName;
    public string description;
    public string option1Text;
    public string option2Text;
    public Sprite eventImage;
}
