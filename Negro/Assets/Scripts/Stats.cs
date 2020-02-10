using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int health;
    private int mentalIllness;
    private int community;
    private int socialStatus;

    public Stats(int health = 0, int mentalIllness = 0, int community = 0, int socialStatus = 0)
    {
        this.health = health;
        this.mentalIllness = mentalIllness;
        this.community = community;
        this.socialStatus = socialStatus;
    }

    public void ApplyEffectOf(Stats modifications)
    {
        health += modifications.health;
        mentalIllness += modifications.mentalIllness;
        community += modifications.community;
        socialStatus += modifications.socialStatus;
    }
    
}
