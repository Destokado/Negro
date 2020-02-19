using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public readonly int id;
    public readonly string text;
    public readonly string art;
    public readonly GameStateManager requirements;
    public readonly Stats statsProbabilityIncrement; // TODO: Implement when choosing events randomly
    public readonly List<Action> actions;

    public Event(int id, string text, string art, HashSet<GameState> requirements, Stats statsProbabilityIncrement, List<Action> actions)
    {
        this.id = id;
        this.text = text;
        this.art = art;
        this.requirements = new GameStateManager(requirements);
        requirements.Remove(null);
        this.statsProbabilityIncrement = statsProbabilityIncrement;
        this.actions = actions;
    }

    public override string ToString()
    {
        return "ID: '" + id + "'. Text: '" + text + "'\n Art: '" + art + "'. Requirements: " + requirements.ToString();
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
        return id;
    }
}