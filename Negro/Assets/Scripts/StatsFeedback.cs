using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsFeedback : MonoBehaviour
{
    [SerializeField] private Sprite arrow;
    [SerializeField] private Sprite dot;

    [SerializeField] private Image healthIcon;
    [SerializeField] private Image sanityIcon;
    [SerializeField] private Image socialStatusIcon;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private OpacityAnimation[] thingsToAnimate;
    //[SerializeField] private OpacityAnimation background;

    private void Start()
    {
        foreach (var thing in thingsToAnimate)
            thing.SetOpacityTo(0f, 0);
        //background.SetOpacityTo(0f,0);
    }

    public void ShowStatsModificationFeedback(Statistics modification)
    {
        SetImageToShowFeedbackOf(healthIcon, modification.health.value);
        SetImageToShowFeedbackOf(sanityIcon, modification.sanity.value);
        SetImageToShowFeedbackOf(socialStatusIcon, modification.socialStatus.value);
        
        foreach (var thing in thingsToAnimate)
            thing.SetOpacityTo(1f, fadeDuration);
        //background.SetOpacityTo(0.4f,fadeDuration);
    }
    
    
    public void HideFeedback()
    {
        foreach (var thing in thingsToAnimate)
            thing.SetOpacityTo(0f, fadeDuration);
        //background.SetOpacityTo(0f,fadeDuration);
    }

    private void SetImageToShowFeedbackOf(Image image, int modification)
    {
        image.sprite = (modification != 0)? arrow : dot;
        image.rectTransform.rotation = Quaternion.Euler(0f, 0f, (modification > 0)? 180f : 0f);
    }
}
