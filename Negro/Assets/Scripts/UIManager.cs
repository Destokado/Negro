﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;
    public const float fadeDuration = 1f;
    
    [SerializeField] private  ActionButton [] actionButtons;
    [SerializeField] private Image eventBackground;
    [SerializeField] private TextMeshProUGUI eventText;
    
    [SerializeField] private ImageOpacityAnimation blackPanel;
    
    [SerializeField] private VideoController videoController;
    
    [SerializeField] private int thresholdToShowEffectVideos;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private EndScreen endScreen;
    
    public void DrawEvent(Event ev)
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(ev.actions[i].IsValid());
            actionButtons[i].SetUp(ev.actions[i]);
        }
        
        eventBackground.sprite = Resources.Load<Sprite>(ev.art);

        SetBlackScreenTo(false); // Change the event after the fade to full opaque ends
    }    

    public void SetBlackScreenTo(bool opaque, float fadeDuration = UIManager.fadeDuration)
    {
        blackPanel.SetOpacityTo(opaque? 1f : 0f, fadeDuration); // Fade to full opaque
    }
    
    public float ShowVideoConsequencesOf(Statistics statisticsModification, Statistics resultGameStatistics)
    {
        Statistic.Type? maxStat = null; // -1 = none, 0 = health, 1 = sanity, 2 = socialStatus
        int difMaxStat = thresholdToShowEffectVideos;
        
        if (Mathf.Abs(statisticsModification.health.value) >= difMaxStat)
        {
            difMaxStat = Mathf.Abs(statisticsModification.health.value);
            maxStat = Statistic.Type.Health;
        }
        
        if (Mathf.Abs(statisticsModification.sanity.value) >= difMaxStat)
        {
            difMaxStat = Mathf.Abs(statisticsModification.sanity.value);
            maxStat = Statistic.Type.Sanity;
        }
        
        if (Mathf.Abs(statisticsModification.socialStatus.value) >= difMaxStat)
        {
            difMaxStat = Mathf.Abs(statisticsModification.socialStatus.value);
            maxStat = Statistic.Type.SocialStatus;
        }
        
        switch (maxStat)
        {
            case Statistic.Type.Health:
                return videoController.ShowVideoFor(resultGameStatistics.health, resultGameStatistics.socialStatus.value > 50); 
            case Statistic.Type.Sanity:
                return videoController.ShowVideoFor(resultGameStatistics.sanity, resultGameStatistics.socialStatus.value > 50); 
            case Statistic.Type.SocialStatus:
                return videoController.ShowVideoFor(resultGameStatistics.socialStatus, resultGameStatistics.socialStatus.value > 50); 
            case null:
                return 0f;
        }
        
        return 0f;
    }

    public void ShowPauseMenu(bool value)
    {
        pauseMenu.Show(value);
    }
    
    public void ShowEndScreen(string endGameMessage)
    {
        endScreen.Show(endGameMessage);
    }


}
