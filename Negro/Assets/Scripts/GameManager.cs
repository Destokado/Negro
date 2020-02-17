using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;
    [SerializeField] private UIManager UiManager;
    public static GameManager Instance;
    
    public EventsManager eventsManager;
    public HashSet<GameState> gameState;
    public Stats stats;
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

    private Event currentEvent;
    private void GameLoop()
    {
        if (IsEndGame())
        {
            EndGame();
            return;
        }
        currentEvent = eventsManager.GetEvent();
        UiManager.Draw(currentEvent);
    }

    public void ApplyActionToGame(Stats aStats,HashSet<GameState> aGameState)
    {
        gameState.UnionWith(aGameState);
        stats.ComputeStats(aStats);
        GameLoop();
    }

    private void EndGame()
    {
        throw new NotImplementedException();
    }

    private bool IsEndGame()
    {
        // TODO
        return false;
    }
}