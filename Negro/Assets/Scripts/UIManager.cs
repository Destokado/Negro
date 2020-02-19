using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;
    [SerializeField] private  ActionButton [] actionButtons;
    
    [SerializeField] private Image eventBackground;
    [SerializeField] private TextMeshProUGUI eventText;

    public void Draw(Event ev)
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(ev.actions[i].IsValidAction());
            actionButtons[i].SetUp(ev.actions[i]);
        }
        
        eventBackground.sprite = Resources.Load<Sprite>(ev.location);
        eventText.text = ev.text;
    }
    
}
