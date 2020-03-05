using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ActionButton : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI tmProGui;
    private Action currentAction;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetUp(Action action)
    {
        currentAction = action.IsValid()? action : null;
        tmProGui.text = action.IsValid()? action.text : "";
    }

    public void Perform()
    {
        
        audioSource.Play();
        currentAction?.Perform();
    }
}
