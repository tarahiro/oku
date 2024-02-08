using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    GameTimeCounter gameTimeCounter;
    private void Awake()
    {
        gameTimeCounter = FindObjectOfType<GameTimeCounter>();
    }

    public void SkipToNextDay()
    {
        gameTimeCounter.GoToNextDay();
    }
}
