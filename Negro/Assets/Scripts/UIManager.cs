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

    private void Start()
    {
        blackPanel.SetOpacityTo(1f, 0); // Fade to full opaque
    }

    private Event currentEvent = null;
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
    
    public void ShowConsequencesOf(Stats statsModification, Stats resultGameStats)
    {
        // TODO
    }
}
