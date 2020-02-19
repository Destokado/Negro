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
        currentAction = action.CanActionBeShownInGame()? action : null;
        tmProGui.text = action.CanActionBeShownInGame()? action.text : "";
    }

    public void Perform()
    {
        currentAction?.Perform();
    }
}
