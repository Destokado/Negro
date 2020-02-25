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
        string id = eventInTextFormat[0];
        string text = eventInTextFormat[1];
        string location = eventInTextFormat[2];
        HashSet<GameState> requirements = GameStateFactory.BuildGameStates(eventInTextFormat[3]);
        Statistics statisticsProbabilityIncrement = new Statistics(ConvertToInt(eventInTextFormat[4], 50), ConvertToInt(eventInTextFormat[5], 50), ConvertToInt(eventInTextFormat[6], 50));
        
        List<Action> actions = new List<Action>(); 
        for (int a = 0; a < 4; a++)
        {
            string actionText = eventInTextFormat[7 + a * 5];
            HashSet<GameState> actionConsequences = GameStateFactory.BuildGameStates(eventInTextFormat[8 + a * 5]);
            Statistics actionStatisticsModifications = new Statistics(ConvertToInt(eventInTextFormat[9 + a * 5]),
                ConvertToInt(eventInTextFormat[10 + a * 5]), ConvertToInt(eventInTextFormat[11 + a * 5]));
            actions.Add(new Action(actionText, actionConsequences, actionStatisticsModifications));
        }
        Event er =  new Event(id, text, location, requirements, statisticsProbabilityIncrement, actions);
        return er;
    }

    public static int ConvertToInt(string value, int defaultValue = 0)
    {
        int retValue = defaultValue;
        int.TryParse(value, out retValue);
        return retValue;
    }
    
}