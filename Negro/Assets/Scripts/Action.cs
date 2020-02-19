using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public GameStateManager consequences { get; private set; }
    public readonly string text;
    public Stats statsModification { get; private set; }


    public Action(string text, HashSet<GameState> consequences, Stats statsModification)
    {
        this.text = text;
        this.consequences = new GameStateManager(consequences);
        consequences.Remove(null);
        this.statsModification = statsModification;
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