using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Text text;
    private Action currentAction;

    public void SetUp(Action action)
    {
        currentAction = action.IsValidAction()? action : null;
        text.text = action.IsValidAction()? action.text : "";
    }

    public void Perform()
    {
        currentAction?.Perform();
    }
}
