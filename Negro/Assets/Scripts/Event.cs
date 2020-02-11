using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private int id;
    private string text;
    private string location;
    private List<string> requirements;
    private Stats statsProbabilityIncrement;
    private List<Action> actions = new List<Action>();

    public List<Action> Actions => actions;

    public Event(int id, string text, string location, List<string> requirements, Stats statsProbabilityIncrement, List<Action> actions)
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

        return id == eventToCompareWith.id;
    }

    protected bool Equals(Event other)
    {
        return id == other.id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
