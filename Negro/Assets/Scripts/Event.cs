using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event
{
    public readonly string id;
    public readonly string text;
    public readonly string art;
    public readonly GameStateManager requirements;
    public readonly Statistics statisticsProbabilityIncrement;
    public readonly List<Action> actions;
    public int validActions {  get { return actions.Count(action => action.IsValid()); } }

    public Event(string id, string text, string art, HashSet<GameState> requirements, Statistics statisticsProbabilityIncrement, List<Action> actions)
    {
        this.id = id;
        this.text = text;
        this.art = art;
        this.requirements = new GameStateManager(requirements);
        requirements.Remove(null);
        this.statisticsProbabilityIncrement = statisticsProbabilityIncrement;
        this.actions = actions;
    }

    public override string ToString()
    {
        return "ID: '" + id + "'. Text: '" + text + "'\n Art: '" + art + "'. Requirements: " + requirements.ToString() + "\nActions: " + actions[0].ToString() + ", " + actions[1].ToString() + ", " + actions[2].ToString() + ", " + actions[3].ToString();
    }

    public override bool Equals(object obj)
    {
        Event eventToCompareWith;

        try
        {
            eventToCompareWith = (Event) obj;
        }
        catch (Exception)
        {
            return false;
        }

        return eventToCompareWith != null && id == eventToCompareWith.id;
    }

    protected bool Equals(Event other)
    {
        return id == other.id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public Action GetFirstValidAction()
    {
        return actions.FirstOrDefault(action => action.IsValid());
    }

    public int GetProbability(Statistics currentStatistics)
    {
        int probability = 100;

        probability -= Mathf.Abs(statisticsProbabilityIncrement.health.value - currentStatistics.health.value)/3;
        probability -= Mathf.Abs(statisticsProbabilityIncrement.sanity.value - currentStatistics.sanity.value)/3;
        probability -= Mathf.Abs(statisticsProbabilityIncrement.socialStatus.value - currentStatistics.socialStatus.value)/3;
        
        //Debug.Log("Probability of event '" + id + "' is " + probability );
        
        return probability;
    }
}