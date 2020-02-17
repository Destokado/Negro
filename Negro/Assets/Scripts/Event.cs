using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public readonly int id;
    public readonly string text;
    public readonly string location;
    public readonly HashSet<GameState> requirements;
    public readonly Stats statsProbabilityIncrement;
    public readonly List<Action> actions;

    public Event(int id, string text, string location, HashSet<GameState> requirements, Stats statsProbabilityIncrement, List<Action> actions)
    {
        this.id = id;
        this.text = text;
        this.location = location;
        this.requirements = requirements;
        this.statsProbabilityIncrement = statsProbabilityIncrement;
        this.actions = actions;
    }

    public override string ToString()
    {
        return "Event '" + id + "': " + text + "\n" + location + ", " + requirements;
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