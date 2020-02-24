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

    
    
    
    public bool AreGameStatesCorrectFor(Event @event)
    {
        if (@event.requirements.ExistsForcedGameStateFor(@event.id.ToString()))
        {
            if (!gameStates.Contains(new GameState(@event.id.ToString(), GameState.Type.ForceEvent)))
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
                    if (String.Compare(@event.id.ToString(), gs.name, StringComparison.OrdinalIgnoreCase) == 0)
                        return true;
                    else
                        return false;
                }
            }
        
            foreach (GameState requirement in @event.requirements.gameStates)
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

    private bool ExistsForcedGameStateFor(string gameStateName)
    {
        GameState searchingGs = new GameState(gameStateName, GameState.Type.ForceEvent);
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
                forcedEvents += gs.ToString() + ", ";
            else
                currentGameState += gs.ToString() + ", ";


        string finalReport = "";
        if (!string.IsNullOrEmpty(forcedEvents))
            finalReport += "Forced events: " + forcedEvents + ". ";
        if (!string.IsNullOrEmpty(currentGameState))
            finalReport += "Game states: " + currentGameState;

        return finalReport;
    }

    public void RemoveEventFromListOfForcedEvents(Event @event)
    {
        gameStates.Remove(new GameState(@event.id.ToString(), GameState.Type.ForceEvent));
    }
}
