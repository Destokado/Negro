using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager
{
    private HashSet<Event> events;

    public EventsManager(HashSet<Event> events)
    {
        this.events = events;
    }
    
}
