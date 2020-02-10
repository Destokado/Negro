using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventFactory
{
    public static HashSet<Event> BuildEvents(string csvFilename)
    {
        return new HashSet<Event>(BuildEvents(CsvReader.ReadAsList(csvFilename)));
    }
    
    public static HashSet<Event> BuildEvents(List<string[]> eventsInTextFormat)
    {
        HashSet<Event> events = new HashSet<Event>();

        for (int e = 1; e < eventsInTextFormat.Count; e++)
            events.Add(BuildEvent(eventsInTextFormat[e]));

        return events;
    }
    
    public static Event BuildEvent(string[] eventInTextFormat)
    {
        int id = Convert.ToInt32(eventInTextFormat[0]);
        string text = eventInTextFormat[1];
        string location = eventInTextFormat[2];
        List<string> requirements = new List<string>(); //TODO: Future addition: record the requirements of the event
        Stats statsProbabilityIncrement = new Stats(Convert.ToInt32(eventInTextFormat[4]), Convert.ToInt32(eventInTextFormat[5]), Convert.ToInt32(eventInTextFormat[6]), Convert.ToInt32(eventInTextFormat[7]));
        
        List<Action> actions = new List<Action>(); 
        for (int a = 0; a < 4; a++)
        {
            string actionText = eventInTextFormat[8 + a * 6];
            List<string> actionConsequences = new List<string>(); //TODO: Future addition: record the consequences of this action
            Stats actionStatsModifications = new Stats(Convert.ToInt32(eventInTextFormat[10 + a * 6]),
                Convert.ToInt32(eventInTextFormat[11 + a * 6]), Convert.ToInt32(eventInTextFormat[12 + a * 6]),
                Convert.ToInt32(eventInTextFormat[13 + a * 6]));
            actions.Add(new Action(actionText, actionConsequences, actionStatsModifications));
        }
        
        return new Event(id, text, location, requirements, statsProbabilityIncrement, actions);
    }
}
