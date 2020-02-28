using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private OpacityAnimation[] elementsToAnimate;
    [SerializeField] private OpacityAnimation background;
    
    private void Awake()
    {
        background.SetOpacityTo(0, 0);
        foreach (OpacityAnimation opacityAnimation in elementsToAnimate)
            opacityAnimation.SetOpacityTo(0, 0);
    }
    
    public void Show(string message)
    {
        messageText.text = message;
        
        background.SetOpacityTo(1, 1f);
        foreach (OpacityAnimation opacityAnimation in elementsToAnimate)
            opacityAnimation.SetOpacityTo(1, 1f);
    }
    
    public void Exit()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void Restart()
    {
        GameManager.Instance.ReloadScene();
    }
}