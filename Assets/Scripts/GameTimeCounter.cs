using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCounter : MonoBehaviour
{
    DateTime m_dateTime;
    const int TickSecond = 3600;
    // ÉQÅ[ÉÄê¢äEÇÃ1ïb = åªé¿ê¢äEÇÃ1ïb * TImeRatio
    const float TimeRatio = 36000f;
    float m_residueTime = 0;

    GameTimeController m_GameTimeControllerCache;

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
            UpdateDateTime(new DateTime(m_dateTime.Ticks + TimeSpan.TicksPerSecond * TickSecond * TickCount),TickCount);

            m_residueTime -= TickSecond * TickCount;
        }
    }
     

    void UpdateDateTime(DateTime dateTime,int progressTickCount)
    {
        m_dateTime = dateTime;
        m_GameTimeControllerCache.UpdateDateTime(m_dateTime,progressTickCount);
    }
}
