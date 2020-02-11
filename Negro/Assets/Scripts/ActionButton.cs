using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text text;
    private Action currentAction;

    public void SetUp(Action action)
    {
        currentAction = action;
        if (text == null)
            Debug.LogWarning("PENE");
        text.text = action == null ? "" : action.text;
    }

    public void Perform()
    {
        if (currentAction != null)
            currentAction.Perform();
    }
}
