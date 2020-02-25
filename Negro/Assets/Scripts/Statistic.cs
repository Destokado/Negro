using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic
{
    public enum Type
    {
        Health,
        Sanity,
        SocialStatus
    }

    public readonly Type type;
    public int val
    {
        get { return _value; }
        set { _value = Mathf.Clamp(value, 0, 100); }
    }
    private int _value;

    public Statistic(Type type, int value)
    {
        this.type = type;
        this.val = val;
    }
}
