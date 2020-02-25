using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmProGui;
    private Action currentAction;

    public void SetUp(Action action)
    {
        currentAction = action.IsValid()? action : null;
        tmProGui.text = action.IsValid()? action.text : "";
    }

    public void Perform()
    {
        currentAction?.Perform();
    }
}
