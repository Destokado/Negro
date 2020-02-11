using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;
    [SerializeField] private UIManager UiManager;
    private EventsManager eventsManager;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one GameObject has been created: " + Instance.name + " and " + gameObject.name,
                this);
        else
            Instance = this;
    }

    private void Start()
    {
        eventsManager = new EventsManager(EventFactory.BuildEvents(eventsCsv.downloadedFileName));
        GameLoop();
    }

    private Event ev;
    Action action;

    private void GameLoop()
    {
        if (IsEndGame())
        {
            EndGame();
            return;
        }
        ev = eventsManager.GetEvent();
        UiManager.Draw(ev);
    }

    public void ApplyActionToGame(Action action)
    {
        throw new NotImplementedException();
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    private bool IsEndGame()
    {
        // TODO
        return false;
        throw new NotImplementedException();
    }
}