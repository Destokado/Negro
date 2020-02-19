using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventsManager
{
    private HashSet<Event> events;

    public EventsManager(HashSet<Event> events)
    {
        this.events = events;
    }

    public Event GetEventFor(GameStateManager gameState)
    {
        RandomPro rnd = new RandomPro();
        Event[] RandomSortedEvents = events.ToArray().OrderBy(x => rnd.Next()).ToArray();

        foreach (Event ev in RandomSortedEvents)
            if (gameState.AreGameStatesCorrectFor(ev))
                return ev;

        return null;
    }
}
