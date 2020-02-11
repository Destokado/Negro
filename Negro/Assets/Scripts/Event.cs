using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private readonly int id;
    private readonly string text;
    private readonly string location;
    private readonly HashSet<GameState> requirements;
    private readonly Stats statsProbabilityIncrement;
    private readonly List<Action> actions;

    public List<Action> Actions => actions;

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