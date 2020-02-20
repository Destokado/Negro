using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;
    [Range(0f, 5f)]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private  ActionButton [] actionButtons;
    [SerializeField] private Image eventBackground;
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private ImageOpacityAnimation blackPanel;
    
    [SerializeField] private VideoController videoController;

    private void Start()
    {
        blackPanel.SetOpacityTo(1f, 0); // Fade to full opaque
    }

    private Event currentEvent = null;
    [SerializeField] private int thresholdToShowEffectVideos;

    public void DrawEvent(Event ev)
    {
        currentEvent = ev;
        blackPanel.SetOpacityTo(1f, fadeDuration); // Fade to full opaque
        Invoke(nameof(DrawCurrentEvent), fadeDuration); // Change the event after the fade to full opaque ends
    }
    
    private void DrawCurrentEvent()
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(currentEvent.actions[i].CanActionBeShownInGame());
            actionButtons[i].SetUp(currentEvent.actions[i]);
        }
        
        eventBackground.sprite = Resources.Load<Sprite>(currentEvent.art);
        eventText.text = currentEvent.text;
        
        
        blackPanel.SetOpacityTo(0f, 1f); // Fade to non opacity
    }
    
    public void ShowConsequencesOf(Statistics statisticsModification, Statistics resultGameStatistics)
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
                videoController.ShowVideoFor(resultGameStatistics.health); break;
            case Statistic.Type.Sanity:
                videoController.ShowVideoFor(resultGameStatistics.sanity); break;
            case Statistic.Type.SocialStatus:
                videoController.ShowVideoFor(resultGameStatistics.socialStatus); break;
        }
        
        
    }
}
