using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;
    [SerializeField] private  ActionButton [] actionButtons;

    public void Draw(Event ev)
    {
        for (var i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(ev.Actions[i].IsValidAction());
            actionButtons[i].SetUp(ev.Actions[i]);
        }
    }

    public Action GetChosenAction()
    {
        return chosenAction;
    }
}
