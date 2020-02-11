using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    private HashSet<GameState> consequences;

    public string text => text;
    private Stats statsModification;

    public Action(string text, HashSet<GameState> consequences, Stats statsModification)
    {
        this.text = text;
        this.consequences = consequences;
        this.statsModification = statsModification;
    }

    public void Perform()
    {
        throw new System.NotImplementedException();
    }
}
