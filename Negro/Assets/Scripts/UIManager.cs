using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Action chosenAction;

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
    
    //TODO: Put the information in the screen
        throw new System.NotImplementedException();
    }

    public Action GetChosenAction()
    {
        return chosenAction;
    }
}
