using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{

    public HashSet<GameState> gameStates { get; private set; }

    public GameStateManager(HashSet<GameState> gameStates)
    {
        this.gameStates = gameStates;
    }

    
    
    public void Compute(GameStateManager newConsequences)
    {
        List<GameState> gsToAdd = new List<GameState>();
        List<GameState> gsToRemove = new List<GameState>();

        foreach (GameState newConsequence in newConsequences.gameStates)
            if (newConsequence.type == GameState.Type.NotExists)
            {
                if (gameStates.Contains(newConsequence))
                    gsToRemove.Add(newConsequence);
            }
            else if (newConsequence.type == GameState.Type.Exists || newConsequence.type == GameState.Type.ForceEvent)
            {
                if (!gameStates.Contains(newConsequence))
                    gsToAdd.Add(newConsequence);
            }

        foreach (GameState gs in gsToAdd)
            gameStates.Add(gs);
        
        foreach (GameState gs in gsToRemove)
            gameStates.Remove(gs);

        gameStates.Remove(null);
    }

    
    
    
    public bool AreGameStatesCorrectFor(Event ev)
    {
        if (ev.requirements.ExistsForcedGameState(ev.id))
        {
            if (!gameStates.Contains(new GameState(ev.id.ToUpper(), GameState.Type.ForceEvent)))
                return false;
            else
                return true;
        }
        else
        {
            foreach (GameState gs in gameStates)
            {
                if (gs.type == GameState.Type.ForceEvent)
                {
                    if (String.Compare(ev.id, gs.name, StringComparison.OrdinalIgnoreCase) == 0)
                        return true;
                    else
                        return false;
                }
            }
        
            foreach (GameState requirement in ev.requirements.gameStates)
            {
                if (requirement.type == GameState.Type.Exists)
                {
                    if (!gameStates.Contains(requirement))
                        return false;
                }
                else if (requirement.type == GameState.Type.NotExists)
                {
                    if (gameStates.Contains(requirement))
                        return false;
                }
            }
        }
        
        return true;
    }

    private bool ExistsForcedGameState(string gameStateName)
    {
        GameState searchingGs = new GameState(gameStateName.ToUpper(), GameState.Type.ForceEvent);
        foreach (GameState gs in gameStates)
        {
            if (gs.Equals(searchingGs))
                if (gs.type == GameState.Type.ForceEvent)
                    return true;
        }

        return false;
    }

    public override string ToString()
    {
        string forcedEvents = "";
        string currentGameState = "";
        
        foreach (GameState gs in gameStates)
            if (gs.type == GameState.Type.ForceEvent)
                forcedEvents += gs + ", ";
            else
                currentGameState += gs + ", ";
        
        string finalReport = "";
        if (!string.IsNullOrEmpty(forcedEvents))
            finalReport += "Forced events: " + forcedEvents.Substring(0, forcedEvents.Length-2) + ".";
        if (!string.IsNullOrEmpty(currentGameState))
        {
            if (!string.IsNullOrEmpty(finalReport)) 
                finalReport += " | ";
            finalReport += "Game states: " + currentGameState.Substring(0, currentGameState.Length-2) + ".";
        }
            

        if (string.IsNullOrEmpty(finalReport))
            return "None.";
        else
            return finalReport;
    }

    public void RemoveEventFromListOfForcedEvents(Event @event)
    {
        gameStates.Remove(new GameState(@event.id.ToUpper(), GameState.Type.ForceEvent));
    }
}
