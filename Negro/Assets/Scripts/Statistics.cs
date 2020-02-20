using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics
{
    public Statistic health { get; private set; }
    public Statistic sanity { get; private set; }
    public Statistic socialStatus { get; private set; }

    public Statistics(int health = 0, int sanity = 0, int socialStatus = 0)
    {
        this.health = new Statistic(Statistic.Type.Health, health);
        this.sanity = new Statistic(Statistic.Type.Sanity, sanity);
        this.socialStatus = new Statistic(Statistic.Type.SocialStatus, socialStatus);
    }

    public void Compute(Statistics modifications)
    {
        health.value += modifications.health.value;
        sanity.value += modifications.sanity.value;
        socialStatus.value += modifications.socialStatus.value;
    }

    public override string ToString()
    {
         return "Health: "+health+" | Sanity: "+sanity+" | Status: "+socialStatus;
        
    }
}
