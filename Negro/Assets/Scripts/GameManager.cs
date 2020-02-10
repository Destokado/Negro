using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EventsManager eventsManager;

    private void Start()
    {
        eventsManager = new EventsManager();
    }
}
