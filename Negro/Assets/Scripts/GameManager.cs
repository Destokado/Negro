using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;
    
    [SerializeField] private UIManager UiManager;
    private EventsManager eventsManager;
    private GameStateManager gameStateManager;
    private Statistics currentGameStatistics;



    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one GameObject has been created: " + Instance.name + " and " + gameObject.name, this);
        else
            Instance = this;
    }

    
    private void Start()
    {
        eventsManager = new EventsManager(EventFactory.BuildEvents(eventsCsv.downloadedFileName));
        gameStateManager = new GameStateManager(new HashSet<GameState>());
        currentGameStatistics = new Statistics(100,100,0);
        
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
        
        currentEvent = eventsManager.GetEventFor(gameStateManager);
        if (currentEvent == null)
            Debug.LogWarning("Not event found for the current Game State.");
        else
            gameStateManager.RemoveEventFromListOfForcedEvents(currentEvent);
        
        Debug.Log("Current event: " + currentEvent.ToString());
        UiManager.DrawEvent(currentEvent);
        Debug.Log("-------------------------------------------\n");
    }

    
    
    
    public void ApplyActionToGame(Action action)
    {
        gameStateManager.Campute(action.consequences);
        Debug.Log("The current Game State is: " + gameStateManager.ToString());
        currentGameStatistics.Compute(action.StatisticsModification);
        Debug.Log("The new stats for the game are: " + currentGameStatistics.ToString());
        UiManager.ShowConsequencesOf(action.StatisticsModification, currentGameStatistics);
        GameLoop();
    }

    private void EndGame()
    {
        // TODO
        throw new NotImplementedException();
    }

    private bool IsEndGame()
    {
        // TODO
        return false;
    }
}