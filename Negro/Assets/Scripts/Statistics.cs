using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics
{
    public Statistic health { get; private set; }
    public Statistic sanity { get; private set; }
    public Statistic socialStatus { get; private set; }

    public Statistics(int health, int sanity, int socialStatus)
    {
        this.health = new Statistic(Statistic.Type.Health, health);
        this.sanity = new Statistic(Statistic.Type.Sanity, sanity);
        this.socialStatus = new Statistic(Statistic.Type.SocialStatus, socialStatus);
    }

    public void Compute(Statistics modifications)
    {
        health.value = Mathf.Clamp(health.value+modifications.health.value, 0, 100);
        sanity.value = Mathf.Clamp(sanity.value+modifications.sanity.value, 0, 100);
        socialStatus.value = Mathf.Clamp(socialStatus.value+modifications.socialStatus.value, 0, 100);
    }

    public override string ToString()
    {
         return "Health: "+health.value+" | Sanity: "+sanity.value+" | Status: "+socialStatus.value;
        
    }
}
