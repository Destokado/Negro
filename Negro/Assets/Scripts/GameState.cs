using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public readonly bool exists;
    private readonly string name;

    public GameState(string name, bool exists = true)
    {
        this.name = name;
        this.exists = exists;
    }

    public override bool Equals(object obj)
    {
        GameState gsToCompare;

        try
        {
            gsToCompare = (GameState) obj;
        }
        catch (Exception)
        {
            return false;
        }
        
        return gsToCompare != null && string.Compare(name, gsToCompare.name, StringComparison.InvariantCultureIgnoreCase) == 0;
    }

    protected bool Equals(GameState other)
    {
        return name == other.name;
    }

    public override int GetHashCode()
    {
        return (name != null ? name.GetHashCode() : 0);
    }
}
