using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;
    
    [SerializeField] private UIManager uiManager;
    private EventsManager eventsManager;
    private GameStateManager gameStateManager;
    private Statistics currentGameStatistics;
    private float delayBeforeGameplayStarts = 0.5f;


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
        uiManager.SetBlackScreenTo(true, 0f);
        
        eventsManager = new EventsManager(EventFactory.BuildEvents(eventsCsv.downloadedFileName));
        eventsManager.CheckEvents();
        eventsManager.CheckActions();
        
        gameStateManager = new GameStateManager(new HashSet<GameState>());
        currentGameStatistics = new Statistics(100,100,5);
        
        Invoke(nameof(GameLoop), delayBeforeGameplayStarts);
    }

    
    
    private Event currentEvent;
    private void GameLoop()
    {
        if (IsEndGame())
        {
            EndGame();
            return;
        }
        
        currentEvent = eventsManager.GetEventFor(currentGameStatistics, gameStateManager);
        if (currentEvent == null)
            Debug.LogWarning("Not event found for the current Game State.");
        else
            gameStateManager.RemoveEventFromListOfForcedEvents(currentEvent);
        
        Debug.Log("Current event: " + currentEvent.ToString());
        uiManager.DrawEvent(currentEvent);
        Debug.Log("-------------------------------------------\n");
    }


    public void ApplyActionToGame(Action action)
    {
        StartCoroutine(CoroutineApplyActionToGame(action));
    }

    private IEnumerator CoroutineApplyActionToGame(Action action)
    {
        uiManager.SetBlackScreenTo(true);
        yield return new WaitForSeconds(UIManager.fadeDuration);
        gameStateManager.Compute(action.consequences);
        Debug.Log("The current Game State is: " + gameStateManager.ToString());
        currentGameStatistics.Compute(action.StatisticsModification);
        Debug.Log("The new stats for the game are: " + currentGameStatistics.ToString());
        float delayForNextEvent = uiManager.ShowConsequencesOf(action.StatisticsModification, currentGameStatistics);
        yield return new WaitForSeconds(delayForNextEvent);
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