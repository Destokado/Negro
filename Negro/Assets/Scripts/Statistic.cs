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
    public int value;

    public Statistic(Type type, int value)
    {
        this.type = type;
        this.value = value;
    }
}
