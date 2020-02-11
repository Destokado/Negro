﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    private string text;

    public string Text => text;

    private List<string> consequences;
    private Stats statsModification;

    public Action(string text, List<string> consequences, Stats statsModification)
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
