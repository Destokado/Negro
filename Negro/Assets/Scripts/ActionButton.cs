using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    private Action currentAction;
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Action action)
    {
        currentAction = action;
        text.text = action.Text;
    }

    public void Perform()
    {
        currentAction.Perform();
    }
}
