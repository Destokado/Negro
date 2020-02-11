using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{

    private HashSet<GameState> currentGameStates;

    public void ProcessEvents(HashSet<GameState> gameStatesToProcess)
    {
        List<GameState> gsToAdd = new List<GameState>();
        List<GameState> gsToRemove = new List<GameState>();

        foreach (GameState gsToProcess in gameStatesToProcess)
            if (!gsToProcess.exists)
            {
                if (currentGameStates.Contains(gsToProcess))
                    gsToRemove.Add(gsToProcess);
            }
            else
            {
                if (!currentGameStates.Contains(gsToProcess))
                    gsToAdd.Add(gsToProcess);
            }

        foreach (GameState gs in gsToAdd)
            currentGameStates.Add(gs);
        
        foreach (GameState gs in gsToRemove)
            currentGameStates.Remove(gs);
    }

    public bool IsCurrentGameStateCorrectFor(HashSet<GameState> gsToCheckAgainst)
    {
        foreach (GameState gsToProcess in gsToCheckAgainst)
            if (!gsToProcess.exists)
            {
                if (currentGameStates.Contains(gsToProcess))
                    return false;
            }
            else
            {
                if (!currentGameStates.Contains(gsToProcess))
                    return false;
            }

        return false;
    }
    
}
