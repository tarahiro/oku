using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCounter : MonoBehaviour
{
    const int TickSecond = 3600;
    // ÉQÅ[ÉÄê¢äEÇÃ1ïb = åªé¿ê¢äEÇÃ1ïb * TImeRatio
    const float TimeRatio = 36000f;
    float m_residueTime = 0;

    MainManager m_mainManagerCache;
    GameTimeController m_GameTimeControllerCache;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
    }

    public void Set(DateTime time, GameTimeController gameTimeContoller)
    {
        m_GameTimeControllerCache = gameTimeContoller;
        UpdateDateTime(time, 0);
    }

    // Update is called once per frame
    void Update()
    {
        m_residueTime += Time.deltaTime * TimeRatio;

        if (m_residueTime > TickSecond)
        {
            int TickCount = (int)m_residueTime / TickSecond;

            //çXêVèàóù
            UpdateDateTime(new DateTime(StaticVariableCollector.gameTime.Ticks + TimeSpan.TicksPerSecond * TickSecond * TickCount),TickCount);

            m_residueTime -= TickSecond * TickCount;
        }
    }
     

    void UpdateDateTime(DateTime dateTime,int progressTickCount)
    {
        m_mainManagerCache.SetGameTime(dateTime);
        m_GameTimeControllerCache.UpdateDateTime(progressTickCount);
    }
}
