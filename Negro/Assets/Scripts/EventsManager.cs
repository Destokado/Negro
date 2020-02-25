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

    public Event GetEventFor(Statistics currentGameStatistics, GameStateManager gameState)
    {
        RandomPro rnd = new RandomPro();
        Event[] RandomSortedEvents = events.ToArray().OrderBy(x => rnd.Next()).ToArray();

        int maxProb = events.Where(gameState.AreGameStatesCorrectFor).Sum(ev => ev.GetProbability(currentGameStatistics));
        int tryProb = rnd.GetRandomInt(0, maxProb);
        int currTryEvCount = 0;

        foreach (Event ev in RandomSortedEvents)
        {
            if (gameState.AreGameStatesCorrectFor(ev))
            {
                currTryEvCount += ev.GetProbability(currentGameStatistics);
                if (currTryEvCount >= tryProb)
                    return ev;
            }
        }

        

        Debug.LogWarning("Event not found. Probably because the game state didn't fit any of the events.\nThe probability was " + tryProb + " out of " + maxProb + ".  Count ended at " + currTryEvCount + ". Was it correct? " + (currTryEvCount >= tryProb));
        return null;
    }
}
