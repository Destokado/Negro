using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public GameStateManager consequences { get; private set; }
    public readonly string text;
    public Statistics StatisticsModification { get; private set; }


    public Action(string text, HashSet<GameState> consequences, Statistics statisticsModification)
    {
        this.text = text;
        this.consequences = new GameStateManager(consequences);
        consequences.Remove(null);
        this.StatisticsModification = statisticsModification;
    }

    public void Perform()
    {
        GameManager.Instance.ApplyActionToGame(this);
    }

    public bool CanActionBeShownInGame()
    {
        return ! ( string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text) ) ;
    }
}