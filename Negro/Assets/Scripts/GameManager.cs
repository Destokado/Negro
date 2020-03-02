using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CsvOnlineSource eventsCsv;
    
    [SerializeField] private UIManager uiManager;
    private EventsManager eventsManager;
    private GameStateManager gameStateManager;
    private Statistics currentGameStatistics;
    private float delayBeforeGameplayStarts = 0.5f;
    public bool pause {
        get { return _pause; }
        set { _pause = value; uiManager.ShowPauseMenu(_pause);}
    }

    private bool _pause { get; set; }

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
        uiManager.SetBlackScreenTo(true, 0f);
        
        eventsManager = new EventsManager(EventFactory.BuildEvents(eventsCsv.downloadedFileName));

        gameStateManager = new GameStateManager(new HashSet<GameState>());
        currentGameStatistics = new Statistics(100,100,5);
        
        Invoke(nameof(GameLoop), delayBeforeGameplayStarts);
    }

    [MenuItem("Negro/Check events")]
    public static void CheckEventsCsv()
    {
        EventsManager eventsManager = new EventsManager(EventFactory.BuildEvents("Events"));
        eventsManager.CheckEvents();
        eventsManager.CheckActions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause = !pause;
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
        
        Debug.Log("Current event: " + currentEvent);
        uiManager.DrawEvent(currentEvent);
        Debug.Log("-------------------------------------------\n");
    }


    public void ApplyActionToGame(Action action)
    {
        
        Debug.Log("Performing action : '" + action + "' - Effects -> " + action.statisticsModification + "\n Consequences -> " + action.consequences);
        Debug.Log("· · · · · · · · · · · · · · · · · · · · · ·\n");
        StartCoroutine(CoroutineApplyActionToGame(action));
    }

    private IEnumerator CoroutineApplyActionToGame(Action action)
    {
        uiManager.SetBlackScreenTo(true);
        yield return new WaitForSeconds(UIManager.fadeDuration);
        
        gameStateManager.Compute(action.consequences);
        currentGameStatistics.Compute(action.statisticsModification);
        
        Debug.Log(" # Current stats: " + currentGameStatistics + "\n");
        Debug.Log(" # Current game state: " + gameStateManager + "\n");
        
        float delayForNextEvent = uiManager.ShowVideoConsequencesOf(action.statisticsModification, currentGameStatistics);
        Invoke(nameof(GameLoop), delayForNextEvent);
        //yield return new WaitForSeconds(delayForNextEvent);
        //GameLoop();
    }

    public void ForceGameLoop()
    {
        CancelInvoke(nameof(GameLoop));
        GameLoop();
    }

    private void EndGame()
    {
        uiManager.ShowEndScreen("Has muerto.");
    }

    private bool IsEndGame()
    {
        return currentGameStatistics.health.value <= 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}