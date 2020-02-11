using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    private string text;
    private HashSet<GameState> consequences;
    private Stats statsModification;

    public Action(string text, HashSet<GameState> consequences, Stats statsModification)
    {
        this.text = text;
        this.consequences = consequences;
        this.statsModification = statsModification;
    }

    public void Perform()
    {
        GameManager.Instance.ApplyActionToGame(this);
    }
}
