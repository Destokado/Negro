using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;

    private EventsManager eventsManager;
    private UIManager UiManager;

    private void Start()
    {
        eventsManager = new EventsManager(EventFactory.BuildEvents(eventsCsv.downloadedFileName));
        UiManager = new UIManager();
        GameLoop();
    }

    private Event ev;
    Action action;

    private void GameLoop()
    {
        ev = eventsManager.GetEvent();
        UiManager.Draw(ev);
        //Wait for user Action
        action = UiManager.GetChosenAction();
        action.Perform();
        //Check if the game should end
        if (!IsEndGame()) GameLoop();
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    private bool IsEndGame()
    {
        throw new NotImplementedException();
    }
}