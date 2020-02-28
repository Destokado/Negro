using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImageOpacityAnimation))]
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private OpacityAnimation[] elementsToAnimate;
    [SerializeField] private OpacityAnimation background;

    private void Awake()
    {
        background.SetOpacityTo(0, 0);
        foreach (OpacityAnimation opacityAnimation in elementsToAnimate)
            opacityAnimation.SetOpacityTo(0, 0);
    }
    
    public void Show(bool value)
    {
        background.SetOpacityTo((value)? 0.4f : 0, 0.5f);
        foreach (OpacityAnimation opacityAnimation in elementsToAnimate)
            opacityAnimation.SetOpacityTo((value)? 1 : 0, 0.5f);
    }

    public void Resume()
    {
        GameManager.Instance.pause = !GameManager.Instance.pause;
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
