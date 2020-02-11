using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;
    [SerializeField] private  ActionButton [] actionButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
