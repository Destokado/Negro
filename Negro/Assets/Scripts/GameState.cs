using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum Type
    {
        Exists,
        NotExists,
        ForceEvent
    }
    
    public readonly Type type;
    public readonly string name;

    public GameState(string name, Type type)
    {
        this.name = name.ToUpper();
        this.type = type;
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
        return name.ToUpper() == other.name.ToUpper();
    }

    public override int GetHashCode()
    {
        return (name != null ? name.GetHashCode() : 0);
    }

    public override string ToString()
    {
        string type = "";
        switch (this.type)
        {
            case Type.Exists:
                type = "";
                break;
            case Type.NotExists:
                type = "!";
                break;
            case Type.ForceEvent:
                type = "_";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return type + this.name;
    }
}
