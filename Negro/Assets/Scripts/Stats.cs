using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int health;
    private int sanity;
    private int socialStatus;

    public Stats(int health = 0, int sanity = 0, int socialStatus = 0)
    {
        this.health = health;
        this.sanity = sanity;
        this.socialStatus = socialStatus;
    }

    public void ComputeStats(Stats modifications)
    {
        health += modifications.health;
        sanity += modifications.sanity;
        socialStatus += modifications.socialStatus;
    }

    public override string ToString()
    {
         return "Health: "+health+"\n Sanity: "+sanity+"\n Status: "+socialStatus;
        
    }
}
