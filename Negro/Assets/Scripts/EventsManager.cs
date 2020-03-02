using System;
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

    public void CheckEvents()
    {
        List<string> errorReport = new List<string>();
        List<string> warningReport = new List<string>();
        
        foreach (Event ev in events)
        {
            foreach (GameState requirement in ev.requirements.gameStates)
            {
                if (!AnyActionHasAsConsequence(requirement))
                    errorReport.Add("The event '" + ev.id + "' has '" + requirement.name + "' as requirement, but no action modifies it as consequence.");
            }

            if (ev.validActions <= 0)
                errorReport.Add("The event '" + ev.id + "' does not have any action attached.");

            if (string.Compare(ev.art, "Z-NONE", StringComparison.InvariantCultureIgnoreCase) == 0 || ev.art.IsNullEmptyOrWhiteSpace())
                warningReport.Add("The event '" + ev.id + "' does not have any art assigned.");
            else if (Resources.Load<Sprite>(ev.art) == null)
                errorReport.Add("The art '"+ ev.art + "' in the event '" + ev.id + "' was not found.");
        }

        foreach (string rep in errorReport)
            Debug.LogError(rep);
        
        foreach (string rep in warningReport)
            Debug.LogWarning(rep);
    }

    private bool AnyActionHasAsConsequence(GameState requirement)
    {
        foreach (Event ev in events)
            foreach (Action action in ev.actions)
                foreach (GameState consequence in action.consequences.gameStates)
                    if (consequence.Equals(requirement))
                        return true;

        return false;
    }

    public void CheckActions()
    {
        List<string> errorReport = new List<string>();
        List<string> warningReport = new List<string>();
        
        foreach (Event ev in events)
            foreach (Action action in ev.actions)
                foreach (GameState consequence in action.consequences.gameStates)
                    if (consequence.type == GameState.Type.ForceEvent)
                    {
                        if (!ExistEventWithId(consequence.name))
                            errorReport.Add("The consequence '" + consequence +  "' in the action '" + action.text + "' in the event '" + ev.id + "' is trying to force a non-existing event.");
                    }
                    else
                    {
                        if (!ExistsEventWithRequirementOf(consequence))
                            warningReport.Add("The consequence '" + consequence +  "' in the action '" + action.text + "' in the event '" + ev.id + "' is never used in any event as requirement.");
                    }

        foreach (string rep in errorReport)
            Debug.LogError(rep);
        
        foreach (string rep in warningReport)
            Debug.LogWarning(rep);
    }
    
    private bool ExistEventWithId(string id)
    {
        foreach (Event ev in events)
            if (string.Compare(ev.id.ToUpper(), id, StringComparison.InvariantCultureIgnoreCase) == 0)
                return true;

        return false;
    }
    
    private bool ExistsEventWithRequirementOf(GameState consequence)
    {
        foreach (Event ev in events)
            foreach (GameState requirement in ev.requirements.gameStates)
                if (requirement.Equals(consequence))
                    if ((consequence.type != GameState.Type.ForceEvent && requirement.type != GameState.Type.ForceEvent) || (consequence.type == GameState.Type.ForceEvent && requirement.type == GameState.Type.ForceEvent))
                        return true;
            

        return false;
    }
}
