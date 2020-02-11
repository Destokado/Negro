using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventsManager
{
    private HashSet<Event> events;
    private GameStateManager gameStateManager;

    public EventsManager(HashSet<Event> events)
    {
        this.events = events;
        gameStateManager = new GameStateManager();
    }

    public Event GetEvent()
    {
        RandomPro rnd = new RandomPro();
        return events.ToArray()[rnd.GetRandomInt(events.Count)];
    }
}
